using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    class DifficultyEnumerator:IEnumerator<MKeyValuePair<string,double>>
    {
        private int _pos = -1;
        private readonly int _len = 0;
        private List<MKeyValuePair<string, double>> Diffs { get; } = new List<MKeyValuePair<string, double>>();
        public MKeyValuePair<string,double> Current => Diffs[_pos];
        public void Reset() => _pos = -1;

        public void Dispose()
        {
        }

        public bool MoveNext() => ++_pos <= _len - 1;
        object IEnumerator.Current => Diffs[_pos];
        public DifficultyEnumerator(CatchDifficultyAttribute attr)
        {
            Diffs.Add(new MKeyValuePair<string, double>("SliderMul",attr.SliderMultiplier));
            Diffs.Add(new MKeyValuePair<string, double>("ApproachRate", attr.ApprochRate));
            Diffs.Add(new MKeyValuePair<string, double>("OverallDifficulty", attr.OverallDifficulty));
            Diffs.Add(new MKeyValuePair<string, double>("HPDrain", attr.HPDrain));
            Diffs.Add(new MKeyValuePair<string, double>("CircleSize", attr.CircleSize));
            Diffs.Add(new MKeyValuePair<string, double>("ApprochRate", attr.ApprochRate));
            Diffs.Add(new MKeyValuePair<string, double>("OverallDifficulty", attr.OverallDifficulty));
            Diffs.Add(new MKeyValuePair<string, double>("CircleSize", attr.CircleSize));
            Diffs.Add(new MKeyValuePair<string, double>("HPDrain", attr.HPDrain));
            Diffs.Add(new MKeyValuePair<string, double>("SliderMultiplier", attr.SliderMultiplier));
            Diffs.Add(new MKeyValuePair<string, double>("SliderTickRate", attr.SliderTickRate));
            _len = Diffs.Count;
        }
    }
}