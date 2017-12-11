using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Draw
{
    public class CircularCloudDrawer
    {
        public Bitmap DrawRectangles(IEnumerable<Rectangle> rectangles, Point center)
        {
            var rectanglesArray = rectangles.ToArray();
            var result = new Bitmap(
                width: center.X * 2 + 1,
                height: center.Y * 2 + 1);

            var graphics = Graphics.FromImage(result);
            var rand = new Random(20);
            foreach (var rectangle in rectanglesArray)
            {
                var customColor =
                    Color.FromArgb(rand.Next() % 100 + 100, rand.Next() % 100 + 100, rand.Next() % 100 + 100);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                graphics.FillRectangles(shadowBrush, new[] {rectangle});
            }

            return result;
        }
    }
}