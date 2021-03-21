namespace osuTools.OsuDB
{
    /// <summary>
    ///     谱面的状态
    /// </summary>
    public enum OsuBeatmapStatus
    {
        /// <summary>
        ///     未知
        /// </summary>
        Unknown,

        /// <summary>
        ///     未提交
        /// </summary>
        NotSubmitted,

        /// <summary>
        ///     未过审
        /// </summary>
        Pending,

        /// <summary>
        ///     已上架并计算分数，pp
        /// </summary>
        Ranked = 4,

        /// <summary>
        ///     已上架并计算分数，pp
        /// </summary>
        Approved,

        /// <summary>
        ///     已上架并计算分数，不计算pp
        /// </summary>
        Qualified,

        /// <summary>
        ///     已上架并计算分数，不计算pp
        /// </summary>
        Loved,

        /// <summary>
        ///     谱面正在从文件加载，与osu!制定的谱面状态无关
        /// </summary>
        Loading
    }
}