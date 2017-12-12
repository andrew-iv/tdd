using System;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudContainer.Services
{
    public class TagCloudDrawer : ITagCloudDrawer
    {
        private readonly Func<Point, ICircularCloudLayouter> _layouterFactory;
        private readonly IWordRenderer _wordRenderer;
        private readonly Func<WordRenderProperties> _propertiesFactory;

        public TagCloudDrawer(Func<Point, ICircularCloudLayouter> layouterFactory, IWordRenderer wordRenderer,
            Func<WordRenderProperties> propertiesFactory)
        {
            _layouterFactory = layouterFactory;
            _wordRenderer = wordRenderer;
            _propertiesFactory = propertiesFactory;
        }

        public Image Draw(WordToDraw[] words)
        {
            var properties = _propertiesFactory();
            var imageSize = properties.ImageSize;
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var layouter = _layouterFactory(center);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in words.OrderBy(x => x.FontColor))
            {
                RenderWord(graphics, word, properties, layouter);
            }
            return bitmap;
        }

        private void RenderWord(Graphics graphics, WordToDraw word, WordRenderProperties properties,
            ICircularCloudLayouter layouter)
        {
            var size = _wordRenderer.MesureText(graphics, word, properties);
            var rect = layouter.PutNextRectangle(size);
            _wordRenderer.RenderWord(graphics, rect.Location, word, properties);
        }
    }
}