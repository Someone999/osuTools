using System;

namespace osuTools
{
    namespace Online.ApiV1
    {
        partial class OnlineBestRecord
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            public int BeatmapID => beatmap_id;

            /// <summary>
            ///     分数ID
            /// </summary>
            public int ScoreID => score_id;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => score;

            /// <summary>
            ///     经权重计算后的pp
            /// </summary>
            public double WeightedPP { get; private set; }

            /// <summary>
            ///     原始pp
            /// </summary>
            public override double PP => pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;
        }

        public partial class OnlineScore
        {
            /// <summary>
            ///     分数ID
            /// </summary>
            public uint ScoreID => score_id;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => score;

            public override double PP => pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;

            /// <summary>
            ///     录像是否可用
            /// </summary>
            public bool ReplayAvailable { get; }
        }

        partial class RecentOnlineResult
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            public int BeatmapID => beatmap_id;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;
        }
    }
}