using System.Drawing;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagCloudDrawer
    {
        Image Draw(Tag[] words, WordRenderProperties properties);
    }
}