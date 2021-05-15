using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.PerformanceCalculator.Catch
{
    public interface ICatchHitObject
    {
        double x { get; }
        double y { get; }
        double Offset { get; }
    }
}
