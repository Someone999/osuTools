using System.IO;
using System.Text;
using RealTimePPDisplayer.Displayer;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        ///     通过OsuRTDataProvider.BeatmapInfo.Beatmap构造Beatmap对象。
        /// </summary>
        /// <param name="x"></param>
        public Beatmap(OsuRTDataProvider.BeatmapInfo.Beatmap x)
        {
            //bmap = x;

            var rt = new DisplayerBase();
            t = x.Title;
            id = x.BeatmapID;
            ut = x.TitleUnicode;
            a = x.Artist;
            ua = x.ArtistUnicode;
            c = x.Creator;
            dif = x.Difficulty;
            ver = x.Version;
            FileName = x.Filename;
            FullPath = x.FilenameFull;
            DownloadLink = x.DownloadLink;
            BackgroundFileName = x.BackgroundFilename;
            id = x.BeatmapID;
            vi = x.VideoFilename;
            sou = "";
            tag = "";
            mak = "";
            au = x.AudioFilename;
            md5calc.ComputeHash(File.ReadAllBytes(x.FilenameFull));
            MD5 = md5calc.GetMD5String();
            stars = rt.BeatmapTuple.Stars;
            b = new StringBuilder(FullPath);
            b.Replace(FileName, VideoFileName);
            fuvi = b.ToString();
            b = new StringBuilder(FullPath);
            b.Replace(FileName, BackgroundFileName);
            FullBackgroundFileName = b.ToString();
            var alllines = File.ReadAllLines(x.FilenameFull);
            foreach (var line in alllines)
            {
                var temparr = line.Split(':');
                if (temparr[0].Contains("CircleSize"))
                {
                    double.TryParse(temparr[1].Trim(), out cs);
                    continue;
                }

                if (temparr[0].Contains("OverallDifficulty"))
                {
                    double.TryParse(temparr[1].Trim(), out od);
                    continue;
                }

                if (temparr[0].Contains("ApproachRate"))
                {
                    double.TryParse(temparr[1].Trim(), out ar);
                    continue;
                }

                if (temparr[0].Contains("HPDrainRate"))
                {
                    double.TryParse(temparr[1].Trim(), out hp);
                    continue;
                }

                if (temparr[0].Contains("Maker:"))
                {
                    mak = line.Replace("Maker:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Source:"))
                {
                    sou = line.Replace("Source:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Tags:"))
                {
                    tag = line.Replace("Tags:", "").Trim();
                    continue;
                }

                if (temparr[0].StartsWith("0,0,\""))
                {
                    if (string.IsNullOrEmpty(BackgroundFileName))
                        BackgroundFileName = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    FullBackgroundFileName = Path.Combine(BeatmapFolder, BackgroundFileName);
                    continue;
                }

                if (temparr[0].StartsWith("Video,"))
                {
                    if (!string.IsNullOrEmpty(vi))
                    {
                        vi = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                        fuvi = Path.Combine(BeatmapFolder, VideoFileName);
                        HasVideo = true;
                    }
                    else
                    {
                        HasVideo = false;
                    }

                    continue;
                }

                if (temparr[0].Contains("Mode"))
                {
                    if (!ModeHasSet)
                    {
                        int.TryParse(temparr[1].Trim(), out m);
                        Mode = (OsuGameMode) m;
                        ModeHasSet = true;
                    }

                    continue;
                }

                if (line.Contains("TimingPoints")) break;
            }

            FullAudioFileName = x.FilenameFull.Replace(x.Filename, x.AudioFilename);
            fuvi = x.FilenameFull.Replace(x.Filename, x.VideoFilename);
            getAddtionalInfo(alllines);
        }
    }
}