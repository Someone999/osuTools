using System.IO;
using osuTools.Beatmaps.BreakTime;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private void GetBreakTimes()
        {
            if (string.IsNullOrEmpty(FullPath))
            {
                BreakTimes = new BreakTimeCollection();
                return;
            }

            var breaktimes = new BreakTimeCollection();
            var map = File.ReadAllLines(FullPath);
            foreach (var str in map)
            {
                var breakstr = str.Split(',');
                if (breakstr.Length == 3)
                {
                    if (int.TryParse(breakstr[0], out var i))
                        if (i == 2)
                            breaktimes.BreakTimes.Add(new BreakTime.BreakTime(long.Parse(breakstr[1]),
                                long.Parse(breakstr[2])));
                }

                if (str.Contains("HitObjects"))
                {
                    break;
                }
            }

            _breakTimes = breaktimes;
        }
    }
}