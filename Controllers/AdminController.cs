using System.Web.Mvc;
using JadeX.HtmlMinifier.Models;
using JadeX.HtmlMinifier.ViewModels;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using Orchard.UI.Notify;

namespace JadeX.HtmlMinifier.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly ISignals signals;

        public AdminController(ISignals signals, IOrchardServices services)
        {
            this.signals = signals;
            Services = services;
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        public ActionResult Index()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage HTML Minifier - Features")))
            {
                return new HttpUnauthorizedResult();
            }

            var settings = Services.WorkContext.CurrentSite.As<MinificationSettingsPart>();

            var model = new IndexViewModel {
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
                CustomAngularDirectiveList = settings.CustomAngularDirectiveList,
                StatisticsInfoWindowPattern = settings.StatisticsInfoWindowPattern
            };

            return View(model);
        }

        [HttpPost, ActionName("Index")]
        [ValidateInput(false)]
        public ActionResult IndexPost()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage HTML Minifier - Features")))
            {
                return new HttpUnauthorizedResult();
            }

            var model = new IndexViewModel();

            if (TryUpdateModel(model))
            {
                var settings = Services.WorkContext.CurrentSite.As<MinificationSettingsPart>();

                settings.WhitespaceMinificationMode = model.WhitespaceMinificationMode;
                settings.RemoveHtmlComments = model.RemoveHtmlComments;
                settings.RemoveHtmlCommentsFromScriptsAndStyles = model.RemoveHtmlCommentsFromScriptsAndStyles;
                settings.RemoveCdataSectionsFromScriptsAndStyles = model.RemoveCdataSectionsFromScriptsAndStyles;
                settings.UseShortDoctype = model.UseShortDoctype;
                settings.PreserveCase = model.PreserveCase;
                settings.UseMetaCharsetTag = model.UseMetaCharsetTag;
                settings.EmptyTagRenderMode = model.EmptyTagRenderMode;
                settings.RemoveOptionalEndTags = model.RemoveOptionalEndTags;
                settings.PreservableOptionalTagList = model.PreservableOptionalTagList;
                settings.PreservableOptionalTagList = model.PreservableOptionalTagList;
                settings.RemoveTagsWithoutContent = model.RemoveTagsWithoutContent;
                settings.CollapseBooleanAttributes = model.CollapseBooleanAttributes;
                settings.RemoveEmptyAttributes = model.RemoveEmptyAttributes;
                settings.AttributeQuotesRemovalMode = model.AttributeQuotesRemovalMode;
                settings.RemoveRedundantAttributes = model.RemoveRedundantAttributes;
                settings.RemoveJsTypeAttributes = model.RemoveJsTypeAttributes;
                settings.RemoveCssTypeAttributes = model.RemoveCssTypeAttributes;
                settings.RemoveHttpProtocolFromAttributes = model.RemoveHttpProtocolFromAttributes;
                settings.RemoveHttpsProtocolFromAttributes = model.RemoveHttpsProtocolFromAttributes;
                settings.RemoveJsProtocolFromAttributes = model.RemoveJsProtocolFromAttributes;
                settings.MinifyEmbeddedCssCode = model.MinifyEmbeddedCssCode;
                settings.MinifyInlineCssCode = model.MinifyInlineCssCode;
                settings.MinifyEmbeddedJsCode = model.MinifyEmbeddedJsCode;
                settings.MinifyInlineJsCode = model.MinifyInlineJsCode;
                settings.ProcessableScriptTypeList = model.ProcessableScriptTypeList;
                settings.MinifyKnockoutBindingExpressions = model.MinifyKnockoutBindingExpressions;
                settings.MinifyAngularBindingExpressions = model.MinifyAngularBindingExpressions;
                settings.CustomAngularDirectiveList = model.CustomAngularDirectiveList;
                settings.StatisticsInfoWindowPattern = model.StatisticsInfoWindowPattern;

                signals.Trigger(MinificationSettingsPart.CacheKey);

                Services.Notifier.Information(T("Feature settings saved successfully."));
            }
            else
            {
                Services.Notifier.Error(T("Could not save feature settings."));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Exclusions()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage HTML Minifier - Exclusions")))
            {
                return new HttpUnauthorizedResult();
            }

            var settings = Services.WorkContext.CurrentSite.As<MinificationSettingsPart>();

            var model = new IndexViewModel {
                ExcludeAdmin = settings.ExcludeAdmin,
                ExcludeAuthenticated = settings.ExcludeAuthenticated,
                HaltOnWarnings = settings.HaltOnWarnings,
                IgnoredUrls = settings.IgnoredUrls
            };

            return View(model);
        }

        [HttpPost, ActionName("Exclusions")]
        public ActionResult ExclusionsPost()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage HTML Minifier - Exclusions")))
            {
                return new HttpUnauthorizedResult();
            }

            var model = new IndexViewModel();

            if (TryUpdateModel(model))
            {
                var settings = Services.WorkContext.CurrentSite.As<MinificationSettingsPart>();

                settings.ExcludeAdmin = model.ExcludeAdmin;
                settings.ExcludeAuthenticated = model.ExcludeAuthenticated;
                settings.HaltOnWarnings = model.HaltOnWarnings;
                settings.IgnoredUrls = model.IgnoredUrls;

                signals.Trigger(MinificationSettingsPart.CacheKey);

                Services.Notifier.Information(T("Exclusion settings saved successfully."));
            }
            else
            {
                Services.Notifier.Error(T("Could not save exclusion settings."));
            }

            return RedirectToAction("Exclusions");
        }
    }
}
