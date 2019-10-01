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

        public StructuredText Content { get; set; }

        public StructuredTextTagHelper(DocumentLinkResolver linkResolver)
        {
            _linkResolver = linkResolver;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            if (Content == null || !Content.Blocks.Any())
                return;

            output.Attributes.RemoveAll("content");           
            output.PreContent.AppendHtml(GetHtml(context));
        }

        protected virtual string GetHtml(TagHelperContext context) {
            var attributes = CreateHtmlAttributeString(context);

            return (Content.Blocks.Count == 1)
                    ? GetHtml(attributes)
                    : $"<div{attributes}>{GetHtml()}</div>";
        }

        protected virtual string GetHtml(string attributes = null)
            => string.IsNullOrWhiteSpace(attributes)
                ? Content.AsHtml(_linkResolver)
                : Content.AsHtml(_linkResolver, Serializer(attributes));

        protected virtual List<TagHelperAttribute> GetAttributes(TagHelperContext context)
            => context
                .AllAttributes
                .Where(x => !x.Name.Equals("content", StringComparison.InvariantCultureIgnoreCase))
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
                case HtmlAttributeValueStyle.DoubleQuotes:
                    return $"{attr.Name}=\"{attr.Value}\"";
                case HtmlAttributeValueStyle.SingleQuotes:
                    return $"{attr.Name}='{attr.Value}'";
                case HtmlAttributeValueStyle.NoQuotes:
                    return $"{attr.Name}={attr.Value}";
                case HtmlAttributeValueStyle.Minimized:
                    return attr.Name;
            }

            return string.Empty;
        }

        protected virtual HtmlSerializer Serializer(string attributes) =>
            HtmlSerializer.For((el, body) =>
            {
                if(body == string.Empty)
                    return string.Empty;

                switch (el)
                {
                    case StructuredText.Heading h:
                        return $"<h{h.Level} {attributes}>{body}</h{h.Level}>";
                    case StructuredText.Paragraph p:
                        return $"<p {attributes}>{body}</p>";
                        // case StructuredText.Image img:
                        //     return $"<img src={img.View.Url} alt={img.View.Alt} {attributes} />";
                    default: 
                        return null;
                }

            });
    }
}