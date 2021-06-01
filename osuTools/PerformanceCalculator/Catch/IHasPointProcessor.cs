using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    interface IHasPointProcessor:ICurveAlgorithm
    {
        OsuPixel PointAtDistance(double length);
        List<OsuPixel> Points { get; }

    }
}