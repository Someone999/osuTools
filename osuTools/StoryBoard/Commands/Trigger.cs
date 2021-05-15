using System.Collections.Generic;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 触发器
    /// </summary>
    public class Trigger : ITriggerCommand, IDurable
    {
        /// <inheritdoc />
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Trigger;
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardCommand ParentCommand { get; set; }
        /// <inheritdoc />
        public string TriggerType { get; set; }
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
        /// <inheritdoc />
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