using System;

namespace osuTools.Online.ApiV1
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
        WIP,

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

    /// <summary>
    ///     音乐的流派
    /// </summary>
    [Serializable]
    public enum Genre
    {
        /// <summary>
        ///     任意流派
        /// </summary>
        Any,

        /// <summary>
        ///     未指定
        /// </summary>
        Unspecified,

        /// <summary>
        ///     电子游戏
        /// </summary>
        VideoGame,

        /// <summary>
        ///     动漫
        /// </summary>
        Anime,

        /// <summary>
        ///     摇滚
        /// </summary>
        Rock,

        /// <summary>
        ///     流行
        /// </summary>
        Pop,

        /// <summary>
        ///     其他
        /// </summary>
        Other,

        /// <summary>
        ///     新式风格
        /// </summary>
        Novelty,

        /// <summary>
        ///     嘻哈
        /// </summary>
        HipHop = 9,

        /// <summary>
        ///     电子乐
        /// </summary>
        Electronic
    }

    /// <summary>
    ///     歌曲的语言
    /// </summary>
    [Serializable]
    public enum Language
    {
        /// <summary>
        ///     任意语言
        /// </summary>
        Any,

        /// <summary>
        ///     其他语言
        /// </summary>
        Other,

        /// <summary>
        ///     英语
        /// </summary>
        English,

        /// <summary>
        ///     日语
        /// </summary>
        Japanese,

        /// <summary>
        ///     汉语
        /// </summary>
        Chinese,

        /// <summary>
        ///     纯音乐
        /// </summary>
        Instrumental,

        /// <summary>
        ///     韩语
        /// </summary>
        Korean,

        /// <summary>
        ///     法语
        /// </summary>
        French,

        /// <summary>
        ///     德语
        /// </summary>
        German,

        /// <summary>
        ///     瑞典语
        /// </summary>
        Swedish,

        /// <summary>
        ///     西班牙语
        /// </summary>
        Spanish,

        /// <summary>
        ///     意大利语
        /// </summary>
        Italian
    }
}