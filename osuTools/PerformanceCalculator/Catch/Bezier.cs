using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    class Bezier : IHasPointProcessor, IHasPosition
    {
        public List<OsuPixel> Position { get; } = new List<OsuPixel>();
        public List<OsuPixel> Points { get; }

        public Bezier(List<OsuPixel> points)
        {
            Points = points;
            Order = Points.Count;
            CalcPoints();
            //Console.WriteLine("Bezier");
        }

        public int Order { get; }

        void CalcPoints()
        {
            if (Position.Count != 0)
                throw new InvalidOperationException("Bezier was calculated twice!");
            List<OsuPixel> subPoint = new List<OsuPixel>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == Points.Count - 1)
                {
                    subPoint.Add(Points[i]);
                    CalcBezier(subPoint);
                    subPoint.Clear();
                }

                else if (subPoint.Count > 1 && Points[i] == subPoint[subPoint.Count - 1])
                {
                    CalcBezier(subPoint);
                    subPoint.Clear();
                }
                subPoint.Add(Points[i]);
            }
        }

        void CalcBezier(List<OsuPixel> points)
        {
            var order = points.Count;
            var step = 0.25 / Constants.SliderQuality / order;
            double i = 0;
            int n = order - 1;
            while (i < 1 + step)
            {
                double x = 0, y = 0;
                for (int p = 0; p < n + 1; p++)
                {
                    var a = MathUtlity.Combine(p, n) * Math.Pow(1 - i, n - p) * Math.Pow(i, p);
                    x += a * points[p].x;
                    y += a * points[p].y;
                }

                var point = new OsuPixel(x, y);
                Position.Add(point);
                i += step;
            }
        }

        public OsuPixel PointAtDistance(double length)
        {
            switch (Order)
            {
                case 0: return null;
                case 1: return Points[0]; 
                default: return MathUtlity.PointAtDistance(Position,length);
            }
        }

    }
}