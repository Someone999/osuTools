namespace osuTools.StoryBoard.Command
{
    /// <summary>
    ///     有开始时间的命令
    /// </summary>
    public interface IHasStartTime
    {
        /// <summary>
        ///     开始时间
        /// </summary>
        int StartTime { get; }
    }
}