using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Services.Helpers
{
    public static class TextOperations
    {
        private static readonly Regex SplitWordsRegex = new Regex("[^а-яА-ЯA-Za-z]+", RegexOptions.Compiled | RegexOptions.Multiline);

        public static string[] ExtractWords(string text)
        {
            return SplitWordsRegex.Split(text).Where(x=>!String.IsNullOrWhiteSpace(x)).ToArray();
        }
    }

    [TestFixture]
    public class TextOperations_Should
    {
        [Test]
        public void ExtractFromText()
        {
            var words = TextOperations.ExtractWords("Ололо, я водитель НЛО. :\r\n я Yellow Submarine =) 123");
                words
                .ShouldBeEquivalentTo(new []
                {
                    "Ололо",
                    "я",
                    "водитель",
                    "НЛО",
                    "я",
                    "Yellow",
                    "Submarine"
                }, because: string.Join(", ", words));
        }
    }
}