using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class ExcludeBoringWordListTransformer : IWordListTransformer
    {
        private readonly HashSet<string> _boring;

        public ExcludeBoringWordListTransformer(string[] boring)
        {
            _boring = new HashSet<string>(boring);
        }

        public IEnumerable<string> Transform(IEnumerable<string> words)
        {
            return words.Where(item => !_boring.Contains(item));
        }
    }

    [TestFixture]
    public class ExcludeBoringVocabularyTransformer_Should
    {
        private ExcludeBoringWordListTransformer _transformer;

        [SetUp]
        public void SetUp()
        {
            _transformer = new ExcludeBoringWordListTransformer(new[] {"я", "what", "when"});
        }

        [Test]
        public void NotChangedOrder_WhenMultiply()
        {
            _transformer.Transform(new[] {"aa", "aa", "я", "ЫЫ", "that", "what", "aa"}).ToArray()
                .ShouldBeEquivalentTo(new[] {"aa", "aa", "ЫЫ", "that", "aa"});
        }
    }
}