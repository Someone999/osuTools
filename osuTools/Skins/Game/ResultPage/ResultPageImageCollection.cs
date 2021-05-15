namespace osuTools.Skins.Game.ResultPage
{
    /// <summary>
    ///     结算页面的图片
    /// </summary>
    public class ResultPageImageCollection
    {
        /// <summary>
        ///     准确度的图片
        /// </summary>
        public GeneralSkinImage Accuracy { get; internal set; }

        /// <summary>
        ///     下方TimePerformance图的框框的图片
        /// </summary>
        public GeneralSkinImage TimePerformanceBox { get; internal set; }

        /// <summary>
        ///     数据显示的托盘
        /// </summary>
        public GeneralSkinImage Panel { get; internal set; }

        /// <summary>
        ///     达到Perfect判定显示的图片
        /// </summary>
        public GeneralSkinImage Perfect { get; internal set; }

        /// <summary>
        ///     最大连击的图片
        /// </summary>
        public GeneralSkinImage MaxCombo { get; internal set; }

        /// <summary>
        ///     Raking的图片
        /// </summary>
        public GeneralSkinImage Title { get; internal set; }

        /// <summary>
        ///     重试按钮的图片
        /// </summary>
        public GeneralSkinImage Retry { get; internal set; }

        /// <summary>
        ///     回放按钮的图片
        /// </summary>
        public GeneralSkinImage Replay { get; internal set; }
    }
}