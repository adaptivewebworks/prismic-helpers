using System;
using Xunit;
using AutoMapper;
using prismic;
using AdaptiveWebworks.Prismic.AutoMapper;
using System.Collections.Generic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.Tests
{
    public class AutoMapperTests
    {
        [Fact]
        public void TestConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Document, TestDestination>()
                    .ForMember(d => d.Uid, opt => opt.Uid())
                    .ForMember(d => d.Fragments, opt => opt.Fragments())
                    .ForMember(d => d.Fragment, opt => opt.Get("field"))
                    .ForMember(d => d.AllFragments, opt => opt.GetAll("field"))
                    .ForMember(d => d.Color, opt => opt.GetColor("field"))
                    .ForMember(d => d.Date, opt => opt.GetDate("field"))
                    .ForMember(d => d.DateTime, opt => opt.GetDateTime("field"))
                    .ForMember(d => d.Embed, opt => opt.GetEmbed("field"))
                    .ForMember(d => d.GeoPoint, opt => opt.GetGeoPoint("field"))
                    .ForMember(d => d.Group, opt => opt.GetGroup("field"))
                    .ForMember(d => d.Html, opt => opt.GetHtml(
                        "field",
                        DocumentLinkResolver.For((dl) => string.Empty))
                    )
                    .ForMember(d => d.Html, opt => opt.GetHtml(
                        "field",
                        DocumentLinkResolver.For((dl) => string.Empty),
                        HtmlSerializer.For((o, s) => string.Empty))
                    )
                    .ForMember(d => d.Image, opt => opt.GetImage("field"))
                    .ForMember(d => d.ImageView, opt => opt.GetImageView("field", "main"))
                    .ForMember(d => d.Link, opt => opt.GetLink("field"))
                    .ForMember(d => d.Number, opt => opt.GetNumber("field"))
                    .ForMember(d => d.SliceZone, opt => opt.GetSliceZone("field"))
                    .ForMember(d => d.StructuredText, opt => opt.GetStructuredText("field"))
                    .ForMember(d => d.Text, opt => opt.GetText("field"))
                    .ForMember(d => d.Timestamp, opt => opt.GetTimestamp("field"))
                    .ForMember(d => d.LinkedDocuments, opt => opt.LinkedDocuments())
                    .ForMember(d => d.LinkedDocumentUid, opt => opt.GetDocumentLinkUid("field"))
                    .ForMember(d => d.LinkDocumentField, opt => opt.GetLinkedField("field", x => x.GetText("linkedField")))
                    .ForMember(d => d.GroupItems, opt => opt.MapGroup("field"));
                ;

                config.CreateMap<GroupDoc, GroupItemTest>()
                    .ForMember(d => d.String, opt => opt.GetText("field"));

                config.CreateMap<CompositeSlice, TestDestination>()
                    .ForMember(d => d.Fragments, opt => opt.CompositeSliceFragments())
                    .ForMember(d => d.Fragment, opt => opt.GetSliceFragment("field"))
                    .ForMember(d => d.AllFragments, opt => opt.GetAllSliceFragments("field"))
                    .ForMember(d => d.Color, opt => opt.GetColorFromSlice("field"))
                    .ForMember(d => d.Date, opt => opt.GetDateFromSlice("field"))
                    .ForMember(d => d.DateTime, opt => opt.GetDateTimeFromSlice("field"))
                    .ForMember(d => d.Embed, opt => opt.GetEmbedFromSlice("field"))
                    .ForMember(d => d.GeoPoint, opt => opt.GetGeoPointFromSlice("field"))
                    .ForMember(d => d.Group, opt => opt.GetGroupFromSlice("field"))
                    .ForMember(d => d.Html, opt => opt.GetHtmlFromSlice(
                        "field",
                        DocumentLinkResolver.For((dl) => string.Empty))
                    )
                    .ForMember(d => d.Html, opt => opt.GetHtmlFromSlice(
                        "field",
                        DocumentLinkResolver.For((dl) => string.Empty),
                        HtmlSerializer.For((o, s) => string.Empty))
                    )
                    .ForMember(d => d.Image, opt => opt.GetImageFromSlice("field"))
                    .ForMember(d => d.ImageView, opt => opt.GetImageViewFromSlice("field", "main"))
                    .ForMember(d => d.Link, opt => opt.GetLinkFromSlice("field"))
                    .ForMember(d => d.Number, opt => opt.GetNumberFromSlice("field"))
                    .ForMember(d => d.StructuredText, opt => opt.GetStructuredTextFromSlice("field"))
                    .ForMember(d => d.Text, opt => opt.GetTextFromSlice("field"))
                    .ForMember(d => d.Timestamp, opt => opt.GetTimestampFromSlice("field"))
                    .ForMember(d => d.LinkedDocuments, opt => opt.LinkedDocumentsFromSlice())
                    .ForMember(d => d.LinkedDocumentUid, opt => opt.GetDocumentLinkUidFromSlice("field"))
                    .ForMember(d => d.LinkDocumentField, opt => opt.GetLinkedFieldFromSlice("field", x => x.GetText("linkedField")))
                    .ForMember(d => d.GroupItems, opt => opt.Ignore())
                    .ForMember(d => d.Uid, opt => opt.Ignore())
                    .ForMember(d => d.SliceZone, opt => opt.Ignore());
                ;
            });

            mapperConfiguration.AssertConfigurationIsValid();
        }

        internal class TestDestination
        {
            public string Uid { get; set; }
            public IDictionary<string, Fragment> Fragments { get; set; }
            public Fragment Fragment { get; set; }
            public IList<Fragment> AllFragments { get; set; }
            public Color Color { get; set; }
            public Date Date { get; set; }
            public DateTime? DateTime { get; set; }
            public Embed Embed { get; set; }
            public GeoPoint GeoPoint { get; set; }
            public Group Group { get; set; }
            public string Html { get; set; }
            public Image Image { get; set; }
            public Image.View ImageView { get; set; }
            public Link Link { get; set; }
            public Number Number { get; set; }
            public SliceZone SliceZone { get; set; }
            public StructuredText StructuredText { get; set; }
            public string Text { get; set; }
            public Timestamp Timestamp { get; set; }
            public IList<DocumentLink> LinkedDocuments { get; set; }
            public string LinkedDocumentUid { get; set; }
            public string LinkDocumentField { get; set; }
            public IList<GroupItemTest> GroupItems { get; set; }
        }

        public class GroupItemTest {
            public string String { get; set; }
        }
    }
}
