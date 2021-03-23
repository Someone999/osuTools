using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    public interface ICatchHitObject
    {
        double x { get; }
        double y { get; }
        double Offset { get; }
    }
    public interface ICurveAlgorithm
    {
    }

    public interface IHasPointProcessor:ICurveAlgorithm
    {
        OsuPixel PointAtDistance(double length);
        List<OsuPixel> Points { get; }
       
    }

    public interface IHasPosition
    {
        List<OsuPixel> Position { get; }
    }
}
