namespace osuTools.OnlineInfo.OsuApiV2.ResultClasses
{
    /// <summary>
    ///     各类封面图片的Url
    /// </summary>
    public class ImageUrl
    {
        /// <summary>
        ///     封面的Url
        /// </summary>
        public string Cover { get; internal set; }

        /// <summary>
        ///     高分辨率封面的Url
        /// </summary>
        public string Cover2X { get; internal set; }

        /// <summary>
        ///     高分辨率卡片预览图的Url
        /// </summary>
        public string Card { get; internal set; }

        /// <summary>
        ///     高分辨率的卡片预览图
        /// </summary>
        public string Card2X { get; internal set; }

        /// <summary>
        ///     列表预览图的Url
        /// </summary>
        public string List { get; internal set; }

        /// <summary>
        ///     高分辨率列表预览图的Url
        /// </summary>
        public string List2X { get; internal set; }

        /// <summary>
        ///     小封面的Url
        /// </summary>
        public string SlimCover { get; internal set; }

        /// <summary>
        ///     高分辨率小封面的Url
        /// </summary>
        public string SlimCover2X { get; internal set; }
    }
}