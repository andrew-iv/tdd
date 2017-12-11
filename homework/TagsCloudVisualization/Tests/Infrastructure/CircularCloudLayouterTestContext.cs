using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Draw;

namespace TagsCloudVisualization.Tests.Infrastructure
{
    internal class CircularCloudLayouterTestContext
    {
        private CircularCloudLayouter _layouter;
        public List<Rectangle> Generated { get; } = new List<Rectangle>();
        public Point Center { get; private set; }

        public CircularCloudLayouterTestContext()
        {
        }

        public void Init(Point center)
        {
            _layouter = new CircularCloudLayouter(center);
            Center = center;
        }

        public Rectangle PutNextRectangle(Size size)
        {
            var res = _layouter.PutNextRectangle(size);
            Generated.Add(res);
            return res;
        }

        public double GetQuality()
        {
            
            var maxRadiusSquare = Generated.Max(rect => CircularCloudLayouter.Metrics.EuclidQuadToFarest(Center, rect));
            var circleSquare = maxRadiusSquare * Math.PI;
            var rectsSquare = Generated.Sum(x => x.Width * x.Height);
            return circleSquare == 0 ? 0 : rectsSquare / circleSquare;
        }

        public void DumpResults()
        {
            Console.WriteLine("Tag cloud visualization saved to file: " +
                                  new CircularCloudDrawer().DrawRectangles(Generated, Center).OutputForTest());
        }

        public void DisplayQuality()
        {
            Console.WriteLine("quality: " + GetQuality().ToString("F"));
        }
    }
}