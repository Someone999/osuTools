using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Std
{
    /// <summary>
    ///     表示一个转盘
    /// </summary>
    public class Spinner : IHitObject, INoteGrouped, IHasEndHitObject
    {
        private string type;

        /// <summary>
        ///     Spinner的结束时间
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        ///     Note的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.Spinner;

        /// <summary>
        ///     Note相对于开始的位置
        /// </summary>
        public int Offset { get; set; } = -1;

        /// <summary>
        ///     Note的音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     Note的音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     Note的位置
        /// </summary>
        public OsuPixel Position { get; } = new OsuPixel(256, 192);

        /// <summary>
        ///     Note会出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Osu;

        /// <summary>
        ///     使用字符串构造一个Spinner对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            this.type = info[3];
            var type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
            if (!types.Contains(HitObjectTypes.Spinner))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            if (types.Contains(HitObjectTypes.NewCombo))
                IsNewGroup = true;
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
            var eval = double.Parse(info[5]);
            EndTime = double.IsNaN(eval) || double.IsInfinity(eval) ? 0 : (int) eval;
            if (info.Length > 6)
                HitSample = new HitSample(info[6]);
        }

        /// <summary>
        ///     获取以osu文件中的描述为格式的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return $"256,192,{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{EndTime},{HitSample.GetData()}";
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