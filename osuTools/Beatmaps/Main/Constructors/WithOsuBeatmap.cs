using System.IO;
using osuTools.OsuDB;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        ///     使用OsuBeatmap初始化Beatmap对象
        /// </summary>
        /// <param name="beatmap"></param>
        /// <param name="getStars"></param>
        public Beatmap(OsuBeatmap beatmap, bool getStars = true)
        {
            var info = new OsuInfo();
            t = beatmap.Title;
            ut = beatmap.TitleUnicode;
            a = beatmap.Artist;
            ua = beatmap.ArtistUnicode;
            c = beatmap.Creator;
            dif = beatmap.Difficulty;
            ver = dif;
            FileName = beatmap.FileName;
            FullPath = info.BeatmapDirectory + "\\" + beatmap.FolderName + "\\" + beatmap.FileName;
            DownloadLink = $"http://osu.ppy.sh/b/{beatmap.BeatmapID}";
            sou = beatmap.Source;
            tag = beatmap.Tags;
            mak = "";
            MD5 = new MD5String(beatmap.MD5);
            FullAudioFileName = info.BeatmapDirectory + "\\" + beatmap.FolderName + "\\" + beatmap.AudioFileName;
            fuvi = "";
            od = beatmap.OD;
            hp = beatmap.HPDrain;
            ar = beatmap.AR;
            cs = beatmap.CS;
            setid = beatmap.BeatmapSetID;
            au = beatmap.AudioFileName;
            Mode = beatmap.Mode;
            if (getStars)
                double.TryParse(beatmap.Stars.ToString(), out stars);
            else stars = 0;
            if (FullPath == "" || !File.Exists(FullPath)) return;
            var alllines = File.ReadAllLines(FullPath);
            foreach (var line in alllines)
            {
                var temparr = line.Split(':');
                if (temparr[0].StartsWith("0,0,\""))
                {
                    if (string.IsNullOrEmpty(BackgroundFileName))
                        BackgroundFileName = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    continue;
                }

                if (temparr[0].StartsWith("Video,"))
                {
                    vi = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    if (!string.IsNullOrEmpty(vi))
                        HasVideo = true;
                    else
                        HasVideo = false;
                    continue;
                }

                fuvi = FullPath.Replace(FileName, vi);
                if (line.Contains("TimingPoints")) break;
            }

            SetBeatmapID(beatmap.BeatmapID);
            getAddtionalInfo(alllines);
        }
    }
}