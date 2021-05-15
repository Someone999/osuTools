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
            public int BeatmapId => _beatmapId;

            /// <summary>
            ///     分数ID
            /// </summary>
            public int ScoreId => _scoreId;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => _score;

            /// <summary>
            ///     经权重计算后的pp
            /// </summary>
            public double WeightedPP { get; private set; }

            /// <summary>
            ///     原始pp
            /// </summary>
            public override double Pp => _pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => _maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int Count300g => _countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int Count300 => _count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int Count200 => _countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int Count100 => _count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int Count50 => _count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int CountMiss => _countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserId => _userId;

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
            public DateTime GetDate => _d;
        }

        public partial class OnlineScore
        {
            /// <summary>
            ///     分数ID
            /// </summary>
            public uint ScoreId => _scoreId;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => _score;

            public override double Pp => _pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => _maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int C300G => _countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int C300 => _count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int C200 => _countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int C100 => _count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int C50 => _count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int CMiss => _countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserId => _userId;

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
            public DateTime GetDate => _d;

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
            public int BeatmapId => _beatmapId;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => _maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int C300G => _countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int C300 => _count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int C200 => _countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int C100 => _count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int C50 => _count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int CMiss => _countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserId => _userId;

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
            public DateTime GetDate => _d;
        }
    }
}