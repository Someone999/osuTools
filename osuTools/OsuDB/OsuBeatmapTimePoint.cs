namespace osuTools.OsuDB
{
    /// <summary>
    ///     一个变速点或时间标志。
    /// </summary>
    public class OsuBeatmapTimePoint
    {
        internal OsuBeatmapTimePoint(double bpm, double offset, bool inherit)
        {
            BPM = 1 / bpm * 1000 * 60;
            Offset = offset;
            Inherit = inherit;
        }

        /// <summary>
        ///     该时间点对应的BPM
        /// </summary>
        public double BPM { get; internal set; }

        /// <summary>
        ///     该时间点相对于开始的偏移量
        /// </summary>
        public double Offset { get; internal set; }

        /// <summary>
        ///     是否为继承时间线(是不是绿线)
        /// </summary>
        public bool Inherit { get; internal set; }
    }
}