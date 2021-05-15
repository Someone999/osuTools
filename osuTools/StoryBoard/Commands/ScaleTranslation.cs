using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 缩放的参数
    /// </summary>
    public class ScaleTranslation : ITranslation
    {
        /// <summary>
        /// 使用指定的参数初始化ScaleTranslation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="starttm"></param>
        /// <param name="endtm"></param>
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