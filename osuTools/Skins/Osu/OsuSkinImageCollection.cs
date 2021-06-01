using System.Collections.Generic;

namespace osuTools.Skins.Osu
{
    /// <summary>
    ///     Osu模式的图片的集合
    /// </summary>
    public class OsuSkinImageCollection
    {
        /// <summary>
        ///     圈圈的外圈的图片
        /// </summary>
        public OsuSkinImage ApproachCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     圈圈的图片
        /// </summary>
        public OsuSkinImage HitCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     小豆豆的图片
        /// </summary>
        public OsuSkinImage FollowPoint { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     编辑器中选中的圈圈的外圈
        /// </summary>
        public OsuSkinImage HitCircleSelect { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     滑条的皮肤图片
        /// </summary>
        public SliderSkinImageCollection SliderSkinImages { get; internal set; } = new SliderSkinImageCollection();

        /// <summary>
        ///     转盘的皮肤图片
        /// </summary>
        public SpinnerSkinImageCollection SpinnerSkinImages { get; internal set; } = new SpinnerSkinImageCollection();

        /// <summary>
        ///     转盘的外圈图片
        /// </summary>
        public List<OsuSkinImage> HitCircleOverlay { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     Osu达到指定连击后显示的图片
        /// </summary>
        public OsuHitBurstImageCollection HitBurstImages { get; internal set; } = new OsuHitBurstImageCollection();
    }
}