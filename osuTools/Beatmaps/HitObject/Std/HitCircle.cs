using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Std
{
    /// <summary>
    ///     表示一个圈圈
    /// </summary>
    public class HitCircle : IHitObject, INoteGrouped
    {
        private int _type;

        /// <summary>
        ///     打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType => HitObjectTypes.HitCircle;

        /// <summary>
        ///     打击物件相对于开始时的偏移
        /// </summary>
        public int Offset { get; set; } = -1;

        /// <summary>
        ///     打击物件的音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     打击物件的音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     打击物件的位置
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     会出现该打击物件的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Osu;

        /// <summary>
        ///     将字符串解析成HitCircle
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            _type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(_type);
            if (!types.Contains(HitObjectTypes.HitCircle))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            if (types.Contains(HitObjectTypes.NewCombo))
                IsNewGroup = true;
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
            if (info.Length > 5)
                HitSample = new HitSample(info[5]);
        }

        /// <summary>
        ///     返回一个以osu格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return
                $"{Position.x},{Position.y},{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{HitSample.GetData()}";
        }
        /// <inheritdoc />
        public bool IsNewGroup { get; set; }
        /// <inheritdoc />
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}