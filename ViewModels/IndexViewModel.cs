using WebMarkupMin.Core;

namespace JadeX.HtmlMinifier.ViewModels
{
    public class IndexViewModel
    {
        public bool ExcludeAdmin { get; set; }

        public bool ExcludeAuthenticated { get; set; }

        public bool HaltOnWarnings { get; set; }

        public string IgnoredUrls { get; set; }

        public WhitespaceMinificationMode WhitespaceMinificationMode { get; set; }

        public bool RemoveHtmlComments { get; set; }

        public bool RemoveHtmlCommentsFromScriptsAndStyles { get; set; }

        public bool RemoveCdataSectionsFromScriptsAndStyles { get; set; }

        public bool UseShortDoctype { get; set; }

        public bool PreserveCase { get; set; }

        public bool UseMetaCharsetTag { get; set; }

        public HtmlEmptyTagRenderMode EmptyTagRenderMode { get; set; }

        public bool RemoveOptionalEndTags { get; set; }

        public string PreservableOptionalTagList { get; set; }

        public bool RemoveTagsWithoutContent { get; set; }

        public bool CollapseBooleanAttributes { get; set; }

        public bool RemoveEmptyAttributes { get; set; }

        public HtmlAttributeQuotesRemovalMode AttributeQuotesRemovalMode { get; set; }

        public bool RemoveRedundantAttributes { get; set; }

        public bool RemoveJsTypeAttributes { get; set; }

        public bool RemoveCssTypeAttributes { get; set; }

        public bool RemoveHttpProtocolFromAttributes { get; set; }

        public bool RemoveHttpsProtocolFromAttributes { get; set; }

        public bool RemoveJsProtocolFromAttributes { get; set; }

        public bool MinifyEmbeddedCssCode { get; set; }

        public bool MinifyInlineCssCode { get; set; }

        public bool MinifyEmbeddedJsCode { get; set; }

        public bool MinifyInlineJsCode { get; set; }

        public string ProcessableScriptTypeList { get; set; }

        public bool MinifyKnockoutBindingExpressions { get; set; }

        public bool MinifyAngularBindingExpressions { get; set; }

        public string CustomAngularDirectiveList { get; set; }

        public string StatisticsInfoWindowPattern { get; set; }
    }
}
