using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Osu.Slider
{
    /// <summary>
    ///     滑条皮肤的图片元素的集合
    /// </summary>
    public class SliderSkinImageCollection
    {
        /// <summary>
        ///     滑条开始时圈圈的外围
        /// </summary>
        public List<OsuSkinImage> SliderStartCircleOverlay { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     滑条结束时圈圈的外围
        /// </summary>
        public List<OsuSkinImage> SliderEndCircleOverlay { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     滑条的球的
        /// </summary>
        public List<OsuSkinImage> SliderBall { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     滑条球的有效判定范围圈的图片
        /// </summary>
        public List<OsuSkinImage> SliderFollowCircle { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     滑条开始时的圈圈
        /// </summary>

        public OsuSkinImage SliderStartCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     滑条结束时的圈圈
        /// </summary>
        public OsuSkinImage SliderEndCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     滑条的得分点的图片
        /// </summary>
        public OsuSkinImage SliderScorePoint { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     反向箭头的图片
        /// </summary>
        public OsuSkinImage ReverseArrow { get; internal set; } = new OsuSkinImage();
    }
}