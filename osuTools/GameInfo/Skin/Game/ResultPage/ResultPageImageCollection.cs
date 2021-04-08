namespace osuTools.Skins.SkinObjects.Generic.ResultPage
{
    /// <summary>
    ///     结算页面的图片
    /// </summary>
    public class ResultPageImageCollection
    {
        /// <summary>
        ///     准确度的图片
        /// </summary>
        public GenericSkinImage Accuracy { get; internal set; }

        /// <summary>
        ///     下方TimePerformance图的框框的图片
        /// </summary>
        public GenericSkinImage TimePerformanceBox { get; internal set; }

        /// <summary>
        ///     数据显示的托盘
        /// </summary>
        public GenericSkinImage Panel { get; internal set; }

        /// <summary>
        ///     达到Perfect判定显示的图片
        /// </summary>
        public GenericSkinImage Perfect { get; internal set; }

        /// <summary>
        ///     最大连击的图片
        /// </summary>
        public GenericSkinImage MaxCombo { get; internal set; }

        /// <summary>
        ///     Raking的图片
        /// </summary>
        public GenericSkinImage Title { get; internal set; }

        /// <summary>
        ///     重试按钮的图片
        /// </summary>
        public GenericSkinImage Retry { get; internal set; }

        /// <summary>
        ///     回放按钮的图片
        /// </summary>
        public GenericSkinImage Replay { get; internal set; }
    }
}