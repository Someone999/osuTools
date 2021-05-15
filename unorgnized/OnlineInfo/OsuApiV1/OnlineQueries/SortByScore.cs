using System;

namespace osuTools
{
    namespace Online
    {
        /// <summary>
        ///     按照分数排列的查询结果
        /// </summary>
        public abstract class SortByScore : IComparable<SortByScore>
        {
            /// <summary>
            ///     获取分数，该方法可重写
            /// </summary>
            public virtual int Score { get; } = 0;

            /// <summary>
            ///     比较分数的高低
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public int CompareTo(SortByScore s)
            {
                if (Score > s.Score) return -1;
                if (Score < s.Score) return 1;
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
                if (a is null && b is null)
                    return true;
                if (a is null || b is null)
                    return false;
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
                if (a is null && b is null)
                    return false;
                if (a is null || b is null)
                    return true;
                return a.Score != b.Score;
            }

            ///<inheritdoc/>
            public override bool Equals(object obj)
            {
                if (obj is SortByScore sort)
                    return sort.Score == Score;
                return false;
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
    }
}