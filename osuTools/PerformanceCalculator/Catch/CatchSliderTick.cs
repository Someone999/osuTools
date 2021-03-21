using System;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchSliderTick:ICatchHitObject,ICloneable
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

        public object Clone()
        {
            return new CatchSliderTick(x, y, Offset);
        }
    }
}