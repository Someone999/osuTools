using System.Collections.Generic;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;
using osuTools.StoryBoard.Tools;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    ///     表示一次移动
    /// </summary>
    public class Move : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        /// <summary>
        ///     此命令的开始时间
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        ///     此命令的结束时间
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        ///     此命令的缓入缓出类型
        /// </summary>
        public StoryBoardEasing Easing { get; set; }
        ///<inheritdoc/>
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();

        /// <summary>
        ///     此命令的类型
        /// </summary>
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Move;

        /// <summary>
        ///     此命令的子命令
        /// </summary>
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();

        /// <summary>
        ///     此命令的父命令
        /// </summary>
        public IStoryBoardCommand ParentCommand { get; set; }

        /// <summary>
        ///     使用字符串构建一个Move对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var datas = data.Split(',');
            if (int.TryParse(datas[1], out var eas))
                Easing = (StoryBoardEasing) eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(datas[1]);
            StartTime = int.Parse(datas[2]);
            var ed = datas[3];
            if (string.IsNullOrEmpty(ed)) datas[3] = datas[2];
            EndTime = int.Parse(datas[3]);
            int i = 4, j = 0;
            if (i + 2 == datas.Length)
                Translations.Add(new MoveTranslation(
                    new StoryBoardPoint(double.Parse(datas[4]), double.Parse(datas[5])),
                    new StoryBoardPoint(double.Parse(datas[4]), double.Parse(datas[5])),
                    StartTime, EndTime));
            while (i + 2 < datas.Length)
            {
                var stindex = i;
                var stx = double.Parse(datas[i++]);
                var sty = double.Parse(datas[i++]);
                var edx = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : stindex]);
                var edy = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : stindex + 1]);
                var dur = EndTime - StartTime;
                Translations.Add(new MoveTranslation(new StoryBoardPoint(stx, sty),
                    new StoryBoardPoint(edx, edy),
                    StartTime + j * dur, EndTime + j * dur));
                j++;
                if (i + 1 < datas.Length)
                    i -= 2;
            }
        }
    }
}