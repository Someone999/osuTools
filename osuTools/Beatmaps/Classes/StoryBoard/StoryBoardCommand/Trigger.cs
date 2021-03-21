using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class Trigger : ITriggerCommand, IDurable
    {
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Trigger;
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        public IStoryBoardCommand ParentCommand { get; set; }
        public string TriggerType { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public void Parse(string line)
        {
            var parts = line.Split(',');
            TriggerType = parts[1];
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
            StartTime = int.Parse(parts[2]);
            EndTime = int.Parse(parts[3]);
        }
    }
}