﻿using System.IO;
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
            Title = x.Title;
            BeatmapId = x.BeatmapID;
            TitleUnicode = x.TitleUnicode;
            Artist = x.Artist;
            ArtistUnicode = x.ArtistUnicode;
            Creator = x.Creator;
            Difficulty = x.Difficulty;
            Version = x.Version;
            FileName = x.Filename;
            FullPath = x.FilenameFull;
            DownloadLink = x.DownloadLink;
            BackgroundFileName = x.BackgroundFilename;
            BeatmapId = x.BeatmapID;
            VideoFileName = x.VideoFilename;
            Source = "";
            Tags = "";
            Maker = "";
            AudioFileName = x.AudioFilename;
            _md5Calc.ComputeHash(File.ReadAllBytes(x.FilenameFull));
            MD5 = _md5Calc.GetMD5String();
            Stars = rt.BeatmapTuple.Stars;
            _b = new StringBuilder(FullPath);
            _b.Replace(FileName, VideoFileName);
            FullVideoFileName = _b.ToString();
            _b = new StringBuilder(FullPath);
            _b.Replace(FileName, BackgroundFileName);
            FullBackgroundFileName = _b.ToString();
            var alllines = File.ReadAllLines(x.FilenameFull);
            foreach (var line in alllines)
            {
                var temparr = line.Split(':');
                if (temparr[0].Contains("CircleSize"))
                {
                    double.TryParse(temparr[1].Trim(), out var c);
                    CS = c;
                    continue;
                }

                if (temparr[0].Contains("OverallDifficulty"))
                {
                    double.TryParse(temparr[1].Trim(), out var o);
                    OD = o;
                    continue;
                }

                if (temparr[0].Contains("ApproachRate"))
                {
                    double.TryParse(temparr[1].Trim(), out var a);
                    AR = a;
                    continue;
                }

                if (temparr[0].Contains("HPDrainRate"))
                {
                    double.TryParse(temparr[1].Trim(), out var h);
                    HP = h;
                    continue;
                }

                if (temparr[0].Contains("Maker:"))
                {
                    Maker = line.Replace("Maker:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Source:"))
                {
                    Source = line.Replace("Source:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Tags:"))
                {
                    Tags = line.Replace("Tags:", "").Trim();
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
                    if (!string.IsNullOrEmpty(VideoFileName))
                    {
                        VideoFileName = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                        FullVideoFileName = Path.Combine(BeatmapFolder, VideoFileName);
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
                    if (!_modeHasSet)
                    {
                        int.TryParse(temparr[1].Trim(), out _m);
                        Mode = (OsuGameMode) _m;
                        _modeHasSet = true;
                    }

                    continue;
                }

                if (line.Contains("TimingPoints")) break;
            }

            FullAudioFileName = x.FilenameFull.Replace(x.Filename, x.AudioFilename);
            FullVideoFileName = x.FilenameFull.Replace(x.Filename, x.VideoFilename);
            getAddtionalInfo(alllines);
        }
    }
}