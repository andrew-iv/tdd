using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Services.Helpers
{
    public static class ContedWordFilter
    {
        public static IEnumerable<CountedWord> GetMostValued(this IEnumerable<CountedWord> countedWords, int takeCount)
        {
            return countedWords.OrderByDescending(x => x.Count).Take(takeCount);
        }
    }

    [TestFixture]
    public class ContedWordFilter_Should
    {
        [Test]
        public void GetMostValued()
        {
            new[]
                {
                    new CountedWord()
                    {
                        Word = "s",
                        Count = 2,
                    },
                    new CountedWord()
                    {
                        Word = "e",
                        Count = 4,
                    },
                    new CountedWord()
                    {
                        Word = "f",
                        Count = 6,
                    }
                }.GetMostValued(2
        ).ShouldBeEquivalentTo(new[]
                {
                    new CountedWord()
                    {
                        Word = "e",
                        Count = 4,
                    },
                    new CountedWord()
                    {
                        Word = "f",
                        Count = 6,
                    }
                })
        ;
    }
    }
}