using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Mania
{
    /// <summary>
    ///     表示一个Mania长条
    /// </summary>
    public class ManiaHold : IManiaHitObject, IHasEndHitObject
    {
        private int type;

        /// <summary>
        ///     长条的结束时间
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        ///     打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType => HitObjectTypes.ManiaHold;

        /// <summary>
        ///     打击物件相对于开始的偏移
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     此属性对长条无效
        /// </summary>
        public OsuPixel Position { get; set; } = new OsuPixel(0, 0);

        /// <summary>
        ///     音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     该打击物件出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Mania;

        /// <summary>
        ///     音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     使用字符串构建一个ManiaHold对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            if (BeatmapColumn == 0)
                throw new ArgumentException();
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type,out _);
            if (!types.Contains(HitObjectTypes.ManiaHold))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            Column = (int) Math.Floor(Position.x * BeatmapColumn / 512d);
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]),out _)[0];
            var ainfo = info[5].Split(':');
            var eval = double.Parse(ainfo[0]);
            EndTime = double.IsNaN(eval) || double.IsInfinity(eval) ? 0 : (int) eval;
            if (ainfo.Length > 5)
                HitSample = new HitSample((SampleSets) int.Parse(ainfo[1]), (SampleSets) int.Parse(ainfo[2]),
                    int.Parse(ainfo[3]), int.Parse(ainfo[4]), ainfo[5]);
        }

        /// <summary>
        ///     以osu文件中的格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return
                $"{Position.x},{Position.y},{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{EndTime}:{HitSample.GetData()}";
        }

        /// <summary>
        ///     谱面键位数
        /// </summary>
        public int BeatmapColumn { get; set; } = 0;

        /// <summary>
        ///     长条所在行
        /// </summary>
        public int Column { get; set; }
        /// <inheritdoc />
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}