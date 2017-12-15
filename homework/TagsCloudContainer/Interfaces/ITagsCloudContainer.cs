using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagsCloudContainer
    {
        void Draw(string inputFile, string outputFilename, WordRenderProperties properties);
    }
}