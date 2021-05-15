using System;
using System.Collections.Generic;

namespace osuTools.Beatmaps
{
    /// <summary>
    ///     存储TimePoint的集合
    /// </summary>
    public class TimePointCollection
    {
        /// <summary>
        /// 原始列表
        /// </summary>
        public List<TimePoint> TimePoints { get; set; } = new List<TimePoint>();

        /// <summary>
        ///     平均BPM
        /// </summary>
        public double AverageBPM
        {
            get
            {
                double b = 0;
                foreach (var tmpoint in TimePoints)
                    if (tmpoint.Uninherited)
                        b += tmpoint.BPM;
                return b / TimePoints.Count;
            }
        }

        /// <summary>
        ///     TimePoint的数量
        /// </summary>
        public int Count => TimePoints.Count;

        public TimePoint this[int index]
        {
            get => index <= TimePoints.Count - 1
                ? TimePoints[index]
                : throw new IndexOutOfRangeException(
                    $"[osuTools::TimePointCollection]Index{index}大于数组下标{TimePoints.Count - 1}");
            set
            {
                if (index <= TimePoints.Count - 1) TimePoints[index] = value;
                else
                    throw new IndexOutOfRangeException(
                        $"[osuTools::TimePointCollection]Index{index}大于数组下标{TimePoints.Count - 1}");
            }
        }
    }
}