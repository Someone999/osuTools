using System;
using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    static class VectorUtility
    {
        public static OsuPixel GetPoint(List<OsuPixel> points,double length)
        {
            List<double> xVals = new List<double>(),yVals=new List<double>();
            foreach (var point in points)
            {
                xVals.Add(point.x);
                yVals.Add(point.y);
            }

            var x = MathUtlity.Catmull(xVals, length);
            var y = MathUtlity.Catmull(yVals, length);
            return new OsuPixel(x, y);
        }

        public static ValueTuple<double,double,double> GetCircumCircle(List<OsuPixel> p)
        {
            var d = 2 * (p[0].x * (p[1].y - p[2].y) + p[1].x * (p[2].y - p[0].y) + p[2].x * (p[0].y - p[1].y));
            if (d == 0)
                throw new ArgumentException("Invalid circle! Unable to chose angle.");

            var ux = ((Math.Pow(p[0].x, 2) + Math.Pow(p[0].y, 2)) * 
                      (p[1].y - p[2].y) +
                      (Math.Pow(p[1].x, 2) + Math.Pow(p[1].y, 2)) * 
                      (p[2].y - p[0].y) +
                      (Math.Pow(p[2].x, 2) + Math.Pow(p[2].y, 2)) * 
                      (p[0].y - p[1].y)) / d;
            var uy = ((Math.Pow(p[0].x, 2) + Math.Pow(p[0].y, 2)) * (p[2].x - p[1].x) +
                      (Math.Pow(p[1].x, 2) + Math.Pow(p[1].y, 2)) * (p[0].x - p[2].x) +
                      (Math.Pow(p[2].x, 2) + Math.Pow(p[2].y, 2)) * (p[1].x - p[0].x)) / d;

            var px = ux - p[0].x;
            var py = uy - p[0].y;
            var r = Math.Pow(Math.Pow(px, 2) + Math.Pow(py, 2), 0.5);
            return (ux,uy,r);
        }

        public static bool IsLeft(List<OsuPixel> p)
        {
            return ((p[1].x - p[0].x) * (p[2].y - p[0].y) - (p[1].y - p[0].y) * (p[2].x - p[0].x)) < 0;
        }
        public static OsuPixel Rotate(double cx,double cy,OsuPixel p,double radians)
        {
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);

            return new OsuPixel((cos * (p.x - cx)) - (sin * (p.y - cy)) + cx,
                (sin * (p.x - cx)) + (cos * (p.y - cy)) + cy);
        }
    }
}