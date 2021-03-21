﻿using System;
using System.Collections.Generic;

namespace osuTools.StoryBoard.Command
{
    public class FadeTranslation : ITranslation
    {
        public FadeTranslation(double start, double target, int starttm, int endtm)
        {
            StartOpacity = start;
            TargetOpacity = target;
        }

        /// <summary>
        ///     变化开始时的透明度。值在0-1之间
        /// </summary>
        public double StartOpacity { get; set; }

        /// <summary>
        ///     变化结束时的透明度。值在0-1之间
        /// </summary>
        public double TargetOpacity { get; set; }

        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }

    /// <summary>
    ///     渐隐
    /// </summary>
    public class Fade : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
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
        public StoryBoardEvent Command { get; set; } = StoryBoardEvent.Fade;

        /// <summary>
        ///     将字符串解析成Fade
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            while (data.StartsWith(" "))
                data = data.Remove(0, 1);
            var parts = data.Split(',');
            if (parts[0] != "F") throw new ArgumentException("该行的数据不适用。");
            var eas = 0;
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
            var suc = int.TryParse(parts[1], out eas);
            if (suc) Easing = (StoryBoardEasing) eas;
            else Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            EndTime = int.Parse(parts[3]);
            var i = 4;
            var j = 1;
            if (i + 1 == parts.Length)
                Translations.Add(
                    new FadeTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime, EndTime));
            while (i + 1 < parts.Length)
            {
                double end = 0;
                var stindex = i;
                var st = double.Parse(parts[i++]);
                end = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex]);
                var dur = StartTime - EndTime;
                Translations.Add(new FadeTranslation(st, end, StartTime + dur * j, EndTime + dur * j));
                if (i + 1 < parts.Length) i--;
                j++;
            }
        }
    }
}