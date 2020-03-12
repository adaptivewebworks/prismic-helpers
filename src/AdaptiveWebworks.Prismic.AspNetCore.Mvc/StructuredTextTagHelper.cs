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
            //TODO: look to move Block count check into here changing the tag name to div and leaving attributes in place for more than 1 block.
            output.SuppressOutput();

            if (Fragment == null || !Fragment.Blocks.Any())
                return;

// TODO: for 1 block elements call suppress output here and change content...
            output.Attributes.RemoveAll("fragment");
            output.Content.SetHtmlContent(GetHtml(context));
        }

        protected virtual string GetHtml(TagHelperContext context)
        {
            var attributes = GetAttributes(context);

            return (Fragment.Blocks.Count == 1)
                    ? GetHtml(attributes, Fragment.Blocks.First().Label)
                    : $"<div{CreateHtmlAttributeString(attributes)}>{Fragment.AsHtml(_linkResolver)}</div>";
        }

        protected virtual string GetHtml(List<TagHelperAttribute> attributes, string label = null)
        {
            var parsedAttrs = CreateHtmlAttributeString(attributes, label);

            return Fragment.AsHtml(_linkResolver, Serializer(parsedAttrs));
        }

        protected virtual List<TagHelperAttribute> GetAttributes(TagHelperContext context)
            => context
                .AllAttributes
                .Where(x => x?.Name.Equals("fragment", StringComparison.InvariantCultureIgnoreCase) != true)
                .ToList();

        protected string CreateHtmlAttributeString(List<TagHelperAttribute> allAttributes, string label = null)
        {
            if(!string.IsNullOrWhiteSpace(label))
            {
                var cssClassAttribute = allAttributes.FirstOrDefault(x => x?.Name == "class");

                var exsistingCssClass = cssClassAttribute?.Value?.ToString();
                
                var newCssClassAttribute = new TagHelperAttribute(
                    "class", 
                    string.Join(" ", new [] {exsistingCssClass, label}.Where(x => !string.IsNullOrWhiteSpace(x)))                    , 
                    HtmlAttributeValueStyle.DoubleQuotes);

                if(cssClassAttribute != null)
                    cssClassAttribute = newCssClassAttribute;
                else 
                    allAttributes.Add(newCssClassAttribute);
            }

            var attributes = string.Join(
                " ",
                allAttributes
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
                        return null;

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