using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Taiko
{
    /// <summary>
    ///     Taiko的皮肤图片元素的集合
    /// </summary>
    public class TaikoSkinImageCollection
    {
        /// <summary>
        ///     双打Note的图片
        /// </summary>
        public TaikoSkinImage TaikoBigCircle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     单打Note的图片
        /// </summary>
        public TaikoSkinImage TaikoHitCircle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的中间的小豆豆
        /// </summary>
        public TaikoSkinImage SliderScorePoint { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的条子
        /// </summary>
        public TaikoSkinImage TaikoRollMiddle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的尾部
        /// </summary>
        public TaikoSkinImage TaikoRollEnd { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     转盘的提示
        /// </summary>
        public TaikoSkinImage SpinnerWarning { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     单打的外圈
        /// </summary>
        public List<TaikoSkinImage> TaikoHitCircleOverlay { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     双打的外圈
        /// </summary>
        public List<TaikoSkinImage> TaikoBigCircleOverlay { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     左侧的敲鼓等动作
        /// </summary>
        public PipidonSkinImageCollection PippidonImages { get; internal set; } = new PipidonSkinImageCollection();

        /// <summary>
        ///     判定的图标
        /// </summary>
        public TaikoHitBurstImageCollection HitBurstImages { get; internal set; } = new TaikoHitBurstImageCollection();
    }
}