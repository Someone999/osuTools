using System.Collections.Generic;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

namespace osuTools.StoryBoard.Command
{
    public class MoveYTranslation : ITranslation
    {
        /// <summary>
        ///     使用变化开始时的Y坐标，变化结束时的Y坐标，开始时间和结束时间初始化一个MoveYTranslation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="sttm"></param>
        /// <param name="edtm"></param>
        public MoveYTranslation(double start, double target, int sttm, int edtm)
        {
            StartPoint = start;
            TargetPoint = target;
            StartTime = sttm;
            EndTime = edtm;
        }

        /// <summary>
        ///     起始点
        /// </summary>
        public double StartPoint { get; set; }

        /// <summary>
        ///     终点
        /// </summary>
        public double TargetPoint { get; set; }

        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }

    public class MoveY : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public StoryBoardEasing Easing { get; set; }
        public List<ITranslation> Translations { get; set; } = new List<ITranslation>();
        public StoryBoardEvent Command { get; } = StoryBoardEvent.MoveY;
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        public IStoryBoardCommand ParentCommand { get; set; }

        public void Parse(string line)
        {
            var parts = line.Split(',');
            var eas = 0;
            if (int.TryParse(parts[1], out eas))
                Easing = (StoryBoardEasing) eas;
            else
                Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
            StartTime = int.Parse(parts[2]);
            EndTime = int.Parse(parts[3]);
            int i = 4, j = 0;
            if (i + 1 == parts.Length)
                Translations.Add(new MoveYTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime,
                    EndTime));
            while (i + 1 < parts.Length)
            {
                var stindex = i;
                var st = double.Parse(parts[i++]);
                var end = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex]);
                var dur = EndTime - StartTime;
                Translations.Add(new MoveYTranslation(st, end, StartTime + j * dur, EndTime + j * dur));
                j++;
                if (i + 1 < parts.Length)
                    i--;
            }
        }
    }
}