using System.Collections.Generic;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        ///     存储最近24小时打出所有成绩
        /// </summary>
        public class OnlineRecentResultCollection : IOnlineInfo<RecentOnlineResult>
        {
            private int _p = -1;
            private int _x = 0;

            /// <summary>
            ///     指示本次查询是否成功
            /// </summary>
            public bool Failed { get; internal set; } = false;

            /// <summary>
            ///     使用整数索引从列表中获取记录
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public RecentOnlineResult this[int x] => RecentResults[x];

            /// <summary>
            ///     存储最近24小时内的记录的列表
            /// </summary>
            public List<RecentOnlineResult> RecentResults { get; } = new List<RecentOnlineResult>();

            /// <summary>
            ///     返回循环访问List的枚举数。
            /// </summary>
            /// <returns></returns>
            public IEnumerator<RecentOnlineResult> GetEnumerator()
            {
                return RecentResults.GetEnumerator();
            }
        }
    }
}