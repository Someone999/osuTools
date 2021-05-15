using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osuTools.Attributes;
using osuTools.Beatmaps;
using osuTools.Exceptions;

namespace osuTools.Game.Mods
{
    /// <summary>
    ///     Mod列表
    /// </summary>
    public class ModList:IEnumerable<Mod>
    {
        private List<Mod> mods = new List<Mod>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mods.GetEnumerator();
        }

        /// <summary>
        ///     Mod数组
        /// </summary>
        public Mod[] Mods => mods.ToArray();

        /// <summary>
        ///     列表中所有Mod对分数的影响
        /// </summary>
        [AvailableVariable("Mods.ScoreMultiplier", "LANG_VAR_MODSCOREMULTIPLIER")]
        public double ScoreMultiplier
        {
            get
            {
                if (mods.Count == 0) return 1;
                mods.Sort((x, y) =>
                    Math.Abs(x.ScoreMultiplier - y.ScoreMultiplier) < double.Epsilon ? 0 : x.ScoreMultiplier > y.ScoreMultiplier ? -1 : 1);
                var multiplier = mods[0].ScoreMultiplier;
                double add = 0;
                if (mods.Count > 1)
                    for (var i = 1; i < mods.Count; i++)
                    {
                        var x = mods[i].ScoreMultiplier;
                        if (x > 1)
                        {
                            if (multiplier > 1)
                                multiplier += x - 1;
                            else
                                multiplier += (x - 1) / 2;
                            if (multiplier > 1.3) add = 0.02;
                            else if (multiplier > 1.2) add = 0.01;
                        }

                        if (x < 1) multiplier *= x;
                    }

                if (multiplier > 1.3) multiplier += 0.03;
                else if (multiplier > 1.15) multiplier += 0.01;
                if (multiplier >= 1.39) multiplier += 0.02;
                if (multiplier < 1)
                    multiplier += add;
                multiplier = double.Parse(multiplier.ToString("f2"));
                return multiplier;
            }
        }

        /// <summary>
        ///     列表中Mod对谱面速度的影响
        /// </summary>
        [AvailableVariable("Mods.TimeRate", "LANG_VAR_MOD_TIMERATE")]
        public double TimeRate
        {
            get
            {
                foreach (var mod in mods)
                    if (mod is IChangeTimeRateMod cmod)
                        return cmod.TimeRate;
                return 1;
            }
        }

        public Mod this[int x]
        {
            get => mods[x];
            set => mods[x] = value;
        }

        /// <summary>
        ///     列表中Mod的数量
        /// </summary>
       [AvailableVariable("Mods.Count","LANG_VAR_MOD")]
        public int Count => mods.Count;

        /// <summary>
        ///     是否所有的Mod都为Ranked Mod
        /// </summary>
        [AvailableVariable("Mods.IsRanked", "LANG_VAR_MOD_ISRANKED")]
        public bool IsRanked => mods.All(mod => mod.IsRankedMod);

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool HasMod(Mod mod)
        {
            var comparer = new ModEqulityComparer();
            return mods.Contains(mod, comparer);
        }

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool Contains(Mod mod)
        {
            return HasMod(mod);
        }

        /// <summary>
        ///     添加Mod到列表
        /// </summary>
        /// <param name="item"></param>
        public void Add(Mod item)
        {
            var comparer = new ModEqulityComparer();
            if (item != null)
            {
                if (item is IHasConflictMods spMod)
                {
                    var conflict = spMod.ConflictMods;
                    foreach (var m in mods)
                        if (conflict.Contains(m, comparer))
                            throw new ConflictingModExistedException(item, m);
                }

                if (mods.Contains(item, comparer))
                    throw new ModExsitedException(item);
                mods.Add(item);
            }
        }

        /// <summary>
        ///     从列表中移除指定Mod
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Mod item)
        {
            foreach (var mod in mods)
                if (mod == item)
                    mods.Remove(mod);
        }

        /// <summary>
        ///     将ModList转换成<see cref="OsuGameMod" />数组
        /// </summary>
        /// <returns></returns>
        public OsuGameMod[] LegacyModList()
        {
            var m = new List<OsuGameMod>();
            foreach (var mod in mods)
                if (mod is ILegacyMod l)
                    m.Add(l.LegacyMod);
            return m.ToArray();
        }

        /// <summary>
        ///     将包含Mod的整数分解成Mod并返回添加后的列表
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static ModList FromInteger(int mod)
        {
            var mods = new ModList();
            if (mod == -1) return mods;
            var s = new string(Convert.ToString(mod, 2).Reverse().ToArray());
            //Sync.Tools.IO.CurrentIO.Write($"[osuTools::ModList] ModString:{s}.");
            for (var i = 0; i < s.Length; i++)
                if (s[i] == '1')
                {
                    var tmpMod = Mod.FromLegacyMod((OsuGameMod) (1 << i));
                    if (tmpMod != null) mods.Add(tmpMod);
                }

            return mods;
        }

        /// <summary>
        ///     默认的排序方法
        /// </summary>
        public void Sort()
        {
            mods.Sort();
        }

        /// <summary>
        ///     使用指定的比较方法对Mod进行排序
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<Mod> comparison)
        {
            mods.Sort(comparison);
        }

        /// <summary>
        ///     使用指定的比较器对Mod进行排序
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<Mod> comparer)
        {
            mods.Sort(comparer);
        }

        /// <summary>
        ///     使用指定的比较器对Mod进行排序
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<Mod> comparer)
        {
            mods.Sort(index, count, comparer);
        }

        /// <summary>
        ///     移除指定位置的Mod
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            mods.RemoveAt(index);
        }

        /// <summary>
        ///     清除列表中的所有Mod
        /// </summary>
        public void ClearMod()
        {
            mods.Clear();
        }

        /// <summary>
        ///     将<see cref="OsuGameMod" />分解成多个Mod并返回添加后的列表
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static ModList FromLegacyMods(OsuGameMod mod)
        {
            var mods = new ModList();
            var s = Convert.ToString((int) mod, 2);
            for (var i = 0; i < s.Length; i++)
                if (s[i] == '1')
                {
                    var tmpMod = Mod.FromLegacyMod((OsuGameMod) (2 << i));
                    if (tmpMod != null)
                        if (tmpMod is IHasConflictMods spMod)
                        {
                            var conflict = spMod.ConflictMods;
                            if (!mods.mods.Any(m => conflict.Contains(m)))
                                mods.Add(tmpMod);
                        }
                }

            return mods;
        }

        /// <summary>
        ///     将Mod数组转换成ModList
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static ModList FromModArray(Mod[] arr)
        {
            var m = new ModList();
            m.mods = new List<Mod>(arr);
            return m;
        }

        /// <summary>
        ///     获取列表的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Mod> GetEnumerator()
        {
            return mods.GetEnumerator();
        }

        /// <summary>
        ///     获取列表中所有Mod的名称
        /// </summary>
        /// <returns></returns>
        public string GetModsString()
        {
            if (mods.Count == 0) return "None";
            var builder = new StringBuilder();
            for (var i = 0; i < mods.Count; i++)
            {
                builder.Append(mods[i].Name);
                if (i != mods.Count - 1)
                    builder.Append(",");
            }

            return builder.ToString();
        }

        /// <summary>
        ///     获取列表中所有Mod的名称的缩写
        /// </summary>
        /// <returns></returns>
        public string GetShortModsString()
        {
            if (mods.Count == 0) return "None";
            var builder = new StringBuilder();
            for (var i = 0; i < mods.Count; i++)
            {
                builder.Append(mods[i].ShortName);
                if (i != mods.Count - 1)
                    builder.Append(",");
            }

            return builder.ToString();
        }

        /// <summary>
        ///     将列表中的所有Mod应用到谱面
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Beatmap ApplyAllMods(Beatmap b)
        {
            mods.ForEach(mod => mod.Apply(b));
            return b;
        }

        /// <summary>
        ///     将Mod转化成int的形式
        /// </summary>
        /// <returns></returns>
        public int ToIntMod()
        {
            var i = 0;
            foreach (var m in mods)
                if (m is ILegacyMod mod)
                    i |= (int) mod.LegacyMod;
            return i;
        }
    }
}