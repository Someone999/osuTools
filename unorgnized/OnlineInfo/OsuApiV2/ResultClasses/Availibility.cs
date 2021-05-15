namespace osuTools.OnlineInfo.OsuApiV2.ResultClasses
{
    /// <summary>
    ///     谱面能否被下载及不能被下载的原因
    /// </summary>
    public class Availibility
    {
        /// <summary>
        ///     是否能够被下载
        /// </summary>
        public bool DownloadDisabled { get; internal set; }

        /// <summary>
        ///     不能被下载的原因
        /// </summary>
        public string MoreInformation { get; internal set; } = "";
    }
}