using System.IO;
using System.Text;
using osuTools.Exceptions;
using osuTools.Game.Modes;
using osuTools.MD5Tools;

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

            FileName = Path.GetFileName(dir);
            FullPath = dir;
            var map = File.ReadAllLines(dir);

            if (map.Length == 0)
            {
                NotValid = true;
                throw new InvalidBeatmapFileException($"文件{dir}为空。");
            }

            if (!map[0].Contains("osu file format"))
            {
                NotValid = true;

                throw new InvalidBeatmapFileException($"文件{dir}不是谱面文件。");
            }

            StringBuilder b = new StringBuilder();
            foreach (var c in map[0])
            {
                if (char.IsDigit(c))
                    b.Append(c);
            }
            BeatmapVersion = int.Parse(b.ToString());

            foreach (var str in map)
            {
                var temparr = str.Split(':');
                if (temparr[0].Contains("AudioFile"))
                {
                    AudioFileName = temparr[1].Trim();
                    FullAudioFileName = Path.GetDirectoryName(dir) + "\\" + AudioFileName;
                    continue;
                }

                if (temparr[0].Contains("Title") && temparr[0].Length <= "Titleuni".Length)
                {
                    Title = temparr[1].Trim();
                    continue;
                }

                if (temparr[0].Contains("Countdown"))
                {
                    HasCountdown = temparr[1].Trim().ToBool();
                    continue;
                }

                if (temparr[0].Contains("TitleUnicode"))
                {
                    TitleUnicode = str.Replace("TitleUnicode:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Artist") && temparr[0].Length <= "Artistuni".Length)
                {
                    Artist = str.Replace("Artist:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("ArtistUnicode"))
                {
                    ArtistUnicode = str.Replace("ArtistUnicode:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Creator"))
                {
                    Creator = str.Replace("Creator:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Version"))
                {
                    Version = str.Replace("Version:", "").Trim();
                    Difficulty = Version;
                    continue;
                }

                if (temparr[0].Contains("Maker"))
                {
                    Maker = str.Replace("Maker:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Source"))
                {
                    Source = str.Replace("Source:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Tags"))
                {
                    Tags = str.Replace("Tags:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("BeatmapId"))
                {
                    int.TryParse(temparr[1].Trim(), out var id);
                    BeatmapId = id;
                    continue;
                }

                if (temparr[0].Contains("CircleSize"))
                {
                    double.TryParse(temparr[1].Trim(), out var c);
                    CircleSize = c;
                    continue;
                }

                if (temparr[0].Contains("OverallDifficulty"))
                {
                    double.TryParse(temparr[1].Trim(), out var o);
                    OverallDifficulty = o;
                    continue;
                }

                if (temparr[0].Contains("ApproachRate"))
                {
                    double.TryParse(temparr[1].Trim(), out var a);
                    ApproachRate = a;

                    continue;
                }

                if (temparr[0].Contains("HPDrainRate"))
                {
                    double.TryParse(temparr[1].Trim(), out var h);
                    HpDrain = h;
                    continue;
                }

                if (temparr[0].Contains("Mode"))
                {
                    if (!_modeHasSet)
                    {
                        int.TryParse(temparr[1].Trim(), out _m);
                        Mode = (OsuGameMode) _m;
                        _modeHasSet = true;
                    }

                    continue;
                }

                DownloadLink = $"http://osu.ppy.sh/b/{BeatmapId}";
                if (str.StartsWith("0,0,\"")) BackgroundFileName = str.Split(',')[2].Replace("\"", "").Trim();
                if (str.StartsWith("Video,"))
                {
                    VideoFileName = str.Split(',')[2].Replace("\"", "").Trim();
                    HasVideo = true;
                }
            }

            getAddtionalInfo(map);
            _md5Calc.ComputeHash(File.ReadAllBytes(dir));
            Md5 = _md5Calc.GetMd5String();
        }
    }
}