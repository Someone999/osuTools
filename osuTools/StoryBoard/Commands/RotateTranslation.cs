using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 旋转的参数
    /// </summary>
    public class RotateTranslation : ITranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public RotateTranslation(Degrees start, Degrees target, int startTime, int endTime)
        {
            StartDegree = start;
            TargetDegree = target;
            StartTime = startTime;
            EndTime = endTime;
        }
        /// <summary>
        /// 初始角度
        /// </summary>
        public Degrees StartDegree { get; set; }
        /// <summary>
        /// 目标角度
        /// </summary>
        public Degrees TargetDegree { get; set; }
        ///<inheritdoc/>
        public int StartTime { get; set; }
        ///<inheritdoc/>
        public int EndTime { get; set; }
    }
}