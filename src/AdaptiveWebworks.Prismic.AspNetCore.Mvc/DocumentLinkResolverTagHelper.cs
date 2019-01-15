using Microsoft.AspNetCore.Razor.TagHelpers;
using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AspNetCore.Mvc
{
    [HtmlTargetElement("a", Attributes = "prismic-resolve-link")]
    public class PrismicLinkResolverTagHelper : TagHelper
    {
        private readonly DocumentLinkResolver _linkResolver;

        [HtmlAttributeName("prismic-resolve-link")]
        public Link Link { get; set; }

        public PrismicLinkResolverTagHelper(DocumentLinkResolver linkResolver)
        {
            _linkResolver = linkResolver;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("prismic-resolve-link");
            output.Attributes.Add("href", ResolveLink());
        }

        protected virtual string ResolveLink()
        {
            switch (Link)
            {
                case DocumentLink d:
                    return _linkResolver.Resolve(d);
                case WebLink w:
                    return w.Url;
                case ImageLink i:
                    return i.Url;
                case FileLink f:
                    return f.Url;
                default: 
                    return "#";
            }
        }
    }
}
