using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Services.Helpers;

namespace TagsCloudContainer.Interfaces
{
    class TxtFileTextSource : ITextSource
    {
        public IEnumerable<string> ReadWords(string fileName)
        {
            return TextOperations.ExtractWords(File.ReadAllText(fileName));
        }
    }
}