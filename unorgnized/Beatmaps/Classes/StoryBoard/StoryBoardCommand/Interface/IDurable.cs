namespace osuTools.StoryBoard.Command
{
    /// <summary>
    ///     会持续的命令
    /// </summary>
    public interface IDurable : IHasStartTime, IHasEndTime
    {
    }
}