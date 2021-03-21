using System;

namespace osuTools.PerformanceCalculator.Catch
{
    [Serializable]
    public class CatchSliderTick:ICatchHitObject
    {
        public double x { get; internal set; }
        public double y { get; internal set; }
        public double Offset { get; internal set; } 

        public CatchSliderTick(double x, double y, double offset)
        {
            this.x = x;
            this.y = y;
            Offset = offset;
        }
    }
}