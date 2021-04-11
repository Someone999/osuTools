﻿using System;
using System.CodeDom;
using System.Drawing;
using System.Windows.Forms;

namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     osu!在游戏中使用的像素
    /// </summary>
    public class OsuPixel:ICloneable
    {
        /// <summary>
        ///     构造一个新OsuPixel对象
        /// </summary>
        public OsuPixel()
        {
        }

        /// <summary>
        ///     使用特定的x和y构造一个新OsuPixel对象
        /// </summary>
        /// <param name="x">指定的x值</param>
        /// <param name="y">指定的y值</param>
        public OsuPixel(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        ///     缩放前的x
        /// </summary>
        public double x { get; }

        /// <summary>
        ///     缩放前的y
        /// </summary>
        public double y { get; }

        /// <summary>
        ///     将osu!Pixel放大至屏幕大小
        /// </summary>
        /// <returns></returns>
        public Point Scale()
        {
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height - 40;
            return new Point((int) (x * (width / 640.0) - x + 320), (int) (y * (height / 480.0) - y + 240));
        }

        /// <summary>
        ///     返回描述osu!Pixel的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"x:{x} y:{y}";
        }

        /// <summary>
        ///     返回以osu!文件中格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return $"{x}:{y}";
        }
        public double Distance(OsuPixel a)
        {
            var x = this.x - a.x;
            var y = this.y - a.y;
            return Math.Sqrt(x * x + y * y);
        }

        public OsuPixel Calc(double value, OsuPixel b)
        {
            var ax = x + value * b.x;
            var ay = y + value * b.y;
            return new OsuPixel(x, y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Round(Math.Pow(x,y));
        }

        public override bool Equals(object obj)
        {
            if (obj is OsuPixel op)
            {
                return Math.Abs(op.x - x) == 0 && Math.Abs(op.y - y) == 0;
            }
            return base.Equals(obj);
        }

        public static bool operator ==(OsuPixel a, OsuPixel b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.GetHashCode() == b.GetHashCode() && a.Equals(b);
        }
        public static bool operator !=(OsuPixel a, OsuPixel b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return a.GetHashCode() != b.GetHashCode() || !a.Equals(b);
        }

        public object Clone()
        {
            return new OsuPixel(x, y);
        }
    }
}