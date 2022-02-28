using System.Collections.Generic;
using System.IO;
using System.Linq;
using osuTools.StoryBoard.Commands;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Tools
{
    /// <summary>
    /// StoryBoard命令的解析器
    /// </summary>
    public class StoryBoardCommandParser
    {
        /// <summary>
        /// 包含StoryBoard数据的文件
        /// </summary>
        public string StoryBoardCommandFile { get; }
        /// <summary>
        /// 字符串处理器
        /// </summary>
        public StringProcessor StringProcessor { get; set; }
        private readonly HashSet<IStoryBoardCommand> _commands = new HashSet<IStoryBoardCommand>();
        /// <summary>
        /// 使用指定的文件初始化StoryBoardCommandParser
        /// </summary>
        /// <param name="storyBoardCommandFile"></param>
        public StoryBoardCommandParser(string storyBoardCommandFile)
        {
            StoryBoardCommandFile = storyBoardCommandFile;
        }
        /// <summary>
        /// 解析文件
        /// </summary>
        /// <returns></returns>
        public IStoryBoardCommand[] Parse()
        {
            string[] s = File.ReadAllLines(StoryBoardCommandFile);
            StoryBoardCommandString lastStr = null;
            foreach (var line in s)
            {
                StringProcessor = new StringProcessor(line);
                StringProcessor.Process();
                bool failed;
                lastStr = StringProcessor.SpaceNum == 0
                    ? new StoryBoardCommandString(null, StringProcessor.ProcessedString, StringProcessor.SpaceNum,out failed)
                    : new StoryBoardCommandString(lastStr, StringProcessor.ProcessedString, StringProcessor.SpaceNum,out failed);
                if (!string.IsNullOrEmpty(lastStr.Command))
                {
                    if (lastStr.CurrentCommand is IStoryBoardMainCommand && !_commands.Contains(lastStr.CurrentCommand) && !failed)
                        _commands.Add(lastStr.CurrentCommand);
                }
            }
            return _commands.ToArray();
        }
        /// <summary>
        /// 获取空格的数量
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetSpaceCount(string s)
        {
            int count = 0;
            if (string.IsNullOrEmpty(s))
                return 0;
            while (s[count] == ' ')
            {
                count++;
            }
            return count;
        }
        /*public IStoryBoardCommand[] Parse()
        {
            string[] s = File.ReadAllLines(StoryBoardCommandFile);
            Stack<IStoryBoardCommand> commandStack = new Stack<IStoryBoardCommand>();
            IStoryBoardCommand currentCommand;
            int lastSpace = -1;
            foreach (var line in s)
            {
                if (StoryBoardCommandString.IsInvalidCommand(line))
                    continue;
                int space = GetSpaceCount(line);
                if(space > lastSpace)
                {
                    if(space == 0)
                    {
                        currentCommand = new StoryBoardMainCommand();
                        currentCommand.Parse(line);
                        commandStack.Push(currentCommand);
                        _commands.Add(currentCommand);
                    }
                    else if (space > 0)
                    {
                        currentCommand = StoryBoardTools.GetEventClassByString(line.TrimStart());
                        ((IStoryBoardSubCommand)currentCommand).ParentCommand = commandStack.Peek();
                        commandStack.Peek().SubCommands.Add((IStoryBoardSubCommand)currentCommand);
                        commandStack.Push(currentCommand);
                        _commands.Add(currentCommand);
                    }
                    lastSpace = space;
                }
                else if (space < lastSpace)
                {
                    
                    commandStack.Pop().SubCommands.Add(StoryBoardTools.GetEventClassByString(line));
                    lastSpace = space;
                }
                else if(space == lastSpace)
                {
                    currentCommand = StoryBoardTools.GetEventClassByString(line.TrimStart());
                    IStoryBoardSubCommand subCommand = (IStoryBoardSubCommand)commandStack.Peek();
                    subCommand.ParentCommand.SubCommands.Add((IStoryBoardSubCommand)currentCommand);
                }
            }
            return _commands.ToArray();
        }*/
    }
}
