using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Services.Helpers;

namespace TagsCloudContainer.Services
{
    class TagCloodConverter : ITagCloodConverter
    {
        private readonly int _topCount;
        private readonly Func<CountedWord, float> _calcEmSize;
        private readonly Func<CountedWord, Color> _calcColor;

        public TagCloodConverter(int topCount, Func<CountedWord, float> calcEmSize, Func<CountedWord, Color> calcColor)
        {
            _topCount = topCount;
            _calcEmSize = calcEmSize;
            _calcColor = calcColor;
        }

        public IEnumerable<Tag> ToTags(IEnumerable<string> wordList)
        {
            return wordList.CountWords().GetMostValued(_topCount).Select(countedWord => new Tag
            {
                FontColor = _calcColor(countedWord),
                FontSize = _calcEmSize(countedWord),
                Word = countedWord.Word
            });
        }
    }
}