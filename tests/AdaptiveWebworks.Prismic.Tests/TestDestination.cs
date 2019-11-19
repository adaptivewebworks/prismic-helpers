using System;
using prismic;
using System.Collections.Generic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.Tests
{
    public class TestDestination
    {
        public string Uid { get; set; }
        public IDictionary<string, IFragment> Fragments { get; set; }
        public IFragment Fragment { get; set; }
        public IList<IFragment> AllFragments { get; set; }
        public Color Color { get; set; }
        public Date Date { get; set; }
        public DateTime? DateTime { get; set; }
        public Embed Embed { get; set; }
        public GeoPoint GeoPoint { get; set; }
        public Group Group { get; set; }
        public string Html { get; set; }
        public Image Image { get; set; }
        public Image.View ImageView { get; set; }
        public ILink Link { get; set; }
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
}
