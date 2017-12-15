using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Services
{
    public class NormalizeWordListTransformer : IWordListTransformer
    {
        public IEnumerable<string> Transform(IEnumerable<string> words)
        {
            return words.Select(arg => arg.ToLower());
        }
    }

    [TestFixture]
    public class NormalizeVocabularyTransformer_Should
    {
        private NormalizeWordListTransformer _transformer;

        [SetUp]
        public void SetUp()
        {
            _transformer = new NormalizeWordListTransformer();
        }

        [TestCase("abc",ExpectedResult = "abc" )]
        [TestCase("aBc", ExpectedResult = "abc")]
        [TestCase("ABC", ExpectedResult = "abc")]
        public string Converts_WhenSingle(string src)
        {
            return _transformer.Transform(new[] {src}).Single();
        }

        [Test]
        public void NotChangedOrder_WhenMultiply()
        {
            _transformer.Transform(new[] { "aa","ии","ЫЫ" }).ToArray()
                .ShouldBeEquivalentTo(new[] { "aa", "ии", "ыы" }, o=>o.WithStrictOrdering());
        }
    }
}