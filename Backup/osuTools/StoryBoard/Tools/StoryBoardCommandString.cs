using osuTools.StoryBoard.Commands;
using osuTools.StoryBoard.Commands.Interface;

namespace osuTools.StoryBoard.Tools
{
    class StoryBoardCommandString
    {
        public int Level { get; }
        public StoryBoardCommandString Parent { get; }
        public IStoryBoardCommand ParentCommand { get; }
        public IStoryBoardCommand CurrentCommand { get; }
        public bool IsFirstSubCommand { get; }
        public string Command { get; }

        bool IsInvalid(string commandStr)
        {
            return commandStr.StartsWith("[") || commandStr.StartsWith("//");
        }
        public StoryBoardCommandString(StoryBoardCommandString last, string commandStr, int level)
        {
            if (!IsInvalid(commandStr))
            {
                Level = level;
                if (last == null)
                    Parent = null;
                else if (last.Parent == null || last.Level == 0)
                {
                    Parent = last;
                    ParentCommand = last.CurrentCommand;
                }
                else if (last.Level < Level)
                {
                    Parent = last;
                    ParentCommand = last.CurrentCommand;
                    IsFirstSubCommand = true;
                }
                else if (last.Level == Level)
                {
                    Parent = last.Parent;
                    ParentCommand = last.ParentCommand;
                }
                else if(last.Level > level)
                {
                    Parent = last.Parent.Parent;
                    ParentCommand = last.Parent.ParentCommand;
                }
                var curTmp = StoryBoardTools.GetEventClassByString(commandStr);
                if (curTmp != null)
                    CurrentCommand = curTmp;
                else
                    CurrentCommand = new StoryBoardMainCommand();
                CurrentCommand.Parse(commandStr);
                if (Parent != null)
                {
                    if (ParentCommand is null)
                    {
                        var tmp = StoryBoardTools.GetEventClassByString(Parent.Command);
                        if (tmp != null)
                            ParentCommand = tmp;
                        else
                            ParentCommand = new StoryBoardMainCommand();
                    }

                    ParentCommand.Parse(Parent.Command);
                    if (CurrentCommand is IStoryBoardSubCommand subCmd)
                    {
                        ParentCommand.SubCommands.Add(subCmd);
                        subCmd.ParentCommand = ParentCommand;
                    }
                }
                Command = commandStr;
            }
            else
            {
                Command = null;
            }
        }
    }
}