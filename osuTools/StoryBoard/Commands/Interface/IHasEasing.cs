using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     有渐变的命令
    /// </summary>
    public interface IHasEasing
    {
        /// <summary>
        ///     渐变方式
        /// </summary>
        StoryBoardEasing Easing { get; }
    }
}