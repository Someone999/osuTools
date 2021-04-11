﻿using osuTools.Exceptions;

namespace osuTools.Skins.Colors
{
    /// <summary>
    ///     带有透明度的RGB颜色
    /// </summary>
    public class RGBAColor : RGBColor
    {
        /// <summary>
        ///     初始化一个rbga都为0的RGBAColor
        /// </summary>
        public RGBAColor() : base(0, 0, 0)
        {
            Alpha = 0;
        }

        /// <summary>
        ///     使用指定的rgba初始化一个RGBAColor
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="alpha"></param>
        public RGBAColor(int r, int g, int b, int alpha = 255) : base(r, g, b)
        {
            Alpha = alpha;
        }

        /// <summary>
        ///     透明度
        /// </summary>
        public int Alpha { get; }

        /// <summary>
        ///     将字符串转换成RGBAColor
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public new static RGBAColor Parse(string s)
        {
            var spliter = (char) 0;
            var c = new RGBAColor(0, 0, 0);
            foreach (var ch in s)
                if (!ch.IsDigit() && ch != ' ')
                {
                    spliter = ch;
                    break;
                }

            var vals = s.Split(spliter);
            if (vals.Length > 3)
                c = new RGBAColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]), int.Parse(vals[3]));
            else
                c = new RGBAColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
            return c;
        }
    }
}