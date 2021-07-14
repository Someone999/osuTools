using System;
using System.Collections.Generic;
using System.Linq;

namespace osuTools.Beatmaps.BreakTime
{
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
        /// <summary>
        /// 获取指定索引处的BreakTime
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
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
        public bool InAnyBreakTime(long offset) => BreakTimes.Any(b => b.InBreakTime(offset));

        /// <summary>
        ///     通过开始时间获取BreakTime，只返回列表中开始时间与指定时间相等的第一项
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public BreakTime GetBreakTimeByStartTime(long startTime) => BreakTimes.FirstOrDefault(b => b.Start == startTime);

        /// <summary>
        ///     通过结束时间获取BreakTime，只返回列表中结束时间与指定时间相等的第一项
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public BreakTime GetBreakTimeByEndTime(long endTime) => BreakTimes.FirstOrDefault(b => b.End == endTime);


    }
}