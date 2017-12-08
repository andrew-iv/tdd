using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Draw;
using TagsCloudVisualization.Tests.Infrastructure;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        [Test]
        public void PlaceCenter_WhenSingle()
        {
            var center = new Point(20, 30);
            var layouter = new CircularCloudLayouter(center);

            var rect = layouter.PutNextRectangle(new Size(10, 12));

            Draw(new[] {rect}, center);

            new {rect.X, rect.Y, rect.Width, rect.Height}
                .ShouldBeEquivalentTo(
                    new {X = 15, Y = 24, Width = 10, Height = 12}
                );
        }

        [Test]
        public void PlaceSomeTopOrBottomCorner_WhenDoubleWide()
        {
            var center = new Point(20, 30);
            var layouter = new CircularCloudLayouter(center);

            layouter.PutNextRectangle(new Size(10, 8));
            var rect2 = layouter.PutNextRectangle(new Size(4, 3));

            (rect2.X - center.X).Should().BeOneOf(-5, 1);
            (rect2.Y - center.Y).Should().BeOneOf(-7, 4);

        }

        private static void Draw(IEnumerable<Rectangle> result, Point center)
        {
            Console.WriteLine("result: " + new CircularCloudDrawer().DrawRectangles(result, center).OutputForTest());
        }
    }
}