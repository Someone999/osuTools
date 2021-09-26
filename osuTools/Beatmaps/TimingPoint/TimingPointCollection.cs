using System;
using System.Collections.Generic;

namespace osuTools.Beatmaps.TimingPoint
{
    /// <summary>
    ///     存储TimePoint的集合
    /// </summary>
    public class TimingPointCollection
    {
        /// <summary>
        /// 原始列表
        /// </summary>
        public List<TimingPoint> TimePoints { get; set; } = new List<TimingPoint>();

        /// <summary>
        ///     平均BPM
        /// </summary>
        public double AverageBpm
        {
            get
            {
                double b = 0;
                foreach (var tmpoint in TimePoints)
                    if (tmpoint.Uninherited)
                        b += tmpoint.Bpm;
                return b / TimePoints.Count;
            }
        }

        /// <summary>
        ///     TimePoint的数量
        /// </summary>
        public int Count => TimePoints.Count;
        /// <summary>
        /// 使用整数下标获取TimePoint
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TimingPoint this[int index]
        {
            get => index <= TimePoints.Count - 1
                ? TimePoints[index]
                : throw new IndexOutOfRangeException(
                    $"[osuTools::TimingPointCollection]Index{index}大于数组下标{TimePoints.Count - 1}");
            set
            {
                if (index <= TimePoints.Count - 1) TimePoints[index] = value;
                else
                    throw new IndexOutOfRangeException(
                        $"[osuTools::TimingPointCollection]Index{index}大于数组下标{TimePoints.Count - 1}");
            }
        }
    }
}