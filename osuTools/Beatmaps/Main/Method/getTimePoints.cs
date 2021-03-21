using System.IO;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private TimePointCollection tps;

        public TimePointCollection TimePoints
        {
            get
            {
                if (tps == null)
                    getTimePoints();
                return tps;
            }
            private set => tps = value;
        }

        private void getTimePoints()
        {
            var map = File.ReadAllLines(FullPath);
            var timePoints = new TimePointCollection();
            var nstr = "";
            foreach (var str in map)
            {
                if (str.Trim().StartsWith("[") && str.Trim().EndsWith("]"))
                    nstr = str.Trim().TrimStart('[').TrimEnd(']');
                if (nstr == "TimingPoints")
                {
                    var comasp = str.Split(',');
                    if (comasp.Length > 7) timePoints.TimePoints.Add(new TimePoint(str));
                    continue;
                }

                if (nstr != "TimingPoints") continue;
            }

            tps = timePoints;
        }
    }
}