using System.IO;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private void getBreakTimes()
        {
            if (string.IsNullOrEmpty(FullPath))
            {
                BreakTimes = new BreakTimeCollection();
                return;
            }

            var breaktimes = new BreakTimeCollection();
            var block = DataBlock.None;
            var map = File.ReadAllLines(FullPath);
            foreach (var str in map)
            {
                if (str.Contains("Break Periods") && str.StartsWith("//")) block = DataBlock.BreakTime;
                if (block == DataBlock.BreakTime)
                {
                    var breakstr = str.Split(',');
                    if (breakstr.Length == 3)
                    {
                        var i = 0;
                        if (int.TryParse(breakstr[0], out i))
                            if (i == 2)
                                breaktimes.BreakTimes.Add(new BreakTime(long.Parse(breakstr[1]),
                                    long.Parse(breakstr[2])));
                    }
                }

                if (str.Contains("HitObjects"))
                {
                    block = DataBlock.HitObjects;
                    break;
                }
            }

            _breakTimes = breaktimes;
        }
    }
}