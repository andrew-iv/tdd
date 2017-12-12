using System.Drawing;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagCloudDrawer
    {
        Image Draw(WordToDraw[] words);
    }
}