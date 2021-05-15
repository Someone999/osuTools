namespace osuTools.StoryBoard
{
    /// <summary>
    ///     StoryBoard的图层
    /// </summary>
    public enum StoryBoardLayer
    {
        /// <summary>
        ///     背景
        /// </summary>
        Background,

        /// <summary>
        ///     失败时
        /// </summary>
        Fail,

        /// <summary>
        ///     打过时
        /// </summary>
        Pass,

        /// <summary>
        ///     前景
        /// </summary>
        Foreground,

        /// <summary>
        ///     未指定时的初始值
        /// </summary>
        None = -1
    }
}