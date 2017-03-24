using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using JadeX.HtmlMinifier.Models;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Logging;
using Orchard.Mvc.Filters;
using Orchard.Security;
using Orchard.UI.Admin;
using WebMarkupMin.Core;

namespace JadeX.HtmlMinifier.Filters
{
    public class HtmlFilter : FilterProvider, IActionFilter
    {
        private readonly WorkContext workContext;
        private readonly ICacheManager cacheManager;
        private readonly ISignals signals;
        private readonly IAuthorizer authorizer;

        public ILogger Logger { get; set; }

        public HtmlFilter(IWorkContextAccessor workContext, ICacheManager cacheManager, ISignals signals, IAuthorizer authorizer)
        {
            Logger = NullLogger.Instance;
            this.workContext = workContext.GetContext();
            this.cacheManager = cacheManager;
            this.signals = signals;
            this.authorizer = authorizer;
        }

        private bool IsIgnoredUrl(string url, string ignoredUrls)
        {
            if (string.IsNullOrEmpty(ignoredUrls))
            {
                return false;
            }

            url = url.TrimStart('~');

            using (var urlReader = new StringReader(ignoredUrls))
            {
                string relativePath;
                while ((relativePath = urlReader.ReadLine()) != null)
                {
                    relativePath = relativePath.TrimStart('~').Trim();

                    // Ignore empty lines and comments
                    if (string.IsNullOrWhiteSpace(relativePath) || relativePath.StartsWith("#"))
                    {
                        continue;
                    }

                    if (string.Equals(relativePath, url, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var settings = cacheManager.Get(
                MinificationSettingsPart.CacheKey,
                context =>
                {
                    context.Monitor(signals.When(MinificationSettingsPart.CacheKey));
                    return workContext.CurrentSite.As<MinificationSettingsPart>();
                });

            var isAdminPage = AdminFilter.IsApplied(filterContext.RequestContext);

            var isIgnoredUrl = IsIgnoredUrl(filterContext.RequestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath, settings.IgnoredUrls);
            var isAdminExcluded = settings.ExcludeAdmin && isAdminPage;
            var isAuthenticatedExcluded = workContext.CurrentUser != null && settings.ExcludeAuthenticated && !isAdminPage;
            var debugEnabled = filterContext.HttpContext.IsDebuggingEnabled;
            var isHtml = new[] { "text/html" }.Contains(workContext.HttpContext.Response.ContentType.ToLower());

            if (filterContext.HttpContext.Response.Filter == null || isAdminExcluded || isAuthenticatedExcluded || isIgnoredUrl || debugEnabled || !isHtml)
            {
                return;
            }

            filterContext.HttpContext.Response.Filter = new MinifyHtmlFilter(
                filterContext.HttpContext.Response.Filter,
                filterContext.HttpContext.Response.Output.Encoding,
                settings,
                Logger,
                authorizer.Authorize(StandardPermissions.SiteOwner));
        }
    }

    internal class MinifyHtmlFilter : MemoryStream
    {
        private readonly Stream stream;
        private readonly Encoding encoding;
        private readonly MinificationSettingsPart settings;
        private string html;
        private readonly bool isOwner;

        public MinifyHtmlFilter(Stream filter, Encoding encoding, MinificationSettingsPart settings, ILogger logger, bool isOwner)
        {
            this.isOwner = isOwner;
            Logger = logger;
            stream = filter;
            this.encoding = encoding;
            this.settings = settings;
        }

        private ILogger Logger { get; set; }

        public override void Write(byte[] buffer, int offset, int count)
        {
            html += encoding.GetString(buffer);
        }

        public override void Flush()
        {
            var htmlMinifier = new WebMarkupMin.Core.HtmlMinifier(new HtmlMinificationSettings
            {
                WhitespaceMinificationMode = settings.WhitespaceMinificationMode,
                RemoveHtmlComments = settings.RemoveHtmlComments,
                RemoveHtmlCommentsFromScriptsAndStyles = settings.RemoveHtmlCommentsFromScriptsAndStyles,
                RemoveCdataSectionsFromScriptsAndStyles = settings.RemoveCdataSectionsFromScriptsAndStyles,
                UseShortDoctype = settings.UseShortDoctype,
                PreserveCase = settings.PreserveCase,
                UseMetaCharsetTag = settings.UseMetaCharsetTag,
                EmptyTagRenderMode = settings.EmptyTagRenderMode,
                RemoveOptionalEndTags = settings.RemoveOptionalEndTags,
                PreservableOptionalTagList = settings.PreservableOptionalTagList,
                RemoveTagsWithoutContent = settings.RemoveTagsWithoutContent,
                CollapseBooleanAttributes = settings.CollapseBooleanAttributes,
                RemoveEmptyAttributes = settings.RemoveEmptyAttributes,
                AttributeQuotesRemovalMode = settings.AttributeQuotesRemovalMode,
                RemoveRedundantAttributes = settings.RemoveRedundantAttributes,
                RemoveJsTypeAttributes = settings.RemoveJsTypeAttributes,
                RemoveCssTypeAttributes = settings.RemoveCssTypeAttributes,
                RemoveHttpProtocolFromAttributes = settings.RemoveHttpProtocolFromAttributes,
                RemoveHttpsProtocolFromAttributes = settings.RemoveHttpsProtocolFromAttributes,
                RemoveJsProtocolFromAttributes = settings.RemoveJsProtocolFromAttributes,
                MinifyEmbeddedCssCode = settings.MinifyEmbeddedCssCode,
                MinifyInlineCssCode = settings.MinifyInlineCssCode,
                MinifyEmbeddedJsCode = settings.MinifyEmbeddedJsCode,
                MinifyInlineJsCode = settings.MinifyInlineJsCode,
                ProcessableScriptTypeList = settings.ProcessableScriptTypeList,
                MinifyKnockoutBindingExpressions = settings.MinifyKnockoutBindingExpressions,
                MinifyAngularBindingExpressions = settings.MinifyAngularBindingExpressions,
                CustomAngularDirectiveList = settings.CustomAngularDirectiveList
            });

            var generateStatistics = !string.IsNullOrWhiteSpace(settings.StatisticsInfoWindowPattern) && isOwner;

            var result = htmlMinifier.Minify(html, string.Empty, encoding, generateStatistics);

            foreach (var error in result.Errors)
            {
                Logger.Error(error.Message);
            }

            var shouldHaltOnWarnings = result.Warnings.Count > 0 && settings.HaltOnWarnings;

            foreach (var warning in result.Warnings)
            {
                if (shouldHaltOnWarnings)
                {
                    Logger.Error(warning.Message);
                }
                else
                {
                    Logger.Warning(warning.Message);
                }
            }

            if (result.Errors.Count == 0 && (result.Warnings.Count == 0 || !shouldHaltOnWarnings))
            {
                html = result.MinifiedContent;

                if (generateStatistics)
                {
                    var statistics = settings.StatisticsInfoWindowPattern
                        .Replace("{Gzip-CompressionRatio}", $"{(float) result.Statistics.CompressionGzipRatio/100:P1}")
                        .Replace("{CompressionRatio}", $"{(float) result.Statistics.CompressionRatio/100:P1}")
                        .Replace("{MinificationDuration}", $"{result.Statistics.MinificationDuration} ms")
                        .Replace("{Gzip-OriginalSize}", $"{(float) result.Statistics.OriginalGzipSize/1000:F} KB")
                        .Replace("{OriginalSize}", $"{(float) result.Statistics.OriginalSize/1000:N} KB")
                        .Replace("{Gzip-MinifiedSize}", $"{(float) result.Statistics.MinifiedGzipSize/1000:N} KB")
                        .Replace("{MinifiedSize}", $"{(float) result.Statistics.MinifiedSize/1000:N} KB")
                        .Replace("{Gzip-Saved}", $"{(float) result.Statistics.SavedGzipInBytes/1000:N} KB")
                        .Replace("{Gzip-SavedInPercent}", $"{(float) result.Statistics.SavedGzipInPercent/100:P1}")
                        .Replace("{Saved}", $"{(float) result.Statistics.SavedInBytes/1000:N} KB")
                        .Replace("{SavedInPercent}", $"{(float) result.Statistics.SavedInPercent/100:P1}");

                    result = htmlMinifier.Minify(statistics, encoding);

                    var bodyIndex = html.IndexOf("</body>", StringComparison.Ordinal);

                    if (bodyIndex > 0)
                    {
                        html = html.Insert(bodyIndex, result.MinifiedContent);
                    }
                    else
                    {
                        html += result.MinifiedContent;
                    }
                }
            }

            stream.Write(encoding.GetBytes(html), 0, encoding.GetByteCount(html));
        }
    }
}
