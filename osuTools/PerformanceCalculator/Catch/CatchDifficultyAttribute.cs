using System;
using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    public class MKeyValuePair<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public MKeyValuePair(TKey key,TValue val)
        {
            Key = key;
            Value = val;
        }
    }
    class DifficultyEnumerator:IEnumerator<MKeyValuePair<string,double>>
    {
        private int _pos = -1;
        private int _len = 0;
        private List<MKeyValuePair<string, double>> diffs { get; set; } = new List<MKeyValuePair<string, double>>();
        public MKeyValuePair<string,double> Current => diffs[_pos];
        public void Reset() => _pos = -1;

        public void Dispose()
        {
        }

        public bool MoveNext() => ++_pos <= _len - 1;
        object IEnumerator.Current => diffs[_pos];
        public DifficultyEnumerator(CatchDifficultyAttribute attr)
        {
            diffs.Add(new MKeyValuePair<string, double>("SliderMul",attr.SliderMultiplier));
            diffs.Add(new MKeyValuePair<string, double>("AR", attr.ApprochRate));
            diffs.Add(new MKeyValuePair<string, double>("OD", attr.OverallDifficulty));
            diffs.Add(new MKeyValuePair<string, double>("HP", attr.HPDrain));
            diffs.Add(new MKeyValuePair<string, double>("CS", attr.CircleSize));
            diffs.Add(new MKeyValuePair<string, double>("ApprochRate", attr.ApprochRate));
            diffs.Add(new MKeyValuePair<string, double>("OverallDifficulty", attr.OverallDifficulty));
            diffs.Add(new MKeyValuePair<string, double>("CircleSize", attr.CircleSize));
            diffs.Add(new MKeyValuePair<string, double>("HPDrain", attr.HPDrain));
            diffs.Add(new MKeyValuePair<string, double>("SliderMultiplier", attr.SliderMultiplier));
            diffs.Add(new MKeyValuePair<string, double>("SliderTickRate", attr.SliderTickRate));
            _len = diffs.Count;
        }
    }
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
                if (s.Equals("AR", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    return ApprochRate;
                if (s.Equals("OD", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    return OverallDifficulty;
                if (s.Equals("CS", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    return CircleSize;
                if (s.Equals("HP", StringComparison.OrdinalIgnoreCase) ||
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
                if (s.Equals("AR", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    ApprochRate = value;
                else if (s.Equals("OD", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    OverallDifficulty = value;
                else if (s.Equals("CS", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    CircleSize = value;
                else if (s.Equals("HP", StringComparison.OrdinalIgnoreCase) ||
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