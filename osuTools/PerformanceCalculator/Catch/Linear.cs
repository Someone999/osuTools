using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    class Linear:ICurveAlgorithm
    {
        public List<OsuPixel> Position { get; }

        public Linear(List<OsuPixel> points) => Position = points;
        
    }
}