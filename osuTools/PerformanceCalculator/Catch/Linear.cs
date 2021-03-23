using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    class Linear:ICurveAlgorithm
    {
        public CloneableList<OsuPixel> Position { get; }

        public Linear(CloneableList<OsuPixel> points) => Position = points;
        
    }
}