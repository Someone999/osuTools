using System;
using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchDifficultyAttribute:IEnumerable<MKeyValuePair<string,double>>
    {
        public double ApprochRate { get; set; } = double.NaN;
        public double OverallDifficulty { get; set; } = double.NaN;
        public double CircleSize { get; set; } = double.NaN;
        public double HPDrain { get; set; } = double.NaN;
        public double SliderMultiplier { get; set; } = double.NaN;
        public double SliderTickRate { get; set; } = double.NaN;

        public double this[string s]
        {
            get
            {
                if (s.Equals("ApproachRate", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    return ApprochRate;
                if (s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    return OverallDifficulty;
                if (s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    return CircleSize;
                if (s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase))
                    return HPDrain;
                if (s.Equals("SliderMul", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("SliderMultiplier", StringComparison.OrdinalIgnoreCase))
                    return SliderMultiplier;
                if (s.Equals("SliderTickRate", StringComparison.OrdinalIgnoreCase))
                    return SliderTickRate;
                throw new ArgumentException();
            }
            set
            {
                if (s.Equals("ApproachRate", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    ApprochRate = value;
                else if (s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    OverallDifficulty = value;
                else if (s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    CircleSize = value;
                else if (s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase))
                    HPDrain = value;
                else if (s.Equals("SliderMul", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("SliderMultiplier", StringComparison.OrdinalIgnoreCase))
                    SliderMultiplier = value;
                else if (s.Equals("SliderTickRate", StringComparison.OrdinalIgnoreCase))
                    SliderTickRate = value;
                else throw new ArgumentException();
            }
        }

        public IEnumerator<MKeyValuePair<string, double>> GetEnumerator() => new DifficultyEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new DifficultyEnumerator(this);
    }
}