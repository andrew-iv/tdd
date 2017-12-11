 using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Draw;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudDrawerShould
    {
        [Test]
        public void WritesBitmapWithGoodSize()
        {
            var bm = DrawRectangles(new[]
            {
                new Rectangle(10, 20, 32, 50),
                new Rectangle(10, 20, 30, 52)
            }, new Point(30, 20));
            new {bm.Height, bm.Width}.ShouldBeEquivalentTo(new {Height = 41, Width = 61});
        }

        [Test]
        
        public void WritesBitmapWithDifferentColors_When2Rects()
        {
            var bitmap = DrawRectangles(new[]
            {
                new Rectangle(1, 2, 2,  3),
                new Rectangle(11, 12, 2, 3)
            }, new Point(20,30));

            new[] {bitmap.GetPixel(2, 3), bitmap.GetPixel(0, 0), bitmap.GetPixel(12, 13)}.Should()
                .OnlyHaveUniqueItems();
        }

        private static Bitmap DrawRectangles(Rectangle[] rectangles, Point center)
        {
            return new CircularCloudDrawer().DrawRectangles(rectangles, center);
        }
    }
}