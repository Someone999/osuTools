using osuTools.Beatmaps.HitObject;
using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Interfaces
{
    /// <summary>
    ///     StoryBoard的动画资源
    /// </summary>
    public interface IStoryBoardAnimation : IStoryBoardResource
    {
        /// <summary>
        ///     动画的位置
        /// </summary>
        OsuPixel Position { get; }

        /// <summary>
        ///     动画的帧数
        /// </summary>
        double FrameCount { get; }

        /// <summary>
        ///     动画帧与帧之间的间隔
        /// </summary>
        double FrameDelay { get; }

        /// <summary>
        ///     动画的循环类型
        /// </summary>
        StoryBoardAnimationLoopType LoopType { get; }
    }
}