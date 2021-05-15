using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// Catch模式的可计算难度的HitObject
    /// </summary>
    public class CatchDifficultyHitObject
    {
        public double Strain { get; private set; } = 1;
        public double Offset { get; internal set; }
        public ICatchHitObject HitObject { get; }
        private double _errorMargin = Constants.AbsolutePlayerPositioningError;
        public double LastMovement { get; internal set; }
        public double PlayerWidth { get; internal set; }
        public double ScaledPosition { get; internal set; }
        public double HyperdashDistance { get; internal set; } = 0;
        public bool HyperDash { get; internal set; } = false;

        public CatchDifficultyHitObject(ICatchHitObject hitObject,double playerWidth)
        {
            PlayerWidth = playerWidth;
            HitObject = hitObject;
            ScaledPosition = HitObject.x * (Constants.NormalizedHitobjectRadius / playerWidth);

        }
        internal void CalcStrain(CatchDifficultyHitObject lastHitObject,double timeRate)
        {
            var time = (HitObject.Offset - lastHitObject.HitObject.Offset) / timeRate;
            var decay = Math.Pow(Constants.DecayBase, time / 1000d);

            Offset = MathUtlity.Clamp(
                lastHitObject.ScaledPosition + lastHitObject.Offset,
                ScaledPosition - (Constants.NormalizedHitobjectRadius - _errorMargin),
                ScaledPosition + (Constants.NormalizedHitobjectRadius - _errorMargin)) - ScaledPosition;
            LastMovement = Math.Abs(ScaledPosition - lastHitObject.ScaledPosition + Offset - lastHitObject.Offset);
            var addition = Math.Pow(LastMovement, 1.3) / 500;
            if (ScaledPosition < lastHitObject.ScaledPosition)
                LastMovement *= -1;

            var additionBonus = 0d;
            var sqrtTime = Math.Sqrt(Math.Max(time, 25));

            if (Math.Abs(LastMovement) > 0.1)
            {
                if (Math.Abs(lastHitObject.LastMovement) > 0.1 &&
                    MathUtlity.Sign(LastMovement) != MathUtlity.Sign(lastHitObject.LastMovement))
                {
                    var bonus = Constants.DirectionChangeBonus / sqrtTime;
                    var bonusFactor = Math.Min(_errorMargin, Math.Abs(LastMovement)) / _errorMargin;
                    addition += bonus * bonusFactor;
                    if (lastHitObject.HyperdashDistance <= 10)
                    {
                        additionBonus += 0.3 * bonusFactor;
                    }
                }
                addition += 7.5 * Math.Min(Math.Abs(LastMovement), Constants.NormalizedHitobjectRadius * 2) / (Constants.NormalizedHitobjectRadius * 6) / sqrtTime;
            }
            if (lastHitObject.HyperdashDistance <= 10)
            {
                if (!lastHitObject.HyperDash)
                {
                    additionBonus += 1;
                }
                else
                {
                    Offset = 0;
                }
                addition *= 1 + additionBonus * ((10 - lastHitObject.HyperdashDistance) / 10);
            }
            addition *= 850.0 / Math.Max(time, 25);
            Strain = lastHitObject.Strain * decay + addition;
        }
    }
}
