namespace osuTools.Skins.Osu
{
    /// <summary>
    ///     转盘的皮肤图片
    /// </summary>
    public class SpinnerSkinImageCollection
    {
        /// <summary>
        ///     转盘中心的圈圈
        /// </summary>
        public OsuSkinImage SpinnerCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘的背景
        /// </summary>
        public OsuSkinImage SpinnerBackground { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘的进度
        /// </summary>
        public OsuSkinImage SpinnerMeter { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘中心圈圈的背景
        /// </summary>
        public OsuSkinImage SpinnerBottom { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘的发光
        /// </summary>
        public OsuSkinImage SpinnerGlow { get; internal set; } = new OsuSkinImage();
        /// <summary>
        /// 尚未调查清楚
        /// </summary>
        public OsuSkinImage SpinnerMiddle { get; internal set; } = new OsuSkinImage();
        /// <summary>
        /// 尚未调查清楚
        /// </summary>
        public OsuSkinImage SpinnerMiddle2 { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘的顶部
        /// </summary>
        public OsuSkinImage SpinnerTop { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转盘的外圈
        /// </summary>
        public OsuSkinImage SpinnerApproachCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转够了了之后显示的
        /// </summary>
        public OsuSkinImage SpinnerClear { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     转转盘的提示语
        /// </summary>
        public OsuSkinImage SpinnerSpin { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     每分钟旋转多少圈
        /// </summary>
        public OsuSkinImage SpinnerRPM { get; internal set; } = new OsuSkinImage();
    }
}