using System.Collections.Generic;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Osu
{
    /// <summary>
    ///     Osu模式的判定图标
    /// </summary>
    public class OsuHitBurstImageCollection : GeneralHitBurstImages
    {
        /// <summary>
        ///     一组Note中有不是300的判定时显示的图片
        /// </summary>
        public List<ISkinImage> Hit100k { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     一组Note中都是300的判定时显示的图片
        /// </summary>
        public List<ISkinImage> Hit300k { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     滑条中的10分的图片
        /// </summary>
        public List<ISkinImage> SliderPoint10 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     滑条中的30分的图片
        /// </summary>
        public List<ISkinImage> SliderPoint30 { get; internal set; } = new List<ISkinImage>();
    }
}