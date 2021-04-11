namespace osuTools.StoryBoard.Command
{
    public class MoveTranslation : ITranslation
    {
        public MoveTranslation(StoryBoardPoint start, StoryBoardPoint tar, int sttm, int edtm)
        {
            StartPoint = start;
            TargetPoint = tar;
            StartTime = sttm;
            EndTime = edtm;
        }

        public StoryBoardPoint StartPoint { get; set; }
        public StoryBoardPoint TargetPoint { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}