using System;
using osuTools.Beatmaps.HitObject.Sounds;

namespace osuTools.Beatmaps.HitObject
{
    public class BananaShower : IHitObject, INoteGrouped, IHasEndHitObject
    {
        private string hitsample;
        private int type;
        public int EndTime { get; internal set; }
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.BananaShower;
        public int Offset { get; set; } = -1;
        public OsuPixel Position { get; } = new OsuPixel(256, 192);
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        public HitSample HitSample { get; set; } = new HitSample();
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;

        public void Parse(string data)
        {
            var info = data.Split(',');
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
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

        public string ToOsuFormat()
        {
            return $"256,192,{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{EndTime},{hitsample}";
        }

        public bool IsNewGroup { get; set; }

        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}