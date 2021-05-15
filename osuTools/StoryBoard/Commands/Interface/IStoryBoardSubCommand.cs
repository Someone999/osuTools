using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     表示一个StroyBoard子命令
    /// </summary>
    public interface IStoryBoardSubCommand : IStoryBoardCommand
    {
        /// <summary>
        ///     事件类型
        /// </summary>
        StoryBoardEvent Command { get; }

        /// <summary>
        ///     父命令
        /// </summary>
        IStoryBoardCommand ParentCommand { get; set; }
    }
}