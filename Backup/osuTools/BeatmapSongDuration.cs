using System;
using System.Collections.Generic;

namespace osuTools
{
    /// <summary>
    /// 存储谱面Id和谱面的曲目时长(单位为毫秒)
    /// </summary>
    public class BeatmapSongDuration:IEqualityComparer<BeatmapSongDuration>,IEquatable<BeatmapSongDuration>
    {
        /// <summary>
        /// 使用谱面Id和曲目时长(单位为毫秒)初始化BeatmapSongDuration
        /// </summary>
        /// <param name="beatmapId"></param>
        /// <param name="beatmapMd5"></param>
        /// <param name="millisec"></param>
        public BeatmapSongDuration(int beatmapId,string beatmapMd5, double millisec)
        {
            BeatmapId = beatmapId;
            BeatmapMd5 = beatmapMd5 ?? throw new NullReferenceException();
            Duration = millisec;
        }

        /// <summary>
        /// 谱面Id
        /// </summary>
        public int BeatmapId { get; }
        /// <summary>
        /// 谱面Md5
        /// </summary>
        public string BeatmapMd5 { get; }
        /// <summary>
        /// 时长，以毫秒为单位
        /// </summary>
        public double Duration { get; internal set; }
        /// <inheritdoc/>
        public bool Equals(BeatmapSongDuration x, BeatmapSongDuration y)
        {
            if (x is null && y is null)
                return true;
            if (x is null || y is null)
                return false;
            return x.BeatmapMd5 == y.BeatmapMd5;
        }
        /// <inheritdoc/>
        public bool Equals(BeatmapSongDuration other)
        {
            return Equals(this, other);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return BeatmapMd5.GetHashCode();
        }
        /// <inheritdoc/>
        public int GetHashCode(BeatmapSongDuration obj)
        {
            return obj.GetHashCode();
        }
    }
}