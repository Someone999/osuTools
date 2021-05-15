namespace osuTools.Skins.Colors.Settings
{
    /// <summary>
    ///     皮肤的颜色设置
    /// </summary>
    public class ColorSetting
    {
        /// <summary>
        ///     连击的颜色变化
        /// </summary>
        public ComboColorCollection ComboColors { get; internal set; } = new ComboColorCollection();

        /// <summary>
        ///     输入文本颜色
        /// </summary>
        public TextColor InputOverlayText { get; internal set; } = new TextColor(0, 0, 0);

        /// <summary>
        ///     主菜单光色
        /// </summary>
        public OverlayColor MenuGlow { get; internal set; } = new OverlayColor(0, 78, 155);

        /// <summary>
        ///     滑条的球的颜色
        /// </summary>
        public RgbColor SliderBall { get; internal set; } = new RgbColor(2, 170, 255);

        /// <summary>
        ///     滑条边界的颜色
        /// </summary>
        public RgbColor SliderBorder { get; internal set; } = new RgbColor(255, 255, 255);

        /// <summary>
        ///     滑条轨迹颜色
        /// </summary>
        public RgbColor SliderTrackOverride { get; internal set; }

        /// <summary>
        ///     选歌界面动态文本颜色
        /// </summary>
        public TextColor SongSelectActiveText { get; internal set; } = new TextColor(0, 0, 0);

        /// <summary>
        ///     选歌界面静态文本颜色
        /// </summary>
        public TextColor SongSelectInactiveText { get; internal set; } = new TextColor(255, 255, 255);

        /// <summary>
        ///     转盘背景颜色
        /// </summary>
        public OverlayColor SpinnerBackground { get; internal set; } = new OverlayColor(100, 100, 100);

        /// <summary>
        ///     暂时不知道
        /// </summary>
        public OverlayColor StarBreakAdditive { get; internal set; } = new OverlayColor(255, 182, 193);
    }
}