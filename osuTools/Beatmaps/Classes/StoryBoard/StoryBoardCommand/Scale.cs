using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class ScaleMultiplier
    {
        public ScaleMultiplier(double overall)
        {
            Overall = overall;
        }

        /// <summary>
        ///     整体缩放倍率
        /// </summary>
        public double Overall { get; set; } = 1;
    }

    public class ScaleTranslation : ITranslation
    {
        public ScaleTranslation(ScaleMultiplier start, ScaleMultiplier target, int starttm, int endtm)
        {
            StartScaleMultiplier = start;
            TargetScaleMultiplier = target;
            StartTime = starttm;
            EndTime = endtm;
        }

        public ScaleMultiplier StartScaleMultiplier { get; set; }
        public ScaleMultiplier TargetScaleMultiplier { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }

    public class Scale : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public StoryBoardEasing Easing { get; set; }
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Scale;
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        public IStoryBoardCommand ParentCommand { get; set; }

        public void Parse(string line)
        {
            var parts = line.Split(',');
            var eas = 0;
            if (int.TryParse(parts[1], out eas))
                Easing = (StoryBoardEasing)eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            if (string.IsNullOrEmpty(parts[3])) parts[3] = parts[2];
            EndTime = int.Parse(parts[3]);
            var i = 4;
            var j = 0;
            if (i + 1 == parts.Length)
                Translations.Add(new ScaleTranslation(new ScaleMultiplier(double.Parse(parts[4])),
                    new ScaleMultiplier(double.Parse(parts[4])), StartTime, EndTime));
            while (i + 1 < parts.Length)
            {
                var stindex = i;
                var st = double.Parse(parts[i++]);
                var ed = double.Parse(parts[i + 1 < parts.Length ? i++ : parts.Length == i + 1 ? i : stindex]);
                var du = EndTime - StartTime;
                Translations.Add(new ScaleTranslation(new ScaleMultiplier(st), new ScaleMultiplier(ed), StartTime + j * du,
                    EndTime + j * du));
                if (i + 1 < parts.Length)
                    i--;
                j++;
            }
        }
    }
}
