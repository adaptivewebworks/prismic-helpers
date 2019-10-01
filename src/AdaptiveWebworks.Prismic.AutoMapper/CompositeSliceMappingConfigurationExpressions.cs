using System;
using System.Collections.Generic;
using AutoMapper;
using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AutoMapper
{
    public static class CompositeSliceMappingConfigurationExpressions
    {
        private static void FromSlice<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            Func<WithFragments, TMember> innerMap)
                where TSource : CompositeSlice
            => opt.MapFrom(s => innerMap(s.GetPrimary()));


        public static void CompositeSliceFragments<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IDictionary<string, IFragment>> opt
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.Fragments);
        }

        public static void GetSliceFragment<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IFragment> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.Get(field));
        }
        public static void GetAllSliceFragments<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IList<IFragment>> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetAll(field));
        }

        public static void GetColorFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Color> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetColor(field));
        }

        public static void GetDateFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Date> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetDate(field));
        }

        public static void GetDateTimeFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, DateTime?> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetDate(field)?.Value);
        }

        public static void GetEmbedFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Embed> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetEmbed(field));
        }

        public static void GetGeoPointFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, GeoPoint> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetGeoPoint(field));
        }

        public static void GetGroupFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Group> opt,
            string field
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetGroup(field));
        }
        public static void GetHtmlFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field,
            DocumentLinkResolver linkResolver
        )
        where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetHtml(field, linkResolver));
        }

        public static void GetHtmlFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field,
            DocumentLinkResolver linkResolver,
            HtmlSerializer serializer
        )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetHtml(field, linkResolver, serializer));
        }

        public static void GetImageFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Image> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetImage(field));
        }

        public static void GetImageViewFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Image.View> opt,
            string field,
            string view
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetImageView(field, view));
        }

        public static void GetLinkFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, ILink> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetLink(field));
        }

        public static void GetDocumentLinkFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, DocumentLink> opt,
            string fieldName
        )
             where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetDocumentLink(fieldName));
        }


        public static void GetNumberFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Number> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetNumber(field));
        }

        public static void GetStructuredTextFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, StructuredText> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetStructuredText(field));
        }

        public static void GetTextFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetText(field));
        }
        public static void GetTimestampFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Timestamp> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetTimestamp(field));
        }

        public static void LinkedDocumentsFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IList<DocumentLink>> opt
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.LinkedDocuments());
        }

        public static void GetDocumentLinkUidFromSlice<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s => s.GetLink(field).GetDocumentLinkUid());
        }
        public static void GetLinkedFieldFromSlice<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            string field,
            Func<WithFragments, TMember> getLinkedField
            )
            where TSource : CompositeSlice
        {
            opt.FromSlice(s =>
                {
                    if (!(s.GetLink(field) is DocumentLink link))
                        return default(TMember);

                    return getLinkedField(link);
                });
        }

        // public static void GetEnum<TSource, TDestination, TMember>(
        //         this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
        //         string field,
        //         Type enumType)
        //         where TSource : CompositeSlice 
        //         => opt.FromSlice(s =>
        //             {
        //                 var stringValue = s.GetText(field)?.Replace(" ", string.Empty);
        //                 var value = enumType.IsValueType ? Activator.CreateInstance(enumType) : null;

        //                 Enum.TryParse(enumType, stringValue, out value);

        //                 return value;
        //             });*/
    }
}
