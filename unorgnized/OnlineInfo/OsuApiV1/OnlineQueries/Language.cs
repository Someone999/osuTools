using System;

namespace osuTools.Online.ApiV1
{
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