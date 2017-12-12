using System;
using System.Drawing;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class WordRenderer : IWordRenderer
    {
        public Size MesureText(Graphics graphics, WordToDraw wordToDraw, WordRenderProperties properties)
        {
            var sizeF = graphics.MeasureString(
                wordToDraw.Word,
                PrepareFont(wordToDraw, properties));


            return CeilSizeF(sizeF);
        }

        private static Font PrepareFont(WordToDraw wordToDraw, WordRenderProperties properties)
        {
            return new Font(properties.FontFamily, wordToDraw.FontSize);
        }

        public void RenderWord(Graphics graphics, Point point, WordToDraw wordToDraw,
            WordRenderProperties properties)
        {
            graphics.DrawString(wordToDraw.Word, PrepareFont(wordToDraw, properties),
                new SolidBrush(wordToDraw.FontColor), point);
        }

        private static Size CeilSizeF(SizeF sizeF)
        {
            return new Size(Convert.ToInt32(Math.Ceiling(sizeF.Width)), Convert.ToInt32(Math.Ceiling(sizeF.Height)));
        }
    }
}