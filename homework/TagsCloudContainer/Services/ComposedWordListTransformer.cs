using System.Collections.Generic;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class ComposedWordListTransformer: IWordListTransformer
    {
        private readonly IWordListTransformer[] _transformers;

        public ComposedWordListTransformer(params IWordListTransformer[] transformers)
        {
            _transformers = transformers;
        }

        public IEnumerable<string> Transform(IEnumerable<string> words)
        {
            foreach (var transformer in _transformers)
            {
                words = transformer.Transform(words);
            }
            return words;
        }
    }
}