using osuTools.StoryBoard.Enums;
using osuTools.StoryBoard.Interfaces;

namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     表示一个StoryBoard的主命令
    /// </summary>
    public interface IStoryBoardMainCommand : IStoryBoardCommand
    {
        /// <summary>
        ///     StoryBoard资源类型
        /// </summary>
        StoryBoardResourceType ResourceType { get; }

        /// <summary>
        ///     StoryBoard资源
        /// </summary>
        IStoryBoardResource Resource { get; }
    }
}