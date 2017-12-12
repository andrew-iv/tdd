using System.Drawing;

namespace TagsCloudVisualization
{
    public static class CloudLayouterFacade
    {
        public static ICircularCloudLayouter CreateLayouter(Point center)
        {
            return new CircularCloudLayouter(center);
        }
    }
}