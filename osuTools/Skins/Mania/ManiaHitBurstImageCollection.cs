using System.Collections.Generic;
using osuTools.Skins.Interfaces;
using osuTools.Skins.Osu;

namespace osuTools.Skins.Mania
{
    /// <summary>
    ///     Mania模式的判定图标
    /// </summary>
    public class ManiaHitBurstImageCollection : GeneralHitBurstImages
    {
        /// <summary>
        /// 200的判定图标
        /// </summary>
        public List<ISkinImage> Hit200 { get; internal set; } = new List<ISkinImage>();
        /// <summary>
        /// Geki的判定图标
        /// </summary>
        public List<ISkinImage> Hit300g { get; internal set; } = new List<ISkinImage>();
    }
}