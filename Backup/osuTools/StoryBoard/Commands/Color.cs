using System.Collections.Generic;
using osuTools.Skins.Color;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;
using osuTools.StoryBoard.Tools;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    ///     颜色变换
    /// </summary>
    public class Color : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        /// <summary>
        ///     开始时间
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        ///     缓入缓出模式
        /// </summary>
        public StoryBoardEasing Easing { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();

        /// <summary>
        ///     子命令
        /// </summary>
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();

        /// <summary>
        ///     父命令
        /// </summary>
        public IStoryBoardCommand ParentCommand { get; set; }

        /// <summary>
        ///     StoryBoard事件
        /// </summary>
        public StoryBoardEvent Command { get; set; } = StoryBoardEvent.Color;

        /// <summary>
        ///     将字符串解析成Fade
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var parts = data.Split(',');
            var suc = int.TryParse(parts[1], out var eas);
            if (suc) Easing = (StoryBoardEasing) eas;
            else Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
            EndTime = int.Parse(parts[3]);
            var i = 4;
            var j = 1;
            if (i + 3 == parts.Length)
                Translations.Add(new ColorTranslation(
                    new RgbColor(int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])),
                    new RgbColor(int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])), StartTime, EndTime));
            while (i + 3 < parts.Length)
            {
                var r = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                var g = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                var b = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                var er = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                var eg = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                var eb = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                var dur = EndTime - StartTime;
                Translations.Add(new ColorTranslation(new RgbColor(r, b, g), new RgbColor(er, eb, eg),
                    StartTime + j * dur, EndTime + j * dur));
                j++;
                if (i + 1 < parts.Length)
                    i -= 3;
            }
        }
    }
}