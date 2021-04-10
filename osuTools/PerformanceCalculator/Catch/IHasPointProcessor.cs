using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    public interface IHasPointProcessor:ICurveAlgorithm
    {
        OsuPixel PointAtDistance(double length);
        List<OsuPixel> Points { get; }

    }
}