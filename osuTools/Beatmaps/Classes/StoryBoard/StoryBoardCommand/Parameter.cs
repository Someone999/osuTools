using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class Parameter : IStoryBoardSubCommand, IDurable, IHasEasing
    {
        public ParameterOperation Operation { get; set; } = ParameterOperation.None;
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public StoryBoardEasing Easing { get; set; }
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Parameter;
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        public IStoryBoardCommand ParentCommand { get; set; }

        public void Parse(string line)
        {
            var parts = line.Split(',');
            var eas = 0;
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) ed = parts[2];
            if (int.TryParse(parts[1], out eas))
                Easing = (StoryBoardEasing) eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            if (string.IsNullOrEmpty(parts[3])) parts[3] = parts[2];
            EndTime = int.Parse(parts[3]);
            var op = parts[4];
            Operation = op == "A" ? ParameterOperation.AddictiveColorBlend :
                op == "H" ? ParameterOperation.HorizentalFlip :
                op == "V" ? ParameterOperation.VerticalFlip : ParameterOperation.None;
        }
    }
}