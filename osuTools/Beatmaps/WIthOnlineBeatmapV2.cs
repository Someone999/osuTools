using System;
using osuTools.OnlineInfo.OsuApiV2.ResultClasses;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        /// 使用<seealso cref="OnlineBeatmapV2"/>初始化一个Beatmap
        /// </summary>
        /// <param name="beatmap">在线查询到的谱面</param>
        public Beatmap(OnlineBeatmapV2 beatmap)
        {
            if (beatmap.BeatmapSet is null)
                throw new ArgumentException("无法从没有BeatmapSet属性的Beatmap对象中部分基础信息。",nameof(beatmap));
            var beatmapSet = beatmap.BeatmapSet;
            Title = beatmapSet.Title;
            TitleUnicode = beatmapSet.TitleUnicode;
            Artist = beatmapSet.Artist;
            ArtistUnicode = beatmapSet.ArtistUnicode;
            Creator = beatmapSet.Creator;
            Difficulty = beatmap.Version;
            Tags = beatmapSet.Tags;
            Source = beatmapSet.Source;
            HasVideo = beatmapSet.HasVideo;
            ApproachRate = beatmap.ApproachRate;
            OverallDifficulty = beatmap.OverallDifficulty;
            HpDrain = beatmap.HpDrain;
            CircleSize = beatmap.CircleSize;
            DownloadLink = $"https://osu.ppy.sh/b/{beatmap.BeatmapId}";
            Stars = beatmap.Stars;
            Mode = beatmap.Mode;
        }
    }
}
