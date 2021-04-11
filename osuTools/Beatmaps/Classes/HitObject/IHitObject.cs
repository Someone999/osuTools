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
}