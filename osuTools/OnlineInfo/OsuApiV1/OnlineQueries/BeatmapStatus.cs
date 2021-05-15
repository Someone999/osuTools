using System;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     在线查询的谱面状态
    /// </summary>
    [Serializable]
    public enum BeatmapStatus
    {
        /// <summary>
        ///     坟谱
        /// </summary>
        Graveyard = -2,

        /// <summary>
        ///     修改中
        /// </summary>
        Wip,

        /// <summary>
        ///     修改中
        /// </summary>
        Pending,

        /// <summary>
        ///     已上架，并计入分数，计入pp
        /// </summary>
        Ranked,

        /// <summary>
        ///     已上架，并计入分数，计入pp
        /// </summary>
        Approved,

        /// <summary>
        ///     已上架，并计入分数，不计入pp
        /// </summary>
        Qualified,

        /// <summary>
        ///     已上架，并计入分数，不计入pp
        /// </summary>
        Loved,

        /// <summary>
        ///     无
        /// </summary>
        None = 2048
    }
}