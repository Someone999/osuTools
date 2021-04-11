using System.IO;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private TimePointCollection _tps;
        /// <summary>
        /// 谱面的所有时间点
        /// </summary>
        public TimePointCollection TimePoints
        {
            get
            {
                if (_tps == null)
                    GetTimePoints();
                return _tps;
            }
            private set => _tps = value;
        }

        private void GetTimePoints()
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

            _tps = timePoints;
        }
    }
}