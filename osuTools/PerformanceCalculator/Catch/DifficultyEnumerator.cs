using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
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
            diffs.Add(new MKeyValuePair<string, double>("ApproachRate", attr.ApprochRate));
            diffs.Add(new MKeyValuePair<string, double>("OverallDifficulty", attr.OverallDifficulty));
            diffs.Add(new MKeyValuePair<string, double>("HPDrain", attr.HPDrain));
            diffs.Add(new MKeyValuePair<string, double>("CircleSize", attr.CircleSize));
            diffs.Add(new MKeyValuePair<string, double>("ApprochRate", attr.ApprochRate));
            diffs.Add(new MKeyValuePair<string, double>("OverallDifficulty", attr.OverallDifficulty));
            diffs.Add(new MKeyValuePair<string, double>("CircleSize", attr.CircleSize));
            diffs.Add(new MKeyValuePair<string, double>("HPDrain", attr.HPDrain));
            diffs.Add(new MKeyValuePair<string, double>("SliderMultiplier", attr.SliderMultiplier));
            diffs.Add(new MKeyValuePair<string, double>("SliderTickRate", attr.SliderTickRate));
            _len = diffs.Count;
        }
    }
}