using osuTools.Attributes;
using osuTools.Game.Modes;
using System;
using osuTools.Game;

namespace osuTools.OrtdpWrapper
{
    /// <summary>
    /// 可以提供内存数据的数据源
    /// </summary>
    public interface IMemoryDataSource: IScoreInfo
    {
        /// <summary>
        /// 准度
        /// </summary>
        double Accuracy { get; }
        /// <summary>
        /// 玩家达到了的最大连击
        /// </summary>
        int AchievedMaxCombo { get; }
        /// <summary>
        /// 缩圈速度
        /// </summary>
        double ApproachRate { get; }
        /// <summary>
        /// 谱面艺术家
        /// </summary>
        string Artist { get; }
        /// <summary>
        /// 谱面艺术家原名
        /// </summary>
        string ArtistUnicode { get; }
        /// <summary>
        /// 音频文件名
        /// </summary>
        string AudioFileName { get; }
        /// <summary>
        /// 背景文件名
        /// </summary>
        string BackgroundFileName { get; }
        /// <summary>
        /// 谱面Id
        /// </summary>
        int BeatmapId { get; }
        /// <summary>
        /// 谱面最大连击
        /// </summary>
        int BeatmapMaxCombo { get; }
        /// <summary>
        /// 谱面集Id
        /// </summary>
        int BeatmapSetId { get; }
        /// <summary>
        /// BreakTime的数量
        /// </summary>
        int BreakTimeCount { get; }
        /// <summary>
        /// 在Hp降至0时会不会失败
        /// </summary>
        bool CanFail { get; }
        /// <summary>
        /// 圈圈大小
        /// </summary>
        double CircleSize { get; }
        /// <summary>
        /// 当前连击
        /// </summary>
        int Combo { get; }
        /// <summary>
        /// 300率
        /// </summary>
        double Count300Rate { get; }
        /// <summary>
        /// 300率的字符串形式
        /// </summary>
        string Count300RateStr { get; }
        /// <summary>
        /// Geki率
        /// </summary>
        double CountGekiRate { get; }
        /// <summary>
        /// Geki率的字符串形式
        /// </summary>
        string CountGekiRateStr { get; }   
        /// <summary>
        /// 谱面作者
        /// </summary>
        string Creator { get; }
        /// <summary>
        /// 当前Bpm
        /// </summary>
        double CurrentBpm { get; }
        /// <summary>
        /// 当前Bpm的字符串形式
        /// </summary>
        string CurrentBpmStr { get; }
        /// <summary>
        /// 当前连击
        /// </summary>
        int CurrentCombo { get; }
        /// <summary>
        /// 当前游戏模式
        /// </summary>
        GameMode CurrentMode { get; }
        /// <summary>
        /// 当前pp
        /// </summary>
        double CurrentPp { get; }
        /// <summary>
        /// 当前pp的字符串形式
        /// </summary>
        string CurrentPpStr { get; }
        /// <summary>
        /// 当前的评级
        /// </summary>
        string CurrentRank { get; }
        /// <summary>
        /// 当前的游戏时间
        /// </summary>
        TimeSpan CurrentTime { get; }
        /// <summary>
        /// 当前的游戏时间的字符串形式
        /// </summary>
        string CurrentTimeStr { get; }
        /// <summary>
        /// 是否在调试模式
        /// </summary>
        bool DebugMode { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        string Difficulty { get; }
        /// <summary>
        /// 难度倍率
        /// </summary>
        int DifficultyMultiplier { get; }
        /// <summary>
        /// 谱面下载链接
        /// </summary>
        string DownloadLink { get; }
        /// <summary>
        /// 谱面最后一个HitObject的时间
        /// </summary>
        TimeSpan DrainTime { get; }
        /// <summary>
        /// 全连时的pp
        /// </summary>
        double FcPp { get; }
        /// <summary>
        /// 全连时的pp的字符串形式
        /// </summary>
        string FcPpStr { get; }
        /// <summary>
        /// 文件名
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// 谱面的最大连击
        /// </summary>
        int FullCombo { get; }
        /// <summary>
        /// 谱面文件全路径
        /// </summary>
        string FullPath { get; }
        /// <summary>
        /// 谱面中HitObject的数量
        /// </summary>
        int HitObjectCount { get; }
        /// <summary>
        /// 时间过了的HitObject在总HitObject中的占比
        /// </summary>
        double HitObjectPercent { get; }
        /// <summary>
        /// HitObjectPercent的字符串形式
        /// </summary>
        string HitObjectPercentStr { get; }
        /// <summary>
        /// 血量，上限为200
        /// </summary>
        double Hp { get; }
        /// <summary>
        /// 掉血的速度和回血的难度
        /// </summary>
        double HpDrain { get; }
        /// <summary>
        /// 血量的字符串形式，上限为200
        /// </summary>
        string HpStr { get; }
        /// <summary>
        /// Mod名字字符串
        /// </summary>
        string ModNames { get; }
        /// <summary>
        /// Mod对分数的影响倍率
        /// </summary>
        double ModScoreMultiplier { get; }
        /// <summary>
        /// Mod短名字字符串
        /// </summary>
        string ModShortNames { get; }
        /// <summary>
        /// 当前游玩的谱面
        /// </summary>
        string NowPlaying { get; }
        /// <summary>
        /// 
        /// </summary>
        int ObjectCount { get; }
        /// <summary>
        /// 总体难度
        /// </summary>
        double OverallDifficulty { get; }
        /// <summary>
        /// 已经过了时间的HitObject
        /// </summary>
        int PassedHitObjectCount { get; }
        /// <summary>
        /// 是否达成Perfect判定
        /// </summary>
        bool Perfect { get; }

        /// <summary>
        /// 游玩次数(暂时弃用)
        /// </summary>
        [BugPresented("Incorrect.")]
        int PlayCount { get; }
        /// <summary>
        /// 玩家名
        /// </summary>
        string PlayerName { get; }
        /// <summary>
        /// 当前游玩时间，以毫秒为单位
        /// </summary>
        double PlayTime { get; }
        /// <summary>
        /// pp的百分比
        /// </summary>
        string PpPercent { get; }
        /// <summary>
        /// 在谱面选择页面计算的pp
        /// </summary>
        double PreCalculatedPp { get; }
        /// <summary>
        /// 实时星星数
        /// </summary>
        double RealTimeStars { get; }
        /// <summary>
        /// 当前BreakTime的剩余时间
        /// </summary>
        TimeSpan RemainingBreakTime { get; }
        /// <summary>
        /// 分数
        /// </summary>
        int Score { get; }
        /// <summary>
        /// 谱面总长度
        /// </summary>
        TimeSpan SongDuration { get; }
        /// <summary>
        /// 谱面来源
        /// </summary>
        string Source { get; }
        /// <summary>
        /// 谱面星星数
        /// </summary>
        double Stars { get; }
        /// <summary>
        /// 系统时间
        /// </summary>
        DateTime SysTime { get; }
        /// <summary>
        /// 谱面标签
        /// </summary>
        string Tags { get; }
        /// <summary>
        /// 游玩时间的百分比
        /// </summary>
        double TimePercent { get; }
        /// <summary>
        /// 游玩时间百分比的字符串形式
        /// </summary>
        string TimePercentStr { get; }
        /// <summary>
        /// 距离下一个BreakTime的时间
        /// </summary>
        TimeSpan TimeToNextBreakTime { get; }
        /// <summary>
        /// TimeToNextBreakTime的字符串形式
        /// </summary>
        string TimeToNextBreakTimeStr { get; }
        /// <summary>
        /// 谱面标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 谱面标题原名
        /// </summary>
        string TitleUnicode { get; }
        /// <summary>
        /// 谱面难度
        /// </summary>
        string Version { get; }
    }
}