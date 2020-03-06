using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AspNetCore.Mvc
{
    [HtmlTargetElement("structured-text")]
    public class StructuredTextTagHelper : TagHelper
    {
        private readonly DocumentLinkResolver _linkResolver;

        public StructuredText Fragment { get; set; }

        public StructuredTextTagHelper(DocumentLinkResolver linkResolver)
        {
            _linkResolver = linkResolver ?? throw new ArgumentNullException(nameof(linkResolver));
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            if (Fragment == null || !Fragment.Blocks.Any())
                return;

            output.Attributes.RemoveAll("content");
            output.Content.SetHtmlContent(GetHtml(context));
        }

        protected virtual string GetHtml(TagHelperContext context)
        {
            var cssClass = context.AllAttributes.FirstOrDefault(x => x?.Name == "class");
            var attributes = CreateHtmlAttributeString(context);

            return (Fragment.Blocks.Count == 1)
                    ? GetHtml(attributes)
                    : $"<div{attributes}>{GetHtml()}</div>";
        }

        protected virtual string GetHtml(string attributes = null)
            => string.IsNullOrWhiteSpace(attributes)
                ? Fragment.AsHtml(_linkResolver)
                : Fragment.AsHtml(_linkResolver, Serializer(attributes));

        protected virtual List<TagHelperAttribute> GetAttributes(TagHelperContext context)
            => context
                .AllAttributes
                .Where(x => x?.Name.Equals("content", StringComparison.InvariantCultureIgnoreCase) != true)
                .ToList();

        protected string CreateHtmlAttributeString(TagHelperContext context)
        {
            var attributes = string.Join(
                " ",
                GetAttributes(context)
                    .Select(BuildAttribute)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                );

            if (string.IsNullOrWhiteSpace(attributes))
                return string.Empty;

            return $" {attributes}";
        }

        protected string BuildAttribute(TagHelperAttribute attr)
        {
            if (attr == null)
                return string.Empty;

            switch (attr.ValueStyle)
            {
                case HtmlAttributeValueStyle.SingleQuotes:
                    return $"{attr.Name}='{attr.Value}'";
                case HtmlAttributeValueStyle.NoQuotes:
                    return $"{attr.Name}={attr.Value}";
                case HtmlAttributeValueStyle.Minimized:
                    return attr.Name;
                case HtmlAttributeValueStyle.DoubleQuotes:
                default:
                    return $"{attr.Name}=\"{attr.Value}\"";
            }
        }

        protected virtual HtmlSerializer Serializer(string attributes)
            => HtmlSerializer.For(
                (el, body) =>
                {
                    if (body == string.Empty)
                        return string.Empty;

                    switch (el)
                    {
                        case StructuredText.Heading h:
                            return $"<h{h.Level}{attributes}>{body}</h{h.Level}>";
                        case StructuredText.Paragraph p:
                            return $"<p{attributes}>{body}</p>";
                        default:
                            return null;
                    }
                }
            );
    }
}