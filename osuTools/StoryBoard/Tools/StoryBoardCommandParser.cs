using System.Collections.Generic;
using System.IO;
using System.Linq;
using osuTools.StoryBoard.Commands.Interface;

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

    }
}
