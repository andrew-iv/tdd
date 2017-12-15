using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Services.Helpers;

namespace TagsCloudContainer.Services
{
    public static class TagCloodConverterFactory
    {
        public static ITagCloodConverter ConstructDefault()
        {
            return new TagCloodConverter(100, TagFontSizeScales.Liniar, TagColorScales.Random);
        }
    }
}