using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    public interface IHasPosition
    {
        List<OsuPixel> Position { get; }
    }
}