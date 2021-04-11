using osuTools.Skins.Colors;

namespace osuTools.StoryBoard.Command
{
    public class ColorTranslation : ITranslation
    {
        public ColorTranslation(RGBColor start, RGBColor target, int starttm, int endtm)
        {
            StartColor = start;
            TargetColor = target;
        }

        public RGBColor StartColor { get; set; }
        public RGBColor TargetColor { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}