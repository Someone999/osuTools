namespace osuTools.StoryBoard.Command
{
    public class RotateTranslation : ITranslation
    {
        public RotateTranslation(Degrees start, Degrees tar, int sttm, int edtm)
        {
            StartDegree = start;
            TargetDegree = tar;
            StartTime = sttm;
            EndTime = edtm;
        }

        public Degrees StartDegree { get; set; }
        public Degrees TargetDegree { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}