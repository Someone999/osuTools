using System;
using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 辅助功能
    /// </summary>
    public static class MathUtlity
    {
        /// <summary>
        /// 如果值大于最大值，返回最大值，小于最小值则返回最小值，否则返回原值
        /// </summary>
        /// <param name="val">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static double Clamp(double val, double min, double max) => val > max ? max : val < min ? min : val;
        /// <summary>
        /// 判断数字是正、负、或0
        /// </summary>
        /// <param name="val">要判断的数字</param>
        /// <returns>是正数返回1，是0返回0，是负数返回-1</returns>
        public static int Sign(double val) => val == 0 ? 0 : val > 0 ? 1 : -1;
        /// <summary>
        /// 求组合数
        /// </summary>
        /// <param name="m">上标</param>
        /// <param name="n">下标</param>
        /// <returns></returns>
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
        /// <summary>
        /// 意义不明，用于计算Catmull曲线的点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static double Catmull(List<double> p, double t)
        {
            return 0.5 * (
                (2 * p[1]) +
                (-p[0] + p[2]) * t +
                (2 * p[0] - 5 * p[1] + 4 * p[2] - p[3]) * Math.Pow(t, 2) +
                (-p[0] + 3 * p[1] - 3 * p[2] + p[3]) * Math.Pow(t, 3));
        }
        /// <summary>
        /// 在指定距离的点
        /// </summary>
        /// <param name="array">所有的点</param>
        /// <param name="distance">距离</param>
        /// <returns></returns>
        public static OsuPixel PointAtDistance(List<OsuPixel> array, double distance)
        {
            int i = 0;
            double currentDistance = 0;
            double newDistance = 0;

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

                newDistance = Math.Sqrt(x * x + y * y);
                currentDistance += newDistance;

                if (distance <= currentDistance)
                    break;
            }

            currentDistance -= newDistance;

            if (Math.Abs(distance - currentDistance) < double.Epsilon)
                return array[i];
            var angle = AngleFromPoints(array[i], array[i + 1]);
            var cart = CartFromPol((distance - currentDistance), angle);
            var coord = array[i].x > array[i + 1].x ? new OsuPixel((array[i].x - cart.x), (array[i].y - cart.y)) : new OsuPixel((array[i].x + cart.y), (array[i].y + cart.y));

            return coord;
        }
        /// <summary>
        /// 计算两点的夹角
        /// </summary>
        /// <param name="p0">点1</param>
        /// <param name="p1">点2</param>
        /// <returns>角度</returns>
        public static double AngleFromPoints(OsuPixel p0,OsuPixel p1)
        {
            return Math.Atan2(p1.y - p0.y, p1.x - p0.x);
        }
        /// <summary>
        /// 意义不明
        /// </summary>
        /// <param name="r"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static OsuPixel CartFromPol(double r, double t)
        {
            var x = (r * Math.Cos(t));
            var y = (r * Math.Sin(t));
            return new OsuPixel(x, y);
        }
        /// <summary>
        /// 在两点连线上的点
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="length"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 与点的距离
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double DistanceFromPoints(List<OsuPixel> array)
        {
            double distance = 0;

            for (int i = 1; i < array.Count; i++)
                distance += array[i].Distance(array[i - 1]);

            return distance;
        }
    }
}