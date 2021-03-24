using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Interface;

namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     表示一个打击物件
    /// </summary>
    public interface IHitObject : IOsuFileContent
    {
        /// <summary>
        ///     打击物件的类型
        /// </summary>
        HitObjectTypes HitObjectType { get; }

        /// <summary>
        ///     打击物件相对于开始的偏移
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        ///     打击物件的音效
        /// </summary>
        HitSample HitSample { get; }

        /// <summary>
        ///     会出现该打击物件的模式
        /// </summary>
        OsuGameMode SpecifiedMode { get; }

        /// <summary>
        ///     音效的类型
        /// </summary>
        HitSounds HitSound { get; }

        /// <summary>
        ///     打击物件的位置
        /// </summary>
        OsuPixel Position { get; }

        /// <summary>
        ///     将字符串解析为IHitObject
        /// </summary>
        /// <param name="data"></param>
        void Parse(string data);
    }

    /// <summary>
    ///     表示Mania模式的HitObject
    /// </summary>
    public interface IManiaHitObject : IHitObject
    {
        /// <summary>
        ///     所在的列数
        /// </summary>
        int Column { get; set; }

        /// <summary>
        ///     谱面总列数
        /// </summary>
        int BeatmapColumn { get; set; }
    }

    /// <summary>
    ///     Note是一组一组出现的
    /// </summary>
    public interface INoteGrouped
    {
        /// <summary>
        ///     是否为一组新的HitObject的第一个HitObject
        /// </summary>
        bool IsNewGroup { get; }
    }

    /// <summary>
    ///     有结束时间的HitObject
    /// </summary>
    public interface IHasEndHitObject
    {
        /// <summary>
        ///     结束时间
        /// </summary>
        int EndTime { get; }
    }
    /// <summary>
    /// Taiko模式的HitObject
    /// </summary>
    public interface ITaikoHit : IHitObject
    {
    }
}