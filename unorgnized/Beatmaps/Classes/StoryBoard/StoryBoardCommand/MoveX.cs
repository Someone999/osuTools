using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class MoveX : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public StoryBoardEasing Easing { get; set; }
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();
        public StoryBoardEvent Command { get; } = StoryBoardEvent.MoveX;
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        public IStoryBoardCommand ParentCommand { get; set; }

        public void Parse(string line)
        {
            var parts = line.Split(',');
            if (int.TryParse(parts[1], out var eas))
                Easing = (StoryBoardEasing)eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            if (string.IsNullOrEmpty(parts[3])) parts[3] = parts[2];
            EndTime = int.Parse(parts[3]);
            int i = 4, j = 1;
            if (i + 1 == parts.Length)
                Translations.Add(new MoveXTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime, EndTime));
            while (i + 1 < parts.Length)
            {
                var stindex = i;
                var st = double.Parse(parts[i++]);
                var ed = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex + 1]);
                var dur = EndTime - StartTime;
                Translations.Add(new MoveXTranslation(st, ed, StartTime + j * dur, EndTime + j * dur));
                j++;
                if (i + 1 < parts.Length)
                    i--;
            }
        }
    }
}
