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
            public int c300G => _c300g;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => _c300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => _c200;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => _c100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => _c50;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => _maxco;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => _cmiss;

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