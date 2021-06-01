using System.Collections.Generic;
using osuTools.Skins.Osu;

namespace osuTools.Skins.Taiko
{
    /// <summary>
    ///     Taiko的判定图标
    /// </summary>
    public class TaikoHitBurstImageCollection : GeneralHitBurstImages
    {
        /// <summary>
        ///     一组Note不全为良时所出现的判定
        /// </summary>
        public List<TaikoSkinImage> Hit100k { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     一组Note全为良时所出现的判定
        /// </summary>
        public List<TaikoSkinImage> Hit300k { get; internal set; } = new List<TaikoSkinImage>();
    }
}