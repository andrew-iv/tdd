using System;
using System.Drawing;
using TagsCloudContainer.Dto;

namespace TagsCloudContainer.Services.Helpers
{
    public class TagColorScales
    {
        private static Random rand = new Random();
        public static Color Random(CountedWord word)
        {
            
            return Color.FromArgb(rand.Next(0x60,0xFF), rand.Next(0x60, 0xFF), rand.Next(0x60, 0xFF));
        }
    }
}