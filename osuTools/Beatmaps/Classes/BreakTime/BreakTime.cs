using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Game.Interface;

namespace osuTools.Beatmaps
{
    /// <summary>
    ///     表示一个休息时间。
    /// </summary>
    public class BreakTime : IOsuFileContent, IEqualityComparer<BreakTime>
    {
        private static BreakTime empty;

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
        public static BreakTime ZeroBreakTime
        {
            get
            {
                if (empty is null) empty = new BreakTime(0, 0);
                return empty;
            }
        }

        /// <summary>
        ///     比较两个BreakTime是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(BreakTime a, BreakTime b)
        {
            return a.Start == b.Start && a.End == b.End;
        }

        /// <summary>
        ///     获取BreakTime的Hash，返回StartTime与EndTime的乘积
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(BreakTime obj)
        {
            return (int) (obj.Start * obj.End);
        }

        public string ToOsuFormat()
        {
            return $"2,{Start},{End}";
        }

        /// <summary>
        ///     将休息时间转换成字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Start} to {End}";
        }

        /// <summary>
        ///     判断给定时间是否在休息时间中
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool InBreakTime(long offset)
        {
            if (offset > Start && offset < End)
                return true;
            return false;
        }
    }

    /// <summary>
    ///     存储多个BreakTime的集合
    /// </summary>
    public class BreakTimeCollection
    {
        /// <summary>
        ///     列表中存储的BreakTime
        /// </summary>
        public List<BreakTime> BreakTimes { get; set; } = new List<BreakTime>();

        /// <summary>
        ///     BreakTime的数量
        /// </summary>
        public int Count => BreakTimes.Count;

        public BreakTime this[int index]
        {
            get => index <= BreakTimes.Count - 1
                ? BreakTimes[index]
                : throw new IndexOutOfRangeException(
                    $"[osuTools::BreaTimeCollection]Index{index}大于数组下标{BreakTimes.Count - 1}");
            set
            {
                if (index <= BreakTimes.Count - 1) BreakTimes[index] = value;
                else
                    throw new IndexOutOfRangeException(
                        $"[osuTools::BreaTimeCollection]Index{index}大于数组下标{BreakTimes.Count - 1}");
            }
        }

        /// <summary>
        ///     判断指定时间是否在列表中的任意一个BreakTime中
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool InAnyBreakTime(long offset)
        {
            foreach (var b in BreakTimes)
                if (b.InBreakTime(offset))
                    return true;
            return false;
        }

        /// <summary>
        ///     通过开始时间获取BreakTime，只返回列表中开始时间与指定时间相等的第一项
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public BreakTime GetBreakTimeByStartTime(long startTime)
        {
            return BreakTimes.Where(b => b.Start == startTime).FirstOrDefault();
        }

        /// <summary>
        ///     通过结束时间获取BreakTime，只返回列表中结束时间与指定时间相等的第一项
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public BreakTime GetBreakTimeByEndTime(long endTime)
        {
            return BreakTimes.Where(b => b.End == endTime).FirstOrDefault();
        }
    }
}