using osuTools.Beatmaps.HitObject;

namespace osuTools.StoryBoard
{
    /// <summary>
    ///     StoryBoard的图像资源
    /// </summary>
    public interface IStoryBoardImage : IStoryBoardResource
    {
        /// <summary>
        ///     图片的位置
        /// </summary>
        OsuPixel Position { get; }
    }
}