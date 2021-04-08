namespace osuTools.Skins.Settings.Fonts
{
    /// <summary>
    ///     字体的设置
    /// </summary>
    public class FontSetting
    {
        /// <summary>
        ///     圈圈数字图片文件文件名的前缀
        /// </summary>
        public string HitCirclePrefix { get; internal set; } = "default";

        /// <summary>
        ///     圈圈中数字之间的间距
        /// </summary>
        public int HitCircleOverlap { get; internal set; } = -2;

        /// <summary>
        ///     分数数字图片文件文件名的前缀
        /// </summary>
        public string ScorePrefix { get; internal set; } = "score";

        /// <summary>
        ///     分数中数字之间的间距
        /// </summary>
        public int ScoreOverlap { get; internal set; } = -2;

        /// <summary>
        ///     连击数数字图片文件文件名的前缀
        /// </summary>
        public string ComboPrefix { get; internal set; } = "score";

        /// <summary>
        ///     连击数中数字之间的间距
        /// </summary>
        public int ComboOverlap { get; internal set; } = -2;
    }
}