using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordListTransformer
    {
        IEnumerable<string> Transform(IEnumerable<string> words);
    }
}