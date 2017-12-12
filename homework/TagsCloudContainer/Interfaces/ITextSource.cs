using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ITextSource
    {
        IEnumerable<string> ReadWords(string filteName);
    }
}