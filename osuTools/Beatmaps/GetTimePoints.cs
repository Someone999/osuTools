using System.IO;
using osuTools.Beatmaps.TimingPoint;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private TimingPointCollection _tps;
        /// <summary>
        /// 谱面的所有时间点
        /// </summary>
        public TimingPointCollection TimingPoints
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
            var timePoints = new TimingPointCollection();
            var nstr = "";
            foreach (var str in map)
            {
                if (str.Trim().StartsWith("[") && str.Trim().EndsWith("]"))
                    nstr = str.Trim().TrimStart('[').TrimEnd(']');
                if (nstr == "TimingPoints")
                {
                    var comasp = str.Split(',');
                    if (comasp.Length > 7) timePoints.TimePoints.Add(new TimingPoint.TimingPoint(str));
                }
            }

            _tps = timePoints;
        }
    }
}