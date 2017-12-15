using System.Collections.Generic;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagCloodConverter
    {
        IEnumerable<Tag> ToTags(IEnumerable<string> wordList);
    }
}