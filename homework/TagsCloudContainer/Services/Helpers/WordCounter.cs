using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Services.Helpers
{
    public static class WordCounter
    {
        public static CountedWord[] CountWords(this IEnumerable<string> wordList)
        {
            return wordList.GroupBy(s => s,StringComparer.Ordinal).Select(gr => new CountedWord()
            {
                Word = gr.Key,
                Count = gr.Count()
            }).ToArray();
        }
    }

    [TestFixture]
    public class WordCounter_Should
    {
        [Test]
        public void CountSameWordsFromGeneratedStrings()
        {
            "что что EEE что".Split(' ').CountWords().ShouldBeEquivalentTo(new []
            {
                new CountedWord()
                {
                    Count = 3,
                    Word = "что"
                },
                new CountedWord()
                {
                    Count = 1,
                    Word = "EEE"
                }
            });

        }
    }

}