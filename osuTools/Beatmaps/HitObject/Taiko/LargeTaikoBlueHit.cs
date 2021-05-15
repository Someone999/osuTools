using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Taiko
{
    /// <summary>
    ///     表示一个Taiko的外侧双打
    /// </summary>
    public class LargeTaikoBlueHit : ITaikoHit
    {
        private int type;

        /// <summary>
        ///     该打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.LargeTaikoBlueHit;

        /// <summary>
        ///     该打击物件相对于开始时的偏移
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     音效
        /// </summary>
        public HitSample HitSample { get; set; }

        /// <summary>
        ///     音效类型
        /// </summary>
        public HitSounds HitSound { get; } = HitSounds.Normal;

        /// <summary>
        ///     该打击物件会出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Taiko;

        /// <summary>
        ///     该属性对LargeTaikoBlueHit无效
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     使用字符串填充一个LargeTaikoBlueHit对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            type = int.Parse(info[3]);
            if (HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type).Contains(HitObjectTypes.HitCircle))
                if (info.Length > 5)
                    HitSample = new HitSample(info[5]);
        }

        /// <summary>
        ///     返回一个以osu文件中格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return $"{Position.x},{Position.y},{Offset},{1 << (int) HitObjectType},{0},{HitSample.GetData()}";
        }
        /// <inheritdoc />
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}