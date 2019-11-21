using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AutoMapper
{
    public static class ImageMappingExtensions
    {
        public static Image.View MapImageView(WithFragments fragments, string field, string view)
        {
            var image = fragments.GetImage(field);

            if (image == null)
                return null;

            if (image.TryGetView(view, out var imageView))
                return imageView;

            return null;
        }
    }
}
