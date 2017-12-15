using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class TagsCloudContainer : ITagsCloudContainer
    {
        private readonly IWordListTransformer _wordListTransformer;
        private readonly ITagCloudDrawer _drawer;
        private readonly ITextSource _textSource;
        private readonly ITagCloodConverter _tagCloodConverter;

        public TagsCloudContainer(IWordListTransformer wordListTransformer, ITagCloudDrawer drawer,
            ITextSource textSource,
            ITagCloodConverter tagCloodConverter)
        {
            _wordListTransformer = wordListTransformer;
            _drawer = drawer;
            _textSource = textSource;
            _tagCloodConverter = tagCloodConverter;
        }

        public void Draw(string inputFile, string outputFilename, WordRenderProperties properties)
        {
            var readWords = _textSource.ReadWords(inputFile);
            var transformedWords = _wordListTransformer.Transform(readWords);
            var tags = _tagCloodConverter.ToTags(transformedWords).ToArray();
            var image = _drawer.Draw(tags, properties);
            image.Save(outputFilename, ImageFormat.Png);
        }
    }
}