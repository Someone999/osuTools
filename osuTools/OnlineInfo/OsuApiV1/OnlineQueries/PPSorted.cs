using System;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     按pp排序的成绩
    /// </summary>
    public abstract class PpSorted : IComparable<PpSorted>
    {
        /// <summary>
        /// pp
        /// </summary>
        public virtual double Pp { get; }

        /// <summary>
        ///     与另一个PPSoted对象比较pp大小
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int CompareTo(PpSorted s)
        {
            if (Pp > s.Pp) return -1;
            if (Pp < s.Pp) return 1;
            return 0;
        }

        /// <summary>
        ///     与另一个PPSorted对象比较pp的大小
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(PpSorted a, PpSorted b)
        {
            return a.Pp > b.Pp;
        }

        /// <summary>
        ///     与另一个PPSorted对象比较pp的大小
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(PpSorted a, PpSorted b)
        {
            return a.Pp < b.Pp;
        }

        /// <summary>
        ///     根据pp判断两个PPSorted对象是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(PpSorted a, PpSorted b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return Math.Abs(a.Pp - b.Pp) < double.Epsilon;
        }

        /// <summary>
        ///     根据pp判断两个PPSorted对象是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(PpSorted a, PpSorted b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return Math.Abs(a.Pp - b.Pp) > double.Epsilon;
        }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is PpSorted sort)
                return Math.Abs(sort.Pp - Pp) < double.Epsilon;
            return false;
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            string ppStr = Pp.ToString("f5");
            int idx = ppStr.IndexOf('.');
            string floatPart = ppStr.Substring(idx);
            if (floatPart.StartsWith("."))
                ppStr = ppStr.Remove(0, 1);
            return (int)(Math.Floor(Pp)+ int.Parse(ppStr));
        }
    }
}