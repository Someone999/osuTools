namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 矢量缩放变化
    /// </summary>
    public class VectorScaleTranslation : ITranslation
    {
        public VectorScaleTranslation(VectorScaleMultiplier start, VectorScaleMultiplier tar, int sttm, int edtm)
        {
            StartScaleMultiplier = start;
            TargetScaleMultiplier = tar;
            StartTime = sttm;
            EndTime = edtm;
        }
        /// <summary>
        /// 起始状态
        /// </summary>
        public VectorScaleMultiplier StartScaleMultiplier { get; set; }
        /// <summary>
        /// 目标状态
        /// </summary>
        public VectorScaleMultiplier TargetScaleMultiplier { get; set; }
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
    }
}