using System;

namespace osuTools.Online
{
    /// <summary>
    ///     按pp排序的成绩
    /// </summary>
    public abstract class PPSorted : IComparable<PPSorted>
    {
        public virtual double PP { get; }

        /// <summary>
        ///     与另一个PPSoted对象比较pp大小
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int CompareTo(PPSorted s)
        {
            if (PP > s.PP) return -1;
            if (PP < s.PP) return 1;
            if (PP == s.PP) return 0;
            return 0;
        }

        /// <summary>
        ///     与另一个PPSorted对象比较pp的大小
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(PPSorted a, PPSorted b)
        {
            return a.PP > b.PP;
        }

        /// <summary>
        ///     与另一个PPSorted对象比较pp的大小
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(PPSorted a, PPSorted b)
        {
            return a.PP < b.PP;
        }

        /// <summary>
        ///     根据pp判断两个PPSorted对象是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(PPSorted a, PPSorted b)
        {
            return a.PP == b.PP;
        }

        /// <summary>
        ///     根据pp判断两个PPSorted对象是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(PPSorted a, PPSorted b)
        {
            return a.PP != b.PP;
        }
    }
}