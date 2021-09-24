using System.Collections.Generic;
using osuTools.Beatmaps.HitObject.Sounds;

namespace osuTools.Beatmaps.HitObject.Tools
{
    /// <summary>
    /// 将int转换为<see cref="HitSounds"/>列表的类
    /// </summary>
    public class HitSoundsConverter : IntToEnumListConverter<HitSounds>
    {
        /// <summary>
        /// 将int转换为<see cref="HitSounds"/>列表，在此转换中，maybeBestVal恒为null
        /// "maybeBestVal" will always be null in this conversion.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="maybeBestVal"></param>
        /// <returns></returns>
        public override List<HitSounds> Convert(int val, out HitSounds? maybeBestVal)
        {
            maybeBestVal = null;
            if (val == 0)
                return new List<HitSounds> { DefaultValue };
            List<HitSounds> lst = new List<HitSounds>();
            string bits = ToReversedBinary(val);
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != '1')
                    continue;
                lst.Add((HitSounds)i);
            }
            return lst;
        }
        /// <inheritdoc/>
        public override HitSounds DefaultValue => HitSounds.Normal;
    }
}