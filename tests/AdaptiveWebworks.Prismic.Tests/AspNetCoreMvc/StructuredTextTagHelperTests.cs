using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiveWebworks.Prismic.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using prismic;
using prismic.fragments;
using Xunit;
using static prismic.fragments.StructuredText;

namespace AdaptiveWebworks.Prismic.Tests.AspNetCoreMvc
{

    public class StructuredTextTagHelperTests
    {
        private DocumentLinkResolver linkResolver = DocumentLinkResolver.For(_ => string.Empty);

        private StructuredText Fragment = new StructuredText(new List<Block>{
            new Paragraph("Test", new List<Span>(), null),
        });

         private StructuredText FragmentWithLabel = new StructuredText(new List<Block>{
            new Paragraph("Test", new List<Span>(), "test-label"),
        });

        [Fact]
        public void StructuredTextTagHelper_throws_an_exception_when_constructor_arguments_are_not_supplied()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new StructuredTextTagHelper(null));
            Assert.Equal("linkResolver", exception.ParamName);
        }

        [Fact]
        public void StructuredTextTagHelper_does_not_output_null_fragment()
        {
            var ctx = CreateContext();
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = null
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal(string.Empty, output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }


        [Fact]
        public void StructuredTextTagHelper_outputs_content()
        {
            var ctx = CreateContext();
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = Fragment
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<p>Test</p>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        [Fact]
        public void StructuredTextTagHelper_outputs_attributes_correctly()
        {
            var ctx = CreateContext(new List<TagHelperAttribute> {
                new TagHelperAttribute("class", "test-class"),
                new TagHelperAttribute("single", "quotes", HtmlAttributeValueStyle.SingleQuotes),
                new TagHelperAttribute("no", "quotes", HtmlAttributeValueStyle.NoQuotes),
                new TagHelperAttribute("minimized", true, HtmlAttributeValueStyle.Minimized),
            });
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = Fragment
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<p class=\"test-class\" single='quotes' no=quotes minimized>Test</p>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        [Fact]
        public void StructuredTextTagHelper_outputs_multiple_blocks_wrapped_in_a_div()
        {
            var ctx = CreateContext(new List<TagHelperAttribute> { });
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = new StructuredText(new List<Block>{
                    new Paragraph("Test", new List<Span>(), null),
                    new Paragraph("Case", new List<Span>(), null),
                })
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<div><p>Test</p><p>Case</p></div>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        [Fact]
        // TODO: PR Prismic SDK to not output empty content then amend this test.
        public void StructuredTextTagHelper_does_not_outputs_elements_with_empty_body()
        {
            var ctx = CreateContext(new List<TagHelperAttribute> { });
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = new StructuredText(new List<Block>{
                    new Paragraph(string.Empty, new List<Span>(), null),
                })
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<p></p>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        [Fact]
        public void StructuredTextTagHelper_outputs_ignores_null_attributes()
        {
            var ctx = CreateContext(new List<TagHelperAttribute> { null });
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = Fragment
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<p>Test</p>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        [Fact]
        public void StructuredTextTagHelper_appends_label_to_cssClass_for_single_block_elements()
        {
            var ctx = CreateContext(new List<TagHelperAttribute> { null });
            var output = CreateOutput();

            var tagHelper = new StructuredTextTagHelper(linkResolver)
            {
                Fragment = FragmentWithLabel
            };

            tagHelper.Process(ctx, output);

            Assert.Null(output.TagName);
            Assert.Equal("<p class=\"test-label\">Test</p>", output.Content.GetContent());
            Assert.False(output.Attributes.TryGetAttribute("content", out var _));
        }

        private TagHelperOutput CreateOutput()
            => new TagHelperOutput(
                "structured-text",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) =>
                    {
                        var tagHelperContent = new DefaultTagHelperContent();
                        tagHelperContent.SetContent(string.Empty);
                        return Task.FromResult<TagHelperContent>(tagHelperContent);
                    }
                );

        private TagHelperContext CreateContext(List<TagHelperAttribute> attributes = null)
        {
            var attributeList =
                new TagHelperAttributeList(attributes ?? new List<TagHelperAttribute>());

            return new TagHelperContext(
                attributeList,
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N")
            );
        }
    }
}