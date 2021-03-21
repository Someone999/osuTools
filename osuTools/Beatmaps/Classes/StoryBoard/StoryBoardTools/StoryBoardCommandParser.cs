using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.StoryBoard.Command;

namespace osuTools.Beatmaps.Classes.StoryBoard.StoryBoardTools
{
    public class StoryBoardCommandString
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
                var curTmp = osuTools.StoryBoard.StoryBoardTools.GetEventClassByString(commandStr);
                if (curTmp != null)
                    CurrentCommand = curTmp;
                else
                    CurrentCommand = new StoryBoardMainCommand();
                CurrentCommand.Parse(commandStr);
                if (Parent != null)
                {
                    if (ParentCommand is null)
                    {
                        var tmp = osuTools.StoryBoard.StoryBoardTools.GetEventClassByString(Parent.Command);
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
    public class StoryBoardCommandParser
    {
        public string StoryBoardCommandFile { get; private set; }
        public StringProcessor StringProcessor { get; set; }
        private HashSet<IStoryBoardCommand> _commands = new HashSet<IStoryBoardCommand>();

        public StoryBoardCommandParser(string storyBoardCommandFile)
        {
            StoryBoardCommandFile = storyBoardCommandFile;
        }

        public IStoryBoardCommand[] Parse()
        {
            string[] s = File.ReadAllLines(StoryBoardCommandFile);
            IStoryBoardCommand last = null;
            StoryBoardCommandString lastStr = null;
            foreach (var line in s)
            {
                StringProcessor = new StringProcessor(line);
                StringProcessor.Process();
                lastStr = StringProcessor.SpaceNum == 0
                    ? new StoryBoardCommandString(null, StringProcessor.ProcessedString, StringProcessor.SpaceNum)
                    : new StoryBoardCommandString(lastStr, StringProcessor.ProcessedString, StringProcessor.SpaceNum);
                if (!string.IsNullOrEmpty(lastStr.Command))
                {
                    if (lastStr.CurrentCommand is IStoryBoardMainCommand && !_commands.Contains(lastStr.CurrentCommand)) //如果当前的命令是主命令
                        _commands.Add(lastStr.CurrentCommand);//添加到列表
                    /*if (!_commands.Contains(lastStr.ParentCommand) && !(lastStr.ParentCommand is null)) //如果当前命令的主命令不为空
                        _commands.Add(lastStr.ParentCommand);  // 添加到列表*/
                }


                /*if (!string.IsNullOrEmpty(lastStr.Command))
                {
                    if (lastStr.Parent == null)
                    {
                        last = new StoryBoardMainCommand();
                        last.Parse(lastStr.Command);
                        _commands.Add(last);
                    }
                    else
                    {
                        var par = last;
                        last = osuTools.StoryBoard.StoryBoardTools.GetEventClassByString(lastStr.Command);
                        if (last is IStoryBoardSubCommand sub)
                        {
                            sub.ParentCommand = par;
                            par.SubCommands.Add(sub);
                            if (!_commands.Contains(par))
                            {
                                if(!(par is IStoryBoardSubCommand))
                                {
                                    _commands.Add(par);
                                }
                            }
                        }
                    }
                    if (last == null)
                    {
                        Console.WriteLine("无法解析的文本。");
                        continue;
                    } 

                    
                }*/

            }
            return _commands.ToArray();
        }

    }
}
