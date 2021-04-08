using System;

namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 表示角度或弧度
    /// </summary>
    public class Degrees
    {
        /// <summary>
        /// 使用指定的参数初始化一个Degress
        /// </summary>
        /// <param name="val">角度或者弧度的值</param>
        /// <param name="isDegree">是否为角度</param>
        public Degrees(double val, bool isDegree)
        {
            if (isDegree)
            {
                Degree = val;
                Radians = Math.PI / 180 * val;
            }
            else
            {
                Radians = val;
                Degree = 180 / Math.PI * val;
            }
        }

        /// <summary>
        ///     角度
        /// </summary>
        public double Degree { get; set; }

        /// <summary>
        ///     弧度
        /// </summary>
        public double Radians { get; set; }
    }
}