namespace osuTools.StoryBoard.Command
{
    /// <summary>
    ///     表示一个循环命令
    /// </summary>
    public interface ILoopCommand : IStoryBoardSubCommand, IHasStartTime
    {
        /// <summary>
        ///     循环次数
        /// </summary>
        int LoopCount { get; }
    }
}