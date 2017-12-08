using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        internal static class Metrics
        {
            public static long EuclidQuadToFarest(Point center, Rectangle rect)
            {
                return new[]
                    {
                        EuclidQuad(center, new Point(rect.Left, rect.Bottom)),
                        EuclidQuad(center, new Point(rect.Right, rect.Bottom)),
                        EuclidQuad(center, new Point(rect.Left, rect.Top)),
                        EuclidQuad(center, new Point(rect.Right, rect.Top))
                    }
                    .Max();
            }

            public static long EuclidQuad(Point center, Point corner)
            {
                return ((long) center.X - corner.X) * (center.X - corner.X) +
                       ((long) center.Y - corner.Y) * (center.Y - corner.Y);
            }
        }

        internal static class CandidateFinder
        {
            /// <returns>Список прямоугольников, расположенных в смежных углах по часовой стрелки, начиная от левого верхнего</returns>
            public static IEnumerable<Rectangle> PlaceInCorners(Rectangle source, Size toPlace)
            {
                yield return new Rectangle(new Point(source.Left - toPlace.Width, source.Top), toPlace);
                yield return new Rectangle(new Point(source.Left, source.Top - toPlace.Height), toPlace);
                yield return new Rectangle(new Point(source.Right, source.Top), toPlace);
                yield return new Rectangle(new Point(source.Right - toPlace.Width, source.Top - toPlace.Height),
                    toPlace);

                yield return new Rectangle(new Point(source.Left - toPlace.Width, source.Bottom - toPlace.Height),
                    toPlace);
                yield return new Rectangle(new Point(source.Left, source.Bottom), toPlace);
                yield return new Rectangle(new Point(source.Right, source.Bottom - toPlace.Height), toPlace);
                yield return new Rectangle(new Point(source.Right - toPlace.Width, source.Bottom), toPlace);
            }
        }

        private readonly Point _center;
        private readonly List<Rectangle> _placedRectangles = new List<Rectangle>();

        public CircularCloudLayouter(Point center)
        {
            _center = center;
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (_placedRectangles.Count == 0)
            {
                return AddToPlaced(CreateFirst(size));
            }
            return AddToPlaced(FindBestCandidate(size));
        }

        private Rectangle FindBestCandidate(Size size)
        {
            return ExcludeIntersections(FindRawCandidates(size))
                .MinBy(rect => Metrics.EuclidQuadToFarest(_center, rect));
        }

        private IEnumerable<Rectangle> FindRawCandidates(Size size)
        {
            return _placedRectangles.SelectMany(placed => CandidateFinder.PlaceInCorners(placed, size));
        }

        private IEnumerable<Rectangle> ExcludeIntersections(IEnumerable<Rectangle> candidates)
        {
            return candidates.Where(candidate => !_placedRectangles.Any(placed => placed.IntersectsWith(candidate)));
        }

        private Rectangle CreateFirst(Size size)
        {
            return new Rectangle(_center.X - size.Width / 2, _center.Y - size.Height / 2, size.Width, size.Height);
        }

        private Rectangle AddToPlaced(Rectangle rectangle)
        {
            _placedRectangles.Add(rectangle);
            return rectangle;
        }
    }
}