using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterCandidateFinderShould
    {
        [Test]
        public void PlaceInAllCorner_CheckSizes()
        {
            var results = ActResult();

            foreach (var item in results)
            {
                item.Width.Should().Be(5);
                item.Height.Should().Be(6);
            }
        }
        [Test]
        public void PlaceInAllCorner_CheckCoordinates()
        {
            var results = ActResult();
            results.Select(rect => new {rect.X, rect.Y}).ToArray().ShouldAllBeEquivalentTo(
                new[]
                {
                    new {X = -4, Y = 2, D = "TopLeftLeft"},
                    new {X = 1, Y = -4, D = "TopLeftTop"},
                    new {X = -1, Y = -4, D = "TopRightTop"},
                    new {X = 4, Y = 2, D = "TopRightRight"},

                    new {X = -4, Y = 0, D = "BottomLeftLeft"},
                    new {X = 1, Y = 6, D = "BottomLeftBottom"},
                    new {X = -1, Y = 6, D = "BottomRightBottom"},
                    new {X = 4, Y = 0, D = "BottomRightRight"}
                }, o=> o.ExcludingMissingMembers()
            );
        }

        private static Rectangle[] ActResult()
        {
            return CircularCloudLayouter.CandidateFinder
                .PlaceInCorners(new Rectangle(1, 2, 3, 4), new Size(5, 6)).ToArray();
        }
    }
}