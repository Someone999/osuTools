using osuTools.Game.Modes;

namespace osuTools.Beatmaps
{
    /// <summary>
    /// 表示一个谱面
    /// </summary>
    public interface IBeatmap
    {
        /// <summary>
        /// 谱面的标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 谱面标题的Unicode格式
        /// </summary>
        string TitleUnicode { get; }
        /// <summary>
        /// 曲目的艺术家
        /// </summary>
        string Artist { get; }
        /// <summary>
        /// 曲目艺术家的Unicode格式
        /// </summary>
        string ArtistUnicode { get; }
        /// <summary>
        /// 谱面作者
        /// </summary>
        string Creator { get; }
        /// <summary>
        /// 谱面的难度标签
        /// </summary>
        string Version { get; }
        /// <summary>
        /// 缩圈速度
        /// </summary>
        double ApproachRate { get; }
        /// <summary>
        /// 圈圈大小
        /// </summary>
        double CircleSize { get; }
        /// <summary>
        /// 掉血、回血的速度
        /// </summary>
        double HpDrain { get; }
        /// <summary>
        /// 总体难度
        /// </summary>
        double OverallDifficulty { get; }
        /// <summary>
        /// 谱面Id
        /// </summary>
        int BeatmapId { get; }
        /// <summary>
        /// 谱面集Id
        /// </summary>
        int BeatmapSetId { get; }
        /// <summary>
        /// 难度星级
        /// </summary>
        double Stars { get; }
        /// <summary>
        /// 谱面中出现次数最多的Bpm
        /// </summary>
        double Bpm { get; }
        /// <summary>
        /// 游戏模式
        /// </summary>
        OsuGameMode Mode { get; }

    }
}