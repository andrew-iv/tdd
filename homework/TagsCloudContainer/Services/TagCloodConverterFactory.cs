using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Services.Helpers;

namespace TagsCloudContainer.Services
{
    public static class TagCloodConverterFactory
    {
        public static ITagCloodConverter ConstructDefault(int topWords = 100)
        {
            return new TagCloodConverter(topWords, TagFontSizeScales.Liniar, TagColorScales.Random);
        }
    }
}