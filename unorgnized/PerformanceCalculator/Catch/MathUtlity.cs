using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    public static class MathUtlity
    {
        public static double Clamp(double val, double min, double max) => val > max ? max : val < min ? min : val;
        public static int Sign(double val) => val == 0 ? 0 : val > 0 ? 1 : -1;
        public static int Combine(int m,int n)
        {
            if(m<0 || m>n)
            {
                return 0;
            }

            m = Math.Min(m, n - m);
            int output = 1;
            for (int i = 1; i < m + 1; i++)
            {
                output = output *(n - m + i) / i;
            }

            return output;
        }

        public static double Catmull(List<double> p, double t)
        {
            return 0.5 * (
                (2 * p[1]) +
                (-p[0] + p[2]) * t +
                (2 * p[0] - 5 * p[1] + 4 * p[2] - p[3]) * Math.Pow(t, 2) +
                (-p[0] + 3 * p[1] - 3 * p[2] + p[3]) * Math.Pow(t, 3));
        }
        public static OsuPixel PointAtDistance(List<OsuPixel> array, double distance)
        {
            int i = 0;
            double current_distance = 0;
            double new_distance = 0;

            if (array.Count < 2)
                return new OsuPixel(0, 0);

            if (distance == 0)
                return array[0];

            if (DistanceFromPoints(array) <= distance)
                return array[array.Count - 1];

            for (int j = 0; j < array.Count - 2; i++)
            {
                var x = (array[i].x - array[i + 1].x);
                var y = (array[i].y - array[i + 1].y);

                new_distance = Math.Sqrt(x * x + y * y);
                current_distance += new_distance;

                if (distance <= current_distance)
                    break;
            }

            current_distance -= new_distance;

            if (Math.Abs(distance - current_distance) < double.Epsilon)
                return array[i];
            else
            {
                var angle = AngleFromPoints(array[i], array[i + 1]);
                var cart = CartFromPol((distance - current_distance), angle);
                OsuPixel coord;
                coord = array[i].x > array[i + 1].x ? new OsuPixel((array[i].x - cart.x), (array[i].y - cart.y)) : new OsuPixel((array[i].x + cart.y), (array[i].y + cart.y));

                return coord;

            }
        }

        public static double AngleFromPoints(OsuPixel p0,OsuPixel p1)
        {
            return Math.Atan2(p1.y - p0.y, p1.x - p0.x);
        }

        public static OsuPixel CartFromPol(double r, double t)
        {
            var x = (r * Math.Cos(t));
            var y = (r * Math.Sin(t));
            return new OsuPixel(x, y);
        }

        public static OsuPixel PointOnLine(OsuPixel p0,OsuPixel p1,double length)
        {
            var fullLength = Math.Pow(Math.Pow(p1.x - p0.x, 2) + Math.Pow(p1.y - p0.y, 2), 0.5);
            var n = fullLength - length;

            if (fullLength == 0)
            {
                Console.WriteLine("full_length was forced to 1!");
                fullLength = 1;
            }

            var x = (n * p0.x + length * p1.x) / fullLength;
            var y = (n * p0.y + length * p1.y) / fullLength;
            //Console.WriteLine($"Before ({p0.x},{p0.y}) ({p1.x},{p1.y})");
            //Console.WriteLine($"After ({x},{y})");
            return new OsuPixel(x, y);
        }

        public static double DistanceFromPoints(List<OsuPixel> array)
        {
            double distance = 0;

            for (int i = 1; i < array.Count; i++)
                distance += array[i].Distance(array[i - 1]);

            return distance;
        }
    }
}