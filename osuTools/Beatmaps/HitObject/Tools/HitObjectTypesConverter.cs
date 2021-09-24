using System;
using System.Collections.Generic;

namespace osuTools.Beatmaps.HitObject.Tools
{
    /// <summary>
    /// 将int转换为<see cref="HitObjectTypes"/>列表的类
    /// </summary>
    public class HitObjectTypesConverter:IntToEnumListConverter<HitObjectTypes>
    {

        bool IsMainHitObjectType(int val) => val == 0 || val == 1 || val == 3 || val == 7; 
       
        /// <summary>
        /// 将int转换为<see cref="HitObjectTypes"/>列表
        /// </summary>
        /// <param name="val"></param>
        /// <param name="maybeBestVal"></param>
        /// <returns></returns>
        public override List<HitObjectTypes> Convert(int val,out HitObjectTypes? maybeBestVal)
        {
            maybeBestVal = null;
            if (val == 0)
                return new List<HitObjectTypes> {HitObjectTypes.HitCircle};
            List<HitObjectTypes> lst = new List<HitObjectTypes>();
            string bits = ToReversedBinary(val);
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != '1') 
                    continue;
                if (IsMainHitObjectType(i))
                {
                    if (maybeBestVal.HasValue)
                    {
                        throw new ArgumentException("Can not add two main type to a same list.");
                    }
                    maybeBestVal = (HitObjectTypes) i;
                }
                lst.Add((HitObjectTypes)i);
            }
            return lst;
        }
        ///<inheritdoc/>
        public override HitObjectTypes DefaultValue => HitObjectTypes.Unknown;
    }
}