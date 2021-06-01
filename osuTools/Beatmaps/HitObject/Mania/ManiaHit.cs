using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Mania
{
    /// <summary>
    ///     表示Mania中的一个Note
    /// </summary>
    public class ManiaHit : IManiaHitObject
    {
        private int IntType { get; set; }

        /// <summary>
        ///     该打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.ManiaHit;

        /// <summary>
        ///     该打击物件相对于开始的偏移
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     音效的类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     该打击物件出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Mania;

        /// <summary>
        ///     自定义音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     此属性对ManiaHit无效
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     使用字符串构建一个ManiaHit对象
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
            IntType = int.Parse(info[3]); 
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(IntType);
            if (!types.Contains(HitObjectTypes.HitCircle)) 
            {
                throw new ArgumentException("该行的数据不适用。"); 
            }

            Column = (int) Math.Floor(Position.x * BeatmapColumn / 512d);
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0]; 
            if (info.Length > 5)
                HitSample = new HitSample(info[5]);
        }

        /// <summary>
        ///     以osu文件中的数据为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return
                $"{Position.x},{Position.y},{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{HitSample.GetData()}";
        }

        /// <summary>
        ///     该Note所在的行
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        ///     谱面的键位数
        /// </summary>
        public int BeatmapColumn { get; set; }

        internal void SetBeatmapColumn(int column)
        {
            BeatmapColumn = column >= 1 && column <= 10 ? column : -1;
        }
        /// <inheritdoc />
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}