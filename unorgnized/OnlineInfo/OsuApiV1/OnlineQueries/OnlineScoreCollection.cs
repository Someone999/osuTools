using System.Collections.Generic;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        ///     获取一个谱面排行榜上最高100个记录。
        /// </summary>
        public class OnlineScoreCollection : IOnlineInfo<OnlineScore>
        {
            private int _enumpos = -1;
            private int _x = 0;

            public OnlineScore this[int x]
            {
                get => Scores[x];
                private set => Scores[x] = value;
            }

            /// <summary>
            ///     指示本次查询是否成功
            /// </summary>
            public bool Failed { get; } = false;

            /// <summary>
            ///     查询到的最佳成绩
            /// </summary>
            public OnlineScore BestScore { get; } = new OnlineScore();

            /// <summary>
            ///     查询到的所有成绩
            /// </summary>
            public List<OnlineScore> Scores { get; } = new List<OnlineScore>();

            /// <summary>
            ///     获取成绩列表的枚举器
            /// </summary>
            /// <returns></returns>
            public IEnumerator<OnlineScore> GetEnumerator()
            {
                return Scores.GetEnumerator();
            }
        }
    }
}