namespace osuTools.Skins.Settings.Slider
{
    /// <summary>
    ///     滑条的皮肤设置
    /// </summary>
    public class SliderSetting
    {
        /// <summary>
        ///     允许滑条的球着色
        /// </summary>
        public bool AllowSliderBallTint { get; internal set; } = false;

        /// <summary>
        ///     翻转滑条的球
        /// </summary>
        public bool SliderBallFlip { get; internal set; } = true;

        /// <summary>
        ///     滑条的球的帧数
        /// </summary>
        public uint SliderBallFrames { get; internal set; }

        /// <summary>
        ///     滑条的风格
        /// </summary>
        public SliderStyles SliderStyle { get; internal set; } = SliderStyles.Gradient;
    }
}