using System.Globalization;
using System.IO;
using System.Text;
using osuTools.Exceptions;
using osuTools.GameInfo;
using osuTools.MD5Tools;
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
            Title = beatmap.Title;
            TitleUnicode = beatmap.TitleUnicode;
            Artist = beatmap.Artist;
            ArtistUnicode = beatmap.ArtistUnicode;
            Creator = beatmap.Creator;
            Difficulty = beatmap.Difficulty;
            Version = Difficulty;
            FileName = beatmap.FileName;
            FullPath = Path.Combine(info.BeatmapDirectory, beatmap.FolderName, beatmap.FileName);
            DownloadLink = $"http://osu.ppy.sh/b/{beatmap.BeatmapId}";
            Source = beatmap.Source;
            Tags = beatmap.Tags;
            Maker = "";
            Md5 = new MD5String(beatmap.Md5);
            FullAudioFileName = Path.Combine(info.BeatmapDirectory, beatmap.FolderName, beatmap.AudioFileName);
            FullVideoFileName = "";
            OverallDifficulty = beatmap.OverallDifficulty;
            HpDrain = beatmap.HpDrain;
            ApproachRate = beatmap.ApproachRate;
            CircleSize = beatmap.CircleSize;
            BeatmapSetId = beatmap.BeatmapSetId;
            AudioFileName = beatmap.AudioFileName;
            Mode = beatmap.Mode;
            if (getStars)
            {
                double.TryParse(beatmap.Stars.ToString(CultureInfo.InvariantCulture), out var stars);
                Stars = stars;
            }
            else Stars = 0;
            if (FullPath == "" || !File.Exists(FullPath)) return;
            var alllines = File.ReadAllLines(FullPath);
            if (!alllines[0].Contains("osu file format"))
            {
                Notv = true;

                throw new InvalidBeatmapFileException($"文件{FullPath}不是谱面文件。");
            }

            StringBuilder b = new StringBuilder();
            foreach (var c in alllines[0])
            {
                if (char.IsDigit(c))
                    b.Append(c);
            }
            BeatmapVersion = int.Parse(b.ToString());
            foreach (var line in alllines)
            {
                var temparr = line.Split(':');
                if (temparr[0].StartsWith("0,0,\""))
                {
                    if (string.IsNullOrEmpty(BackgroundFileName))
                        BackgroundFileName = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    FullBackgroundFileName = Path.Combine(BeatmapFolder, BackgroundFileName);
                    continue;
                }

                if (temparr[0].StartsWith("Video,"))
                {
                    VideoFileName = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    FullVideoFileName = Path.Combine(BeatmapFolder, FullVideoFileName);
                    HasVideo = !string.IsNullOrEmpty(VideoFileName);
                    continue;
                }

                FullVideoFileName = FullPath.Replace(FileName, VideoFileName);
                if (line.Contains("TimingPoints")) break;
            }

            BeatmapId = beatmap.BeatmapId;
            getAddtionalInfo(alllines);
        }
    }
}