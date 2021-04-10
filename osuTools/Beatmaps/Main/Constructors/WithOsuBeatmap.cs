using System.Globalization;
using System.IO;
using System.Windows.Forms;
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
            MD5 = new MD5String(beatmap.MD5);
            FullAudioFileName = Path.Combine(info.BeatmapDirectory, beatmap.FolderName, beatmap.AudioFileName);
            FullVideoFileName = "";
            OverallDifficulty = beatmap.OverallDifficulty;
            HPDrain = beatmap.HpDrain;
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
                    if (!string.IsNullOrEmpty(VideoFileName))
                        HasVideo = true;
                    else
                        HasVideo = false;
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