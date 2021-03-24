using osuTools.Online.ApiV1;

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
            t = olbeatmap.Title;
            ut = t;
            a = olbeatmap.Artist;
            ua = a;
            c = olbeatmap.Creator;
            dif = olbeatmap.Version;
            ver = dif;
            FileName = "";
            FullPath = "";
            DownloadLink = "";
            sou = olbeatmap.Source;
            tag = olbeatmap.Tags;
            mak = "";
            MD5 = new MD5String(olbeatmap.MD5);
            FullAudioFileName = "";
        }
    }
}