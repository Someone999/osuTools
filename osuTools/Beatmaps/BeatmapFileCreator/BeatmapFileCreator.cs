using System.Text;

namespace osuTools.Beatmaps.BeatmapFileCreator
{
    public partial class BeatmapFileCreator
    {
        /// <summary>
        /// 默认的谱面格式
        /// </summary>
        public static string BeatmapFileFormat =
            "osu file format v14\n\n" +
            "[General]\n" +
            "AudioFilename: {0}\n" +
            "AudioLeadIn: {1}\n" +
            "PreviewTime: {2}\n" +
            "Countdown: {3}\n" +
            "SampleSet: {4}\n" +
            "StackLeniency: {5}\n" +
            "Mode: {6}\n" +
            "LetterboxInBreaks: {7}\n" +
            "WidescreenStoryboard: {8}\n\n" +
            "[Editor]\n" +
            "Bookmarks: {9}\n" +
            "DistanceSpacing: {10}\n" +
            "BeatDivisor: {11}\n" +
            "GridSize: {12}\n" +
            "TimelineZoom: {13}\n\n" +
            "[Metadata]\n" +
            "Title:{14}\n" +
            "TitleUnicode:{15}\n" +
            "Artist:{16}\n" +
            "ArtistUnicode:{17}\n" +
            "Creator:{18}\n" +
            "Version:{19}\n" +
            "Source:{20}\n" +
            "Tags:{21}\n" +
            "BeatmapId:{22}\n" +
            "BeatmapSetId:{23}\n\n" +
            "[Difficulty]\n" +
            "HPDrainRate:{24}\n" +
            "CircleSize:{25}\n" +
            "OverallDifficulty:{26}\n" +
            "ApproachRate:{27}\n" +
            "SliderMultiplier:{28}\n" +
            "SliderTickRate:{29}\n\n" +
            "[Events]\n" +
            "0,0,{30},0,0\n" +
            "{31}\n\n" +
            "{32}\n\n" +
            "[TimingPoints]\n" +
            "{33}\n\n" +
            "{34}\n\n" +
            "[HitObjects]\n" +
            "{35}\n\n";

        /// <summary>
        ///     获取要写入的格式
        /// </summary>
        /// <returns></returns>
        public virtual string GetFormat()
        {
            var b = BaseBeatmap;
            var bookm = b.Bookmarks;
            if (bookm.Count == 0) bookm.Add(0);
            var breaks = b.BreakTimes;
            var hitObjects = b.HitObjects;
            var timePoints = b.TimePoints;
            var breaktimestrs = new StringBuilder();
            var bookmks = new StringBuilder();
            var timePointsStrs = new StringBuilder();
            var hitObjectsStrs = new StringBuilder();
            for (var i = 0; i < bookm.Count; i++)
            {
                bookmks.Append(bookm[i]);
                if (i != bookm.Count - 1)
                    bookmks.Append(",");
            }

            for (var i = 0; i < breaks.Count; i++)
                breaktimestrs.Append(breaks[i].ToOsuFormat() + "\n");
            for (var i = 0; i < timePoints.Count; i++)
                timePointsStrs.Append(timePoints[i].ToOsuFormat() + "\n");
            foreach (var hitObject in hitObjects)
                hitObjectsStrs.Append(hitObject.ToOsuFormat() + "\n");

            var video = $"Video,0,\"{b.VideoFileName}\"";
            return string.Format(BeatmapFileFormat, b.AudioFileName, b.AudioLeadIn, b.PreviewTime,
                b.HasCountdown ? 1 : 0, (int) b.SampleSet, b.StackLeniency, (int) b.Mode, b.LetterboxInBreaks ? 1 : 0,
                b.WidescreenStoryboard ? 1 : 0, bookmks, b.DistanceSpacing, b.BeatDivisor, b.GridSize, b.TimelineZoom,
                b.Title, b.TitleUnicode, b.Artist, b.ArtistUnicode, b.Creator, b.Version,
                b.Source, b.Tags, b.BeatmapId, b.BeatmapSetId, b.HpDrain, b.CircleSize, b.OverallDifficulty, b.ApproachRate, b.SliderMultiplier,
                b.SliderTickRate, b.BackgroundFileName, b.HasVideo ? video : "", breaktimestrs, timePointsStrs, "",
                hitObjectsStrs);
        }
    }
}