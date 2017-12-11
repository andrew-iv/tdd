using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterMetricShould
    {
        [TestCase(6, 2, 7, 4, ExpectedResult = 5)]
        [TestCase(1, 2, 2, 1, ExpectedResult = 2)]
        [TestCase(1, 2, 1, 2, ExpectedResult = 0)]
        public long EquildQuadShould(int x1, int y1, int x2, int y2)
        {
            return CircularCloudLayouter.Metrics.EuclidQuad(new Point(x1, y1), new Point(x2, y2));
        }

        public static IEnumerable<TestCaseData> EuclidQuadToFarestSource
        {
            get
            {
                yield return new TestCaseData(new Point(3, 1), new Rectangle(3, 1, 2, 1))
                {
                    ExpectedResult = 5
                };
                yield return new TestCaseData(new Point(3, 1), new Rectangle(2, 1, 2, 3))
                {
                    ExpectedResult = 10
                };
                yield return new TestCaseData(new Point(3, 1), new Rectangle(0, 0, 3 ,1))
                {
                    ExpectedResult = 10
                };
                yield return new TestCaseData(new Point(3000000, 1000000), new Rectangle(0, 0, 3000000, 1000000))
                {
                    ExpectedResult = 10000000000000L
                };
                yield return new TestCaseData(new Point(3, 1), new Rectangle(3, 1, 0, 0))
                {
                    ExpectedResult = 0
                };
            }
        }

        [TestCaseSource(nameof(EuclidQuadToFarestSource))]
        public long EuclidQuadToFarestShould(Point point, Rectangle rect)
        {
            return CircularCloudLayouter.Metrics.EuclidQuadToFarest(point, rect);
        }
    }
}