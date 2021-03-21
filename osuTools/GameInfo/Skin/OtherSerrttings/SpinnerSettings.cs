namespace osuTools.Skins.Settings.Spinner
{
    /// <summary>
    ///     转盘的皮肤设置
    /// </summary>
    public class SpinnerSetting
    {
        /// <summary>
        ///     淡出转盘
        /// </summary>
        public bool SpinnerFadePlayfield { get; internal set; } = false;

        /// <summary>
        ///     转盘调频
        /// </summary>
        public bool SpinnerFrequencyModulate { get; internal set; } = true;

        /// <summary>
        ///     转盘不闪烁
        /// </summary>
        public bool SpinnerNoBlink { get; internal set; } = false;
    }
}