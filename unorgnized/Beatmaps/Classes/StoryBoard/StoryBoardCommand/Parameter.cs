using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 参数
    /// </summary>
    public class Parameter : IStoryBoardSubCommand, IDurable, IHasEasing
    {
        /// <summary>
        /// 行为
        /// </summary>
        public ParameterOperation Operation { get; set; } = ParameterOperation.None;
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
        /// <inheritdoc />
        public StoryBoardEasing Easing { get; set; }
        /// <inheritdoc />
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Parameter;
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardCommand ParentCommand { get; set; }
        /// <inheritdoc />
        public void Parse(string line)
        {
            var parts = line.Split(',');
            if (int.TryParse(parts[1], out var eas))
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