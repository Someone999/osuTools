using osuTools.Skins.Colors;

namespace osuTools.StoryBoard.Command
{
    public class ColorTranslation : ITranslation
    {
        public ColorTranslation(RgbColor start, RgbColor target, int starttm, int endtm)
        {
            StartColor = start;
            TargetColor = target;
        }

        public RgbColor StartColor { get; set; }
        public RgbColor TargetColor { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}