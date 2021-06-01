using System.Collections.Generic;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{

        /// <summary>
        ///     存储最高PP榜指定数量的记录，最多100个。
        /// </summary>
        public class OnlineBestRecordCollection : IOnlineInfo<OnlineBestRecord>
        {
            /// <summary>
            ///     存储的记录
            /// </summary>
            public List<OnlineBestRecord> Records { get; } = new List<OnlineBestRecord>();

            /// <summary>
            ///     指示此次查询是否失败
            /// </summary>
            public bool Failed { get; internal set; }

            /// <summary>
            ///     使用整数索引从列表获取BestRecord
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public OnlineBestRecord this[int x] => Records[x];

            /// <summary>
            ///     获取成绩列表的枚举器
            /// </summary>
            /// <returns></returns>
            public IEnumerator<OnlineBestRecord> GetEnumerator()
            {
                return Records.GetEnumerator();
            }
        }
}