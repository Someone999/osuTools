using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 矢量缩放变化的参数
    /// </summary>
    public class VectorScaleTranslation : ITranslation
    {
        /// <summary>
        /// 使用指定的参数初始化一个VectorScaleTranslattion
        /// </summary>
        /// <param name="start">初始倍率</param>
        /// <param name="tar">目标倍率</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public VectorScaleTranslation(VectorScaleMultiplier start, VectorScaleMultiplier tar, int startTime, int endTime)
        {
            StartScaleMultiplier = start;
            TargetScaleMultiplier = tar;
            StartTime = startTime;
            EndTime = endTime;
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