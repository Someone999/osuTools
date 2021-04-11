using osuTools.Replays;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        ///     在指定的文件夹搜索该谱面的录像。
        /// </summary>
        /// <param name="replyfolder">要搜索的文件夹</param>
        /// <returns>返回一个存储录像信息的类的数组</returns>
        public ReplayCollection GetReplaysForBeatmap(string replyfolder = "")
        {
            var info = new OsuInfo();
            var r = new ReplayCollection();
            if (replyfolder == "")
                replyfolder = info.OsuDirectory + "\\Replays";
            var replays = ReplayCollection.GetAllReplays(replyfolder);
            foreach (var replay in replays)
                if (replay.BeatmapMd5 == MD5.ToString())
                    r.Add(replay);
            return r;
        }
    }
}