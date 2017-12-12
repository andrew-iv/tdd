using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordTransformer
    {
        IEnumerable<string> Prepare(IEnumerable<string> words);
    }
}