using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Catch
{
    /// <summary>
    /// Catch的香蕉雨
    /// </summary>
    public class BananaShower : INoteGrouped, IHasEndHitObject
    {
        private readonly string _hitsample = null;
        private int _type;

        /// <inheritdoc />
        public int EndTime { get; internal set; }

        /// <inheritdoc />
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.BananaShower;

        /// <inheritdoc />
        public int Offset { get; set; } = -1;
        /// <inheritdoc />
        public OsuPixel Position { get; } = new OsuPixel(256, 192);
        /// <inheritdoc />
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        /// <inheritdoc />
        public HitSample HitSample { get; set; } = new HitSample();
        /// <inheritdoc />
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;
        /// <inheritdoc />n
        public void Parse(string data)
        {
            var info = data.Split(',');
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            _type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(_type);
            if (!types.Contains(HitObjectTypes.Spinner))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            if (types.Contains(HitObjectTypes.NewCombo))
                IsNewGroup = true;
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
            EndTime = int.Parse(info[5]);
            if (info.Length > 6)
                HitSample = new HitSample(info[6]);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return $"256,192,{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{EndTime},{_hitsample}";
        }
        /// <inheritdoc/>
        public bool IsNewGroup { get; set; }
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}