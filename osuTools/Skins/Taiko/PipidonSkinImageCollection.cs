using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Taiko
{
    /// <summary>
    ///     左侧敲鼓等动作的集合
    /// </summary>
    public class PipidonSkinImageCollection
    {
        /// <summary>
        ///     每100连击播放一次
        /// </summary>
        public List<TaikoSkinImage> PippidonClear { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     每Miss一个播放一次
        /// </summary>
        public List<TaikoSkinImage> PippidonFail { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     在非KiaiTime，连击数不是100的倍数，未连续Miss时播放
        /// </summary>
        public List<TaikoSkinImage> PippidonIdle { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     在KiaiTime，连击数不是100的倍数，未连续Miss时播放
        /// </summary>
        public List<TaikoSkinImage> PipidonKiai { get; internal set; } = new List<TaikoSkinImage>();
    }
}