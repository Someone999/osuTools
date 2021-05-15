using System.Collections.Generic;

namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     表示一个StoryBoard的命令
    /// </summary>
    public interface IStoryBoardCommand
    {
        /// <summary>
        ///     子命令列表
        /// </summary>
        List<IStoryBoardSubCommand> SubCommands { get; }

        /// <summary>
        ///     将字符串解析为命令
        /// </summary>
        /// <param name="data"></param>
        void Parse(string data);
    }
}