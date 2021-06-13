using osuTools.Game.Modes;

namespace osuTools.Replays
{
    /// <summary>
        ///     录像文件包含的数据
        /// </summary>
        partial class Replay
        {
            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int CountGeki => _countGeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int Count300 => _count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int CountKatu => _countKatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int Count100 => _count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int Count50 => _count50;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => _maxco;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => _countMiss;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score { get; private set; }

            /// <summary>
            ///     保存录像的osu!的版本
            /// </summary>
            public int OsuVersion { get; private set; }

            /// <summary>
            ///     准度的字符串，格式为百分比，精确到两位小数
            /// </summary>
            public string AccuracyStr { get; private set; }

            /// <summary>
            ///     准确度
            /// </summary>
            public double Accuracy { get; private set; }

            /// <summary>
            ///     游戏模式
            /// </summary>
            public OsuGameMode Mode { get; private set; }

            /// <summary>
            ///     玩家名
            /// </summary>
            public string Player { get; private set; }

            /// <summary>
            ///     录像的MD5
            /// </summary>
            public string ReplayMd5 { get; private set; }

            /// <summary>
            ///     谱面的MD5
            /// </summary>
            public string BeatmapMd5 { get; private set; }

            /// <summary>
            ///     是否达成Perfect判定
            /// </summary>
            public bool Perfect { get; private set; }
        }
}