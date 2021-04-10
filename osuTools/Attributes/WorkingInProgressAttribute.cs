using System;
using osuTools.Exceptions;

namespace osuTools.Attributes
{
    /// <summary>
    ///     表示被标记的元素还处于开发期
    /// </summary>
    public class WorkingInProgressAttribute : Attribute
    {
        /// <summary>
        ///     使用<see cref="DevelopmentStage" />初始化一个WorkingInProgressAttribute
        /// </summary>
        /// <param name="stage">开发阶段</param>
        /// <param name="time">标记时间</param>
        public WorkingInProgressAttribute(DevelopmentStage stage, string time)
        {
            Stage = stage;
            MarkAt = time;
        }

        /// <summary>
        ///     开发阶段
        /// </summary>
        public DevelopmentStage Stage { get; }

        /// <summary>
        ///     标记的时间
        /// </summary>
        public string MarkAt { get; }
    }
}