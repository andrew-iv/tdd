using System;
using System.Drawing;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class WordRenderer : IWordRenderer
    {
        public Size MesureText(Graphics graphics, Tag tag, WordRenderProperties properties)
        {
            var sizeF = graphics.MeasureString(
                tag.Word,
                PrepareFont(tag, properties));


            return CeilSizeF(sizeF);
        }

        private static Font PrepareFont(Tag tag, WordRenderProperties properties)
        {
            return new Font(properties.FontFamily, tag.FontSize);
        }

        public void RenderWord(Graphics graphics, Point point, Tag tag,
            WordRenderProperties properties)
        {
            graphics.DrawString(tag.Word, PrepareFont(tag, properties),
                new SolidBrush(tag.FontColor), point);
        }

        private static Size CeilSizeF(SizeF sizeF)
        {
            return new Size(Convert.ToInt32(Math.Ceiling(sizeF.Width)), Convert.ToInt32(Math.Ceiling(sizeF.Height)));
        }
    }
}