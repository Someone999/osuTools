using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;
using osuTools.Game.Mods;

namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     HitObject的工具类
    /// </summary>
    public static class HitObjectTools
    {
        /// <summary>
        ///     将整数分解，并解析成相应类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bit"></param>
        /// <param name="maybeType"></param>
        /// <returns></returns>
        public static List<T> GetGenericTypesByInt<T>(int bit,out HitObjectTypes? maybeType) where T : Enum
        {
            maybeType = null;
            var lst = new List<T>();
            var cur = bit;
            if (typeof(T) == typeof(HitSounds))
                if (cur == 0)
                    lst.Add((T) (object) HitSounds.Normal);
            if (typeof(T) == typeof(SampleSets))
                if (cur == 0)
                    lst.Add((T) (object) SampleSets.Default);
            if (typeof(T) == typeof(OsuGameMod))
                if (cur == 0)
                    lst.Add((T) (object) OsuGameMod.None);
            string bitStr = new string(Convert.ToString(bit, 2).Reverse().ToArray());
            for(int i = 0;i < bitStr.Length;i++)
            {
                if (bitStr[i] != '1') continue;
                if (typeof(T) != typeof(OsuGameMode))
                {
                    if (typeof(T) == typeof(HitObjectTypes))
                    {
                        if (i == 0 || i == 1 || i == 2 || i == 7)
                            maybeType = (HitObjectTypes?) i;
                    }
                    var rslt = (object)i;
                    lst.Add((T)rslt);
                }
                else
                {
                    lst.Add((T) (object) (1 << i));
                }
            }

            return lst;
        }

        /// <summary>
        /// 将整数分解，并解析成相应类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static List<T> GetGenericTypesByInt<T>(int bit) where T : Enum
            => GetGenericTypesByInt<T>(bit, out _);
    }
}