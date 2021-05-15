namespace osuTools.StoryBoard.Command
{
    /// <summary>
    ///     有结束时间的命令
    /// </summary>
    public interface IHasEndTime
    {
        /// <summary>
        ///     结束时间
        /// </summary>
        int EndTime { get; }
    }
}