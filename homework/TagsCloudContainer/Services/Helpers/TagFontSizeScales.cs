using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Services.Helpers
{
    public class TagFontSizeScales
    {
        public static float Liniar(CountedWord word)
        {
            const float initial = 10F;
            const float scale = 10F;
            return initial + word.Count / scale;
        }
    }
}