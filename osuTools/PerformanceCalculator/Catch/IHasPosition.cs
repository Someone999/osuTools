using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 有可以定位的点
    /// </summary>
    public interface IHasPosition
    {
        /// <summary>
        /// 可以定位的点
        /// </summary>
        List<OsuPixel> Position { get; }
    }
}