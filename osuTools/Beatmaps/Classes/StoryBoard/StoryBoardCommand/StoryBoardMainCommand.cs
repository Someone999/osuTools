using System.Collections.Generic;

namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// StoryBoard主命令
    /// </summary>
    public class StoryBoardMainCommand : IStoryBoardMainCommand
    {
        /// <inheritdoc />
        public StoryBoardResourceType ResourceType { get; set; }
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardResource Resource { get; set; }
        /// <inheritdoc />

        public void Parse(string line)
        {
            var ls = line.Split(',');
            if (line[0] != ' ')
            {
                if (ls[0] == "Sprite")
                    Resource = new Sprite();
                if (ls[0] == "Sample")
                    Resource = new Audio();
                if (ls[0] == "Animation")
                    Resource = new Animation();
                Resource.Parse(line);
                ResourceType = Resource.ResourceType;
            }
        }
    }
}