using System;
using Xunit;
using AutoMapper;
using prismic;
using AdaptiveWebworks.Prismic.AutoMapper;

namespace AdaptiveWebworks.Prismic.Tests
{
    public class ImageMappingTests
    {

        readonly Document _document;
        public ImageMappingTests()
        {
            _document = Fixtures.GetDocument("ImageFixture.json");
        }

        [Fact]
        public void MapImageView_correctly_maps()
        {
            var mapper = CreateMapper(config =>
            {
                config
                    .CreateMap<Document, TestDestination>()
                    .ForMember(d => d.ImageView, s => s.GetImageView("test.image", "main"))
                    .ForAllOtherMembers(opt => opt.Ignore());
            });
            var dest = mapper.Map<TestDestination>(_document);

            Assert.NotNull(dest.ImageView);
            Assert.Equal("https://images.prismic.io/test/test.jpg?auto=compress,format", dest.ImageView.Url);
        }

        [Fact]
        public void MapImage_correctly_maps()
        {
            var mapper = CreateMapper(config =>
            {
                config
                    .CreateMap<Document, TestDestination>()
                    .ForMember(d => d.Image, s => s.GetImage("test.image"))
                    .ForAllOtherMembers(opt => opt.Ignore());
            });

            var dest = mapper.Map<TestDestination>(_document);

            Assert.NotNull(dest.Image);
            Assert.Equal("https://images.prismic.io/test/test.jpg?auto=compress,format", dest.Image.GetView("main").Url);
        }


        private IMapper CreateMapper(Action<IMapperConfigurationExpression> action)
        {
            var config = new MapperConfiguration(action);
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }

    }
}
