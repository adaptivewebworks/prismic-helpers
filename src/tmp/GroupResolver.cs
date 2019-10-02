using AutoMapper;
using prismic;

namespace AdaptiveWebworks.Prismic.AutoMapper
{
    public class GroupResolver<TDest, TMember> : IValueResolver<WithFragments, TDest, TMember>
    {
        private readonly string _fieldName;

        public GroupResolver(string fieldName)
        {
            _fieldName = fieldName;
        }

        public TMember Resolve(WithFragments source, TDest destination, TMember destMember, ResolutionContext context)
        {
            if (source == null)
                return default(TMember);

            var group = source.GetGroup(_fieldName);

            return context.Mapper.Map<TMember>(group.GroupDocs);
        }
    }
}
