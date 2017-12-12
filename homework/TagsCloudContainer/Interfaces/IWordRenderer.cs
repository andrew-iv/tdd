using System.Drawing;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordRenderer
    {
        Size MesureText(Graphics graphics, WordToDraw wordToDraw, WordRenderProperties properties);
        void RenderWord(Graphics graphics, Point point, WordToDraw wordToDraw, WordRenderProperties properties);
    }
}