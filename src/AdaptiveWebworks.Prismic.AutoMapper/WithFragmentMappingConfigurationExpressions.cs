using System;
using System.Collections.Generic;
using AutoMapper;
using prismic;
using prismic.fragments;

namespace AdaptiveWebworks.Prismic.AutoMapper
{

    public static class WithFragmentMappingConfigurationExpressions
    {
        public static void Uid<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt
            )
            where TSource : Document
        {
            opt.MapFrom(s => s.Uid);
        }

        public static void Fragments<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IDictionary<string, Fragment>> opt
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.Fragments);
        }

        public static void Get<TDestination>(
            this IMemberConfigurationExpression<Document, TDestination, Fragment> opt,
            string field
        )
        => opt.MapFrom(s => s.Get($"{s.Type}field"));

        private static void Get<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Fragment> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.Get(field));
        }
        public static void GetAll<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IList<Fragment>> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetAll(field));
        }

        public static void GetColor<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Color> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetColor(field));
        }

        public static void GetDate<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Date> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetDate(field));
        }

        public static void GetDateTime<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, DateTime?> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.ResolveUsing(s => s.GetDate(field)?.Value);
        }

        public static void GetEmbed<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Embed> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetEmbed(field));
        }

        public static void GetGeoPoint<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, GeoPoint> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.ResolveUsing(s => s.GetGeoPoint(field));
        }

        public static void GetGroup<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Group> opt,
            string field
        )
            where TSource : WithFragments
        {
            opt.ResolveUsing(s => s.GetGroup(field));
        }
        public static void GetHtml<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field,
            DocumentLinkResolver linkResolver
        )
        where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetHtml(field, linkResolver));
        }

        public static void GetHtml<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field,
            DocumentLinkResolver linkResolver,
            HtmlSerializer serializer
        )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetHtml(field, linkResolver, serializer));
        }

        public static void GetImage<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Image> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetImage(field));
        }

        public static void GetImageView<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Image.View> opt,
            string field,
            string view
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetImageView(field, view));
        }

        public static void GetLink<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Link> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetLink(field));
        }

        public static void GetDocumentLink<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, DocumentLink> opt,
            string fieldName
        )
             where TSource : WithFragments
        {
            opt.ResolveUsing(s => s.GetDocumentLink(fieldName));
        }

        public static void GetNumber<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Number> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetNumber(field));
        }

        public static void GetSliceZone<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, SliceZone> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetSliceZone(field));
        }

        public static void GetStructuredText<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, StructuredText> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetStructuredText(field));
        }

        public static void GetText<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetText(field));
        }
        public static void GetTimestamp<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, Timestamp> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetTimestamp(field));
        }

        public static void LinkedDocuments<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IList<DocumentLink>> opt
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.LinkedDocuments());
        }

        public static void GetDocumentLinkUid<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, string> opt,
            string field
            )
            where TSource : WithFragments
        {
            opt.MapFrom(s => s.GetLink(field).GetDocumentLinkUid());
        }
        public static void GetLinkedField<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            string field,
            Func<WithFragments, TMember> getLinkedField
            )
            where TSource : WithFragments
            => opt.ResolveUsing(s =>
                {
                    var link = s.GetLink(field) as DocumentLink;

                    if (link == null)
                        return default(TMember);

                    return getLinkedField(link);
                });


        // public static void GetEnum<TSource, TDestination, TMember>(
        //         this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
        //         string field,
        //         Type enumType)
        //         where TSource : WithFragments 
        //         => opt.ResolveUsing(s =>
        //             {
        //                 var stringValue = s.GetText(field)?.Replace(" ", string.Empty);
        //                 var value = enumType.IsValueType ? Activator.CreateInstance(enumType) : null;

        //                 Enum.TryParse(enumType, stringValue, out value);

        //                 return value;
        //             });
        public static void MapGroup<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            string fieldName
        )
             where TSource : WithFragments
        {
            opt.ResolveUsing(new GroupResolver<TDestination, TMember>(fieldName));
        }

        public static void MapFromSlice<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            Func<GroupDoc, TMember> map
        )
             where TSource : CompositeSlice
        {
            opt.MapFrom(s => map(s.GetPrimary()));
        }

        public static void MapFromSliceItems<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> opt,
            Func<IList<GroupDoc>, ResolutionContext, TMember> map
        )
            where TSource : CompositeSlice
        {
            opt.ResolveUsing((src, dest, member, ctx) => map(src.GetItems()?.GroupDocs, ctx));
        }

        public static void GetEnum<TSource, TDestination, TEnumMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TEnumMember> opt,
            string field)
            where TSource : WithFragments
            where TEnumMember : struct, System.Enum  
        {
            opt.ResolveUsing(s =>
            {
                var stringValue = s.GetText(field)?.Replace(" ", string.Empty);
                
                if(Enum.TryParse<TEnumMember>(stringValue, false, out TEnumMember result))
                    return result;

                return default(TEnumMember);
            });
        }
    }
}
