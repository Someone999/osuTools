namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 缩放变化
    /// </summary>
    public class ScaleTranslation : ITranslation
    {
        public ScaleTranslation(ScaleMultiplier start, ScaleMultiplier target, int starttm, int endtm)
        {
            StartScaleMultiplier = start;
            TargetScaleMultiplier = target;
            StartTime = starttm;
            EndTime = endtm;
        }
        /// <summary>
        /// 起始状态
        /// </summary>
        public ScaleMultiplier StartScaleMultiplier { get; set; }
        /// <summary>
        /// 目标状态
        /// </summary>
        public ScaleMultiplier TargetScaleMultiplier { get; set; }
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
    }
}