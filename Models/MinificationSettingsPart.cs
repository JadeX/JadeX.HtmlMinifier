using Orchard.ContentManagement;
using WebMarkupMin.Core;

namespace JadeX.HtmlMinifier.Models
{
    public class MinificationSettingsPart : ContentPart
    {
        public const string CacheKey = "MinificationSettingsPart";

        public bool ExcludeAdmin
        {
            get { return this.Retrieve(x => x.ExcludeAdmin, true); }
            set { this.Store(x => x.ExcludeAdmin, value); }
        }

        public bool ExcludeAuthenticated
        {
            get { return this.Retrieve(x => x.ExcludeAuthenticated, true); }
            set { this.Store(x => x.ExcludeAuthenticated, value); }
        }

        public bool HaltOnWarnings
        {
            get { return this.Retrieve(x => x.HaltOnWarnings); }
            set { this.Store(x => x.HaltOnWarnings, value); }
        }

        public string IgnoredUrls
        {
            get { return this.Retrieve(x => x.IgnoredUrls); }
            set { this.Store(x => x.IgnoredUrls, value); }
        }

        public WhitespaceMinificationMode WhitespaceMinificationMode
        {
            get { return this.Retrieve(x => x.WhitespaceMinificationMode, WhitespaceMinificationMode.Medium); }
            set { this.Store(x => x.WhitespaceMinificationMode, value); }
        }

        public bool RemoveHtmlComments
        {
            get { return this.Retrieve(x => x.RemoveHtmlComments, true); }
            set { this.Store(x => x.RemoveHtmlComments, value); }
        }

        public bool RemoveHtmlCommentsFromScriptsAndStyles
        {
            get { return this.Retrieve(x => x.RemoveHtmlCommentsFromScriptsAndStyles, true); }
            set { this.Store(x => x.RemoveHtmlCommentsFromScriptsAndStyles, value); }
        }

        public bool RemoveCdataSectionsFromScriptsAndStyles
        {
            get { return this.Retrieve(x => x.RemoveCdataSectionsFromScriptsAndStyles, true); }
            set { this.Store(x => x.RemoveCdataSectionsFromScriptsAndStyles, value); }
        }

        public bool UseShortDoctype
        {
            get { return this.Retrieve(x => x.UseShortDoctype, true); }
            set { this.Store(x => x.UseShortDoctype, value); }
        }

        public bool PreserveCase
        {
            get { return this.Retrieve(x => x.PreserveCase); }
            set { this.Store(x => x.PreserveCase, value); }
        }

        public bool UseMetaCharsetTag
        {
            get { return this.Retrieve(x => x.UseMetaCharsetTag, true); }
            set { this.Store(x => x.UseMetaCharsetTag, value); }
        }

        public HtmlEmptyTagRenderMode EmptyTagRenderMode
        {
            get { return this.Retrieve(x => x.EmptyTagRenderMode); }
            set { this.Store(x => x.EmptyTagRenderMode, value); }
        }

        public bool RemoveOptionalEndTags
        {
            get { return this.Retrieve(x => x.RemoveOptionalEndTags, true); }
            set { this.Store(x => x.RemoveOptionalEndTags, value); }
        }

        public string PreservableOptionalTagList
        {
            get { return this.Retrieve(x => x.PreservableOptionalTagList); }
            set { this.Store(x => x.PreservableOptionalTagList, value); }
        }

        public bool RemoveTagsWithoutContent
        {
            get { return this.Retrieve(x => x.RemoveTagsWithoutContent); }
            set { this.Store(x => x.RemoveTagsWithoutContent, value); }
        }

        public bool CollapseBooleanAttributes
        {
            get { return this.Retrieve(x => x.CollapseBooleanAttributes, true); }
            set { this.Store(x => x.CollapseBooleanAttributes, value); }
        }

        public bool RemoveEmptyAttributes
        {
            get { return this.Retrieve(x => x.RemoveEmptyAttributes, true); }
            set { this.Store(x => x.RemoveEmptyAttributes, value); }
        }

        public HtmlAttributeQuotesRemovalMode AttributeQuotesRemovalMode
        {
            get { return this.Retrieve(x => x.AttributeQuotesRemovalMode, HtmlAttributeQuotesRemovalMode.Html5); }
            set { this.Store(x => x.AttributeQuotesRemovalMode, value); }
        }

        public bool RemoveRedundantAttributes
        {
            get { return this.Retrieve(x => x.RemoveRedundantAttributes, true); }
            set { this.Store(x => x.RemoveRedundantAttributes, value); }
        }

        public bool RemoveJsTypeAttributes
        {
            get { return this.Retrieve(x => x.RemoveJsTypeAttributes, true); }
            set { this.Store(x => x.RemoveJsTypeAttributes, value); }
        }

        public bool RemoveCssTypeAttributes
        {
            get { return this.Retrieve(x => x.RemoveCssTypeAttributes, true); }
            set { this.Store(x => x.RemoveCssTypeAttributes, value); }
        }

        public bool RemoveHttpProtocolFromAttributes
        {
            get { return this.Retrieve(x => x.RemoveHttpProtocolFromAttributes); }
            set { this.Store(x => x.RemoveHttpProtocolFromAttributes, value); }
        }

        public bool RemoveHttpsProtocolFromAttributes
        {
            get { return this.Retrieve(x => x.RemoveHttpsProtocolFromAttributes); }
            set { this.Store(x => x.RemoveHttpsProtocolFromAttributes, value); }
        }

        public bool RemoveJsProtocolFromAttributes
        {
            get { return this.Retrieve(x => x.RemoveJsProtocolFromAttributes, true); }
            set { this.Store(x => x.RemoveJsProtocolFromAttributes, value); }
        }

        public bool MinifyEmbeddedCssCode
        {
            get { return this.Retrieve(x => x.MinifyEmbeddedCssCode, true); }
            set { this.Store(x => x.MinifyEmbeddedCssCode, value); }
        }

        public bool MinifyInlineCssCode
        {
            get { return this.Retrieve(x => x.MinifyInlineCssCode, true); }
            set { this.Store(x => x.MinifyInlineCssCode, value); }
        }

        public bool MinifyEmbeddedJsCode
        {
            get { return this.Retrieve(x => x.MinifyEmbeddedJsCode, true); }
            set { this.Store(x => x.MinifyEmbeddedJsCode, value); }
        }

        public bool MinifyInlineJsCode
        {
            get { return this.Retrieve(x => x.MinifyInlineJsCode, true); }
            set { this.Store(x => x.MinifyInlineJsCode, value); }
        }

        public string ProcessableScriptTypeList
        {
            get { return this.Retrieve(x => x.ProcessableScriptTypeList); }
            set { this.Store(x => x.ProcessableScriptTypeList, value); }
        }

        public bool MinifyKnockoutBindingExpressions
        {
            get { return this.Retrieve(x => x.MinifyKnockoutBindingExpressions); }
            set { this.Store(x => x.MinifyKnockoutBindingExpressions, value); }
        }

        public bool MinifyAngularBindingExpressions
        {
            get { return this.Retrieve(x => x.MinifyAngularBindingExpressions); }
            set { this.Store(x => x.MinifyAngularBindingExpressions, value); }
        }

        public string CustomAngularDirectiveList
        {
            get { return this.Retrieve(x => x.CustomAngularDirectiveList); }
            set { this.Store(x => x.CustomAngularDirectiveList, value); }
        }

        public string StatisticsInfoWindowPattern
        {
            get { return this.Retrieve(x => x.StatisticsInfoWindowPattern); }
            set { this.Store(x => x.StatisticsInfoWindowPattern, value); }
        }
    }
}
