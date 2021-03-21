using System;
using System.IO;
using System.Text;
using osuTools.ExtraMethods;
using osuTools.osuToolsException;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        ///     通过osu文件路径来构造一个Beatmap
        /// </summary>
        /// <param name="dir">osu文件路径</param>
        public Beatmap(string dir)
        {
            if (File.Exists(dir))
            {
            }
            else
            {
                throw new FileNotFoundException("指定的谱面文件不存在。");
            }

            var i = 0;
            FileName = Path.GetFileName(dir);
            FullPath = dir;
            var map = File.ReadAllLines(dir);

            if (map.Length == 0)
            {
                notv = true;
                throw new InvalidBeatmapFileException($"文件{dir}为空。");
            }

            if (!map[0].Contains("osu file format"))
            {
                notv = true;
                
                throw new InvalidBeatmapFileException($"文件{dir}不是谱面文件。");
            }
            else
            {
                StringBuilder b = new StringBuilder();
                foreach (var c in map[0])
                {
                    if (char.IsDigit(c))
                        b.Append(c);
                }
                BeatmapVersion = int.Parse(b.ToString());
            }

            foreach (var str in map)
            {
                i++;
                var temparr = str.Split(':');
                if (temparr[0].Contains("AudioFile"))
                {
                    au = temparr[1].Trim();
                    FullAudioFileName = Path.GetDirectoryName(dir) + "\\" + AudioFileName;
                    continue;
                }

                if (temparr[0].Contains("Title") && temparr[0].Length <= "Titleuni".Length)
                {
                    t = temparr[1].Trim();
                    continue;
                }

                if (temparr[0].Contains("Countdown"))
                {
                    HasCountdown = temparr[1].Trim().ToBool();
                    continue;
                }

                if (temparr[0].Contains("TitleUnicode"))
                {
                    ut = str.Replace("TitleUnicode:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Artist") && temparr[0].Length <= "Artistuni".Length)
                {
                    a = str.Replace("Artist:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("ArtistUnicode"))
                {
                    ua = str.Replace("ArtistUnicode:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Creator"))
                {
                    c = str.Replace("Creator:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Version"))
                {
                    ver = str.Replace("Version:", "").Trim();
                    dif = ver;
                    continue;
                }

                if (temparr[0].Contains("Maker"))
                {
                    mak = str.Replace("Maker:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Source"))
                {
                    sou = str.Replace("Source:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Tags"))
                {
                    tag = str.Replace("Tags:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("BeatmapId"))
                {
                    int.TryParse(temparr[1].Trim(), out id);
                    continue;
                }

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

                DownloadLink = $"http://osu.ppy.sh/b/{BeatmapID}";
                if (str.StartsWith("0,0,\"")) BackgroundFileName = str.Split(',')[2].Replace("\"", "").Trim();
                if (str.StartsWith("Video,"))
                {
                    vi = str.Split(',')[2].Replace("\"", "").Trim();
                    HasVideo = true;
                }
            }

            getAddtionalInfo(map);
            md5calc.ComputeHash(File.ReadAllBytes(dir));
            MD5 = md5calc.GetMD5String();
        }
    }
}