using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 在X轴移动的参数
    /// </summary>
    public class MoveXTranslation:ITranslation
    {
        /// <summary>
        /// 使用指定的参数初始化MoveXTranslation
        /// </summary>
        /// <param name="start">初始位置</param>
        /// <param name="target">目标位置</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public MoveXTranslation(double start, double target, int startTime, int endTime)
        {
            StartPoint = start;
            TargetPoint = target;
            StartTime = startTime;
            EndTime = endTime;
        }
        /// <summary>
        /// 初始位置
        /// </summary>
        public double StartPoint { get; set; }
        /// <summary>
        /// 目标位置
        /// </summary>
        public double TargetPoint { get; set; }
        ///<inheritdoc/>
        public int StartTime { get; set; }
        ///<inheritdoc/>
        public int EndTime { get; set; }
    }
}