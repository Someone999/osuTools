using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 移动的参数
    /// </summary>
    public class MoveTranslation : ITranslation
    {
        /// <summary>
        /// 使用指定的参数初始化一个MoveTranslation
        /// </summary>
        /// <param name="start">初始位置</param>
        /// <param name="target">目标位置</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public MoveTranslation(StoryBoardPoint start, StoryBoardPoint target, int startTime, int endTime)
        {
            StartPoint = start;
            TargetPoint = target;
            StartTime = startTime;
            EndTime = endTime;
        }
        /// <summary>
        /// 初始位置
        /// </summary>
        public StoryBoardPoint StartPoint { get; set; }
        /// <summary>
        /// 目标位置
        /// </summary>
        public StoryBoardPoint TargetPoint { get; set; }
        ///<inheritdoc/>
        public int StartTime { get; set; }
        ///<inheritdoc/>
        public int EndTime { get; set; }
    }
}