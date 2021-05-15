namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     表示一个触发器命令
    /// </summary>
    public interface ITriggerCommand : IStoryBoardSubCommand, IDurable
    {
        /// <summary>
        ///     触发器类型
        /// </summary>
        string TriggerType { get; }
    }
}