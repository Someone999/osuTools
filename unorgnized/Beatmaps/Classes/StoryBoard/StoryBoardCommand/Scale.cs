using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class Scale : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
        /// <inheritdoc />
        public StoryBoardEasing Easing { get; set; }
        /// <inheritdoc />
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();
        /// <inheritdoc />
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Scale;
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardCommand ParentCommand { get; set; }
        /// <inheritdoc />
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
