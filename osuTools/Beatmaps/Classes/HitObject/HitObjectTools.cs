using System;
using System.Collections.Generic;

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
        /// <returns></returns>
        public static List<T> GetGenericTypesByInt<T>(int bit) where T : Enum
        {
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

            while (cur > 0)
            {
                var log2 = Math.Log(cur, 2);
                var log2int = (int) Math.Truncate(log2);
                var value = (int) Math.Pow(2, log2int);
                if (typeof(T) != typeof(OsuGameMod))
                {
                    var rslt = (T) Enum.Parse(typeof(T), log2int.ToString());
                    lst.Add(rslt);
                }
                else
                {
                    lst.Add((T) Enum.Parse(typeof(T), value.ToString()));
                }

                cur -= value;
            }

            return lst;
        }
    }
}