using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    class Catmull : IHasPointProcessor,IHasPosition
    {
        public List<OsuPixel> Points { get;  }
        public List<OsuPixel> Position { get; }
        public int Order { get; }
        public double Step { get; set; }

        public Catmull(List<OsuPixel> points)
        {
            Points = points;
            Position = new List<OsuPixel>();
            Step = 2.5 / Constants.SliderQuality;
            Order = points.Count;
            CalcPoints();
            //Console.WriteLine("Catmull");
        }

        void CalcPoints()
        {
            if (Position.Count != 0)
                throw new InvalidOperationException("Catmull was calculated twice!");
            for (int i = 0; i < Order - 1; i++)
            {
                var t = 0d;
                while (t < Step - 1)
                {
                    var p1 = i >= 1 ? Points[i - 1] : Points[i];
                    var p2 = Points[i];

                    var p3 = i + 1 < Order ? Points[i + 1] : p2.Calc(1,p2.Calc(-1,p1));

                    var p4 = i + 2 < Order ? Points[i + 2] : p2.Calc(1, p3.Calc(-1, p2));
                    var pixels = new[] {p1, p2, p3, p4}.ToList();
                    var p = VectorUtility.GetPoint(pixels, t);
                    Position.Add(p);
                    t += Step;
                }
            }
        }
        public OsuPixel PointAtDistance(double length)
        {
            switch (Order)
            {
                case 0: return null;
                case 1: return Points[0];
                default: return MathUtlity.PointAtDistance(Points, length);
            }
        }
    }
}
