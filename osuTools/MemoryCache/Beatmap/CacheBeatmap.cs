using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using osuTools.Beatmaps;
using osuTools.Game.Modes;
using osuTools.OnlineInfo.OsuApiV1.OnlineQueries;
using osuTools.OsuDB.Beatmap;

namespace osuTools.MemoryCache.Beatmap
{
    /// <summary>
    /// 内存中缓存的谱面
    /// </summary>
    public class CacheBeatmap : IBeatmap, IEqualityComparer<CacheBeatmap>
    {
        private readonly Beatmaps.Beatmap _innerBeatmap;
        /// <summary>
        /// 当前存储的Beatmap
        /// </summary>
        public Beatmaps.Beatmap CurrentBeatmap => _innerBeatmap;
        /// <summary>
        /// 使用<seealso cref="IBeatmap"/>创建一个CacheBeatmap
        /// </summary>
        /// <param name="beatmap"></param>
        /// <param name="songDuration"></param>
        /// <param name="drainTime"></param>
        /// <param name="status"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CacheBeatmap(Beatmaps.Beatmap beatmap, TimeSpan songDuration, TimeSpan drainTime, OsuBeatmapStatus status)
        {
            _innerBeatmap = beatmap ?? throw new ArgumentNullException(nameof(beatmap));
            SongDuration = songDuration;
            DrainTime = drainTime;
            Status = status;
        }
        /// <summary>
        /// 曲目时长
        /// </summary>
        public TimeSpan SongDuration { get; }
        /// <summary>
        /// 最后一个HitObject的时间
        /// </summary>
        public TimeSpan DrainTime { get; }

        /// <summary>
        /// 谱面状态
        /// </summary>
        public OsuBeatmapStatus Status { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Title => _innerBeatmap.Title;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TitleUnicode =>  _innerBeatmap.TitleUnicode;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Artist => _innerBeatmap.Artist;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ArtistUnicode => _innerBeatmap.ArtistUnicode;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Creator => _innerBeatmap.Creator;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Version => _innerBeatmap.Version;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double ApproachRate => _innerBeatmap.ApproachRate;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double CircleSize => _innerBeatmap.CircleSize;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double HpDrain => _innerBeatmap.HpDrain;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double OverallDifficulty => _innerBeatmap.OverallDifficulty;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int BeatmapId => _innerBeatmap.BeatmapId;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int BeatmapSetId => _innerBeatmap.BeatmapSetId;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Stars => _innerBeatmap.Stars;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double Bpm => _innerBeatmap.Bpm;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public OsuGameMode Mode => _innerBeatmap.Mode;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return $"{Artist} - {Title} [{Version}]";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Equals(CacheBeatmap x, CacheBeatmap y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.GetType() == y.GetType() && Equals(x._innerBeatmap, y._innerBeatmap);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int GetHashCode(CacheBeatmap obj)
        {
            return obj._innerBeatmap != null ? obj._innerBeatmap.GetHashCode() : 0;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is CacheBeatmap cacheBeatmap)
            {
                return cacheBeatmap._innerBeatmap == _innerBeatmap;
            }
            return false;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode()
        {
            return _innerBeatmap.Md5.ToString().GetHashCode();
        }
    }
}
