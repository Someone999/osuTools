using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    class Perfect:IHasPointProcessor
    {
        public List<OsuPixel> Points { get; }
        public double cx { get; set; }
        public double cy { get; set; }
        public double Radius { get; private set; }

        public Perfect(List<OsuPixel> points)
        {
            Points = points;
            SetupPath();
            //Console.WriteLine("PerfectCircle");
        }

        void SetupPath()
        {
            var tuple = VectorUtility.GetCircumCircle(Points);
            (cx, cy, Radius) = tuple;
            if (VectorUtility.IsLeft(Points))
                Radius *= -1;
        }

        public OsuPixel PointAtDistance(double length)
        {
            var radius = length / Radius;
            return VectorUtility.Rotate(cx, cy,Points[0], radius);
        }
    }
}