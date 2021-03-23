using System;

namespace osuTools
{
    namespace Online
    {
        /// <summary>
        ///     代表一个使用osu!api查询的数据的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface OnlineInfo<T>
        {
        }

        /// <summary>
        ///     按照分数排列的查询结果
        /// </summary>
        public abstract class SortByScore : IComparable<SortByScore>
        {
            /// <summary>
            ///     获取分数，该方法可重写
            /// </summary>
            public virtual int Score { get; }

            /// <summary>
            ///     比较分数的高低
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public int CompareTo(SortByScore s)
            {
                if (Score > s.Score) return -1;
                if (Score < s.Score) return 1;
                if (Score == s.Score) return 0;
                return 0;
            }

            /// <summary>
            ///     比较两个<seealso cref="SortByScore" />对象的分数大小
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator >(SortByScore a, SortByScore b)
            {
                return a.Score > b.Score;
            }

            /// <summary>
            ///     比较两个<seealso cref="SortByScore" />对象的分数大小
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator <(SortByScore a, SortByScore b)
            {
                return a.Score < b.Score;
            }

            /// <summary>
            ///     指示两个<seealso cref="SortByScore" />对象的分数是否相等
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator ==(SortByScore a, SortByScore b)
            {
                return a.Score == b.Score;
            }

            /// <summary>
            ///     指示两个<seealso cref="SortByScore" />对象的分数是否相等
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator !=(SortByScore a, SortByScore b)
            {
                return a.Score != b.Score;
            }

            /// <summary>
            ///     确定指定的对象是否等于当前对象。
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            /// <summary>
            ///     根据分数获取的一个值
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return Score | (8 << 2);
            }
        }

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
}