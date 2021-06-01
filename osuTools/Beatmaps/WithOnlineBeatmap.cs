using osuTools.MD5Tools;
using osuTools.OnlineInfo.OsuApiV1.OnlineQueries;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        /// 使用<seealso cref="OnlineBeatmap"/>初始化Beatmap对象>
        /// </summary>
        /// <param name="olbeatmap"></param>
        public Beatmap(OnlineBeatmap olbeatmap)
        {
            Title = olbeatmap.Title;
            TitleUnicode = Title;
            Artist = olbeatmap.Artist;
            ArtistUnicode = Artist;
            Creator = olbeatmap.Creator;
            Difficulty = olbeatmap.Version;
            Version = Difficulty;
            FileName = "";
            FullPath = "";
            DownloadLink = "";
            Source = olbeatmap.Source;
            Tags = olbeatmap.Tags;
            Maker = "";
            Md5 = new MD5String(olbeatmap.Md5);
            FullAudioFileName = "";
        }
    }
}