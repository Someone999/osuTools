namespace osuTools.StoryBoard.Command
{
    public class MoveXTranslation : ITranslation
    {
        public MoveXTranslation(double start, double target, int sttm, int edtm)
        {
            StartPoint = start;
            TargetPoint = target;
            StartTime = sttm;
            EndTime = edtm;
        }

        public double StartPoint { get; set; }
        public double TargetPoint { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}