﻿using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 矢量缩放倍率
    /// </summary>
    public class VectorScaleMultiplier
    {
        public VectorScaleMultiplier(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double x { get; set; } = 1;
        public double y { get; set; } = 1;
    }
    /// <summary>
    /// 矢量缩放变化
    /// </summary>
    public class VectorScaleTranslation : ITranslation
    {
        public VectorScaleTranslation(VectorScaleMultiplier start, VectorScaleMultiplier tar, int sttm, int edtm)
        {
            StartScaleMultiplier = start;
            TargetScaleMultiplier = tar;
            StartTime = sttm;
            EndTime = edtm;
        }
        /// <summary>
        /// 起始状态
        /// </summary>
        public VectorScaleMultiplier StartScaleMultiplier { get; set; }
        /// <summary>
        /// 目标状态
        /// </summary>
        public VectorScaleMultiplier TargetScaleMultiplier { get; set; }
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
    }
    /// <summary>
    /// 矢量缩放
    /// </summary>
    public class VectorScale : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        /// <inheritdoc />
        public int StartTime { get; set; }
        /// <inheritdoc />
        public int EndTime { get; set; }
        /// <inheritdoc />
        public StoryBoardEasing Easing { get; set; }
        /// <inheritdoc />
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();
        /// <inheritdoc />
        public StoryBoardEvent Command { get; } = StoryBoardEvent.VectorScale;
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardCommand ParentCommand { get; set; }
        /// <inheritdoc />

        public void Parse(string line)
        {
            var datas = line.Split(',');
            var eas = 0;
            if (int.TryParse(datas[1], out eas))
                Easing = (StoryBoardEasing) eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(datas[1]);
            if (string.IsNullOrEmpty(datas[3])) datas[3] = datas[2];
            StartTime = int.Parse(datas[2]);
            EndTime = int.Parse(datas[3]);
            int i = 4, j = 0;
            if (i + 2 == datas.Length)
                Translations.Add(new VectorScaleTranslation(
                    new VectorScaleMultiplier(double.Parse(datas[4]), double.Parse(datas[5])),
                    new VectorScaleMultiplier(double.Parse(datas[4]), double.Parse(datas[5])),
                    StartTime, EndTime));
            while (i + 2 < datas.Length)
            {
                var stindex = i;
                var initindex = i;
                var xst = double.Parse(datas[i++]);
                var xed = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : stindex]);
                var yst = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : initindex]);
                var yed = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : initindex]);
                var dur = EndTime - StartTime;
                Translations.Add(new VectorScaleTranslation(new VectorScaleMultiplier(xst, xed),
                    new VectorScaleMultiplier(yst, yed), StartTime + j * dur, EndTime + j * dur));
                j++;
                if (i + 1 < datas.Length)
                    i -= 2;
            }
        }
    }
}