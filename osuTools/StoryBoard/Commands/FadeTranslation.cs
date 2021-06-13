using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 透明度变化的参数
    /// </summary>
    public class FadeTranslation : ITranslation
    {
        /// <summary>
        /// 用指定的参数初始化一个FadeTranslate
        /// </summary>
        /// <param name="start">初始透明度</param>
        /// <param name="target">目标透明度</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public FadeTranslation(double start, double target, int startTime, int endTime)
        {
            StartOpacity = start;
            TargetOpacity = target;
            StartTime = startTime;
            EndTime = endTime;
        }

        /// <summary>
        ///     变化开始时的透明度。值在0-1之间
        /// </summary>
        public double StartOpacity { get; set; }

        /// <summary>
        ///     变化结束时的透明度。值在0-1之间
        /// </summary>
        public double TargetOpacity { get; set; }
        ///<inheritdoc/>
        public int StartTime { get; set; }
        ///<inheritdoc/>
        public int EndTime { get; set; }
    }
}