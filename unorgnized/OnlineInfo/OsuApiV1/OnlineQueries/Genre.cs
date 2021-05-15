using System;

namespace osuTools.Online.ApiV1
{
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
}