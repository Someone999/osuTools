namespace osuTools.Beatmaps
{
    /// <summary>
    ///     osu文件中的数据区域
    /// </summary>
    public enum
        DataBlock
    {
        /// <summary>
        ///     通用数据
        /// </summary>
        General,

        /// <summary>
        ///     谱面数据
        /// </summary>
        Metadata,

        /// <summary>
        ///     编辑器数据
        /// </summary>
        Editor,

        /// <summary>
        ///     难度数据
        /// </summary>
        Diffculty,

        /// <summary>
        ///     StoryBoard事件
        /// </summary>
        Event,

        /// <summary>
        ///     时间点
        /// </summary>
        TimePoints,

        /// <summary>
        ///     打击物件数据
        /// </summary>
        HitObjects,

        /// <summary>
        ///     背景
        /// </summary>
        Background,

        /// <summary>
        ///     休息时间
        /// </summary>
        BreakTime,

        /// <summary>
        ///     失败时的StoryBoard事件
        /// </summary>
        Fail,

        /// <summary>
        ///     通过时的StoryBoard事件
        /// </summary>
        Pass,

        /// <summary>
        ///     前景
        /// </summary>
        Foreground,

        /// <summary>
        ///     StoryBoard音效
        /// </summary>
        SoundSample,

        /// <summary>
        ///     未定义
        /// </summary>
        None
    }
}