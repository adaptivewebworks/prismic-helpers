using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AutoMapper
{
    public static class WithFragmentExtensions
    {
        // TODO move to Core library...
        public static DocumentLink GetDocumentLink(this WithFragments fragment, string fieldName)
            => fragment.GetLink(fieldName) as DocumentLink;

        public static string GetDocumentLinkUid<TLink>(this TLink link)
            where TLink : Link
        {
            var docLink = link as DocumentLink;

            if (docLink == null)
                return string.Empty;

            return docLink.Uid;
        }
    }
}
