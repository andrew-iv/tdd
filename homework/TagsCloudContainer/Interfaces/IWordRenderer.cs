using System.Drawing;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordRenderer
    {
        Size MesureText(Graphics graphics, Tag tag, WordRenderProperties properties);
        void RenderWord(Graphics graphics, Point point, Tag tag, WordRenderProperties properties);
    }
}