using osuTools.Skins.Color;
using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 颜色变换的参数
    /// </summary>
    public class ColorTranslation : ITranslation
    {
        /// <summary>
        /// 使用指定的参数初始化一个ColorTranslation
        /// </summary>
        /// <param name="start">初始颜色</param>
        /// <param name="target">目标颜色</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public ColorTranslation(RgbColor start, RgbColor target, int startTime, int endTime)
        {
            StartColor = start;
            TargetColor = target;
            StartTime = startTime;
            EndTime = endTime;
        }
        /// <summary>
        /// 初始颜色
        /// </summary>
        public RgbColor StartColor { get; set; }
        /// <summary>
        /// 目标颜色
        /// </summary>
        public RgbColor TargetColor { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public int StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public int EndTime { get; set; }
    }
}