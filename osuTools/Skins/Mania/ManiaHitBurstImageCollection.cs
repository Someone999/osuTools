using System.Collections.Generic;
using osuTools.Skins.Interfaces;
using osuTools.Skins.SkinObjects.Osu;

namespace osuTools.Skins.SkinObjects.Mania
{
    /// <summary>
    ///     Mania模式的判定图标
    /// </summary>
    public class ManiaHitBurstImageCollection : GeneralHitBurstImages
    {
        public List<ISkinImage> Hit200 { get; internal set; } = new List<ISkinImage>();
        public List<ISkinImage> Hit300g { get; internal set; } = new List<ISkinImage>();
    }
}