namespace osuTools.StoryBoard.Command
{
    public class FadeTranslation : ITranslation
    {
        public FadeTranslation(double start, double target, int starttm, int endtm)
        {
            StartOpacity = start;
            TargetOpacity = target;
        }

        /// <summary>
        ///     变化开始时的透明度。值在0-1之间
        /// </summary>
        public double StartOpacity { get; set; }

        /// <summary>
        ///     变化结束时的透明度。值在0-1之间
        /// </summary>
        public double TargetOpacity { get; set; }

        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}