using System.Collections.Generic;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Osu
{
    /// <summary>
    ///     通用的判定突变
    /// </summary>
    public class GeneralHitBurstImages
    {
        /// <summary>
        ///     Miss的判定图标
        /// </summary>
        public List<ISkinImage> Hit0 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     50的判定图标
        /// </summary>
        public List<ISkinImage> Hit50 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     100的判定图标
        /// </summary>
        public List<ISkinImage> Hit100 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     300的判定图标
        /// </summary>
        public List<ISkinImage> Hit300 { get; internal set; } = new List<ISkinImage>();
    }
}