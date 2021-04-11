using System;
using System.Collections.Generic;
using System.Reflection;
using osuTools.Game.Interface;

namespace osuTools.Beatmaps
{
    /// <summary>
    ///     表示一个休息时间。
    /// </summary>
    public class BreakTime : IOsuFileContent, IEqualityComparer<BreakTime>
    {
        private static BreakTime _empty;

        /// <summary>
        ///     使用开始时间和结束时间构造一个BreakTime对象
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public BreakTime(long start, long end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        ///     开始时间，以毫秒为单位
        /// </summary>
        public long Start { get; internal set; }

        /// <summary>
        ///     结束时间，以毫秒为单位
        /// </summary>
        public long End { get; internal set; }

        /// <summary>
        ///     休息时间的间隔。
        /// </summary>
        public TimeSpan Period => TimeSpan.FromMilliseconds(End - Start);

        /// <summary>
        ///     一个开始时间和结束时间均为0的BreakTime
        /// </summary>
        public static BreakTime ZeroBreakTime => _empty ?? (_empty = new BreakTime(0, 0));

        /// <summary>
        ///     比较两个BreakTime是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(BreakTime a, BreakTime b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null)return false;
            return a.Start == b.Start && a.End == b.End;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj != null && (obj is BreakTime?Equals(this, obj):obj.Equals(this));

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        /// <summary>
        ///     获取BreakTime的Hash，返回StartTime与EndTime的乘积
        /// </summary>
        /// <param name="breakTime"></param>
        /// <returns></returns>
        public int GetHashCode(BreakTime breakTime) => (int) (breakTime.Start * breakTime.End);

        /// <inheritdoc />
        public string ToOsuFormat() => $"2,{Start},{End}";

        /// <summary>
        ///     将休息时间转换成字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Start} to {End}";

        /// <summary>
        ///     判断给定时间是否在休息时间中
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool InBreakTime(long offset) => offset > Start && offset < End;

    }
}