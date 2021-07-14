using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osuTools.Attributes;
using osuTools.Beatmaps;
using osuTools.Exceptions;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    ///     Mod列表
    /// </summary>
    public class ModList:IEnumerable<Mod>
    {
        private readonly List<Mod> _mods = new List<Mod>();
        IEnumerator IEnumerable.GetEnumerator() => _mods.GetEnumerator();

        /// <summary>
        ///     Mod数组
        /// </summary>
        public Mod[] Mods => _mods.ToArray();

        /// <summary>
        ///     列表中所有Mod对分数的影响
        /// </summary>
        [AvailableVariable("Mods.ScoreMultiplier", "LANG_VAR_MODSCOREMULTIPLIER")]
        public double ScoreMultiplier
        {
            get;
            private set;
        }

        void CalcScoreMul()
        {
            if (_mods.Count == 0)
            {
                ScoreMultiplier = 1;
                return;
            }

            _mods.Sort((x, y) =>
                Math.Abs(x.ScoreMultiplier - y.ScoreMultiplier) < double.Epsilon ? 0 : x.ScoreMultiplier > y.ScoreMultiplier ? -1 : 1);
            var multiplier = _mods[0].ScoreMultiplier;
            double add = 0;
            if (_mods.Count > 1)
                for (var i = 1; i < _mods.Count; i++)
                {
                    var x = _mods[i].ScoreMultiplier;
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
            ScoreMultiplier = multiplier;
        }

        void CalcTimeRate()
        {
            TimeRate = 1;
            foreach (var m in _mods)
            {
                if (m is IChangeTimeRateMod changeTimeRateMod)
                    TimeRate *= changeTimeRateMod.TimeRate;
            }
        }

        void IsModsRanked()
        {
            IsRanked = _mods.All(m => m.IsRankedMod);
        }

        /// <summary>
        ///     列表中Mod对谱面速度的影响
        /// </summary>
        [AvailableVariable("Mods.TimeRate", "LANG_VAR_MOD_TIMERATE")]
        public double TimeRate
        {
            get;
            private set;
        } = 1;
        /// <summary>
        /// 获取或设置指定索引处的Mod
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Mod this[int x]
        {
            get => _mods[x];
            set => _mods[x] = value;
        }

        /// <summary>
        ///     列表中Mod的数量
        /// </summary>
       [AvailableVariable("Mods.Count","LANG_VAR_MOD")]
        public int Count => _mods.Count;

        /// <summary>
        ///     是否所有的Mod都为Ranked Mod
        /// </summary>
        [AvailableVariable("Mods.IsRanked", "LANG_VAR_MOD_ISRANKED")]
        public bool IsRanked
        {
            get;
            private set;
        }

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool HasMod(Type mod) => _mods.Any((m) => m.GetType() == mod);

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool HasMod(OsuGameMod mod)
        {
            foreach (var mod1 in _mods)
            {
                if(mod1 is ILegacyMod legacyMod)
                    if (legacyMod.LegacyMod == mod)
                        return true;
            }

            return false;
        }

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool Contains(Type mod)
        {
            return HasMod(mod);
        }

        /// <summary>
        ///     列表中是否含有指定Mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public bool Contains(OsuGameMod mod)
        {
            return HasMod(mod);
        }

        /// <summary>
        ///     添加Mod到列表
        /// </summary>
        /// <param name="item"></param>
        /// <param name="mode"></param>
        public void Add(Mod item,OsuGameMode? mode = null)
        {
            var comparer = new ModEqulityComparer();
            if (item != null)
            {
                if (item is IHasConflictMods spMod)
                {
                    var conflict = spMod.ConflictMods;
                    foreach (var m in _mods)
                        if (conflict.Contains(m, comparer))
                            throw new ConflictingModExistedException(item, m);
                }

                if (_mods.Contains(item, comparer))
                    throw new ModExsitedException(item);
                if (mode.HasValue)
                    item.CheckAndSetForMode(GameMode.FromLegacyMode(mode.Value));
                _mods.Add(item);
               RecalculateProperties();
            }
        }

        /// <summary>
        ///     从列表中移除指定Mod
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Mod item)
        {
            foreach (var mod in _mods)
                if (mod == item)
                    _mods.Remove(mod);
            RecalculateProperties();
        }

        /// <summary>
        ///     将ModList转换成<see cref="OsuGameMod" />数组
        /// </summary>
        /// <returns></returns>
        public OsuGameMod[] LegacyModList()
        {
            var m = new List<OsuGameMod>();
            foreach (var mod in _mods)
                if (mod is ILegacyMod l)
                    m.Add(l.LegacyMod);
            return m.ToArray();
        }

        /// <summary>
        ///     将包含Mod的整数分解成Mod并返回添加后的列表
        /// </summary>
        /// <param name="mod"></param>
        ///<param name="mode"></param>
        /// <returns></returns>
        public static ModList FromInteger(int mod,OsuGameMode? mode = null)
        {
            var mods = new ModList();
            if (mod == -1) return mods;
            var s = new string(Convert.ToString(mod, 2).Reverse().ToArray());
            for (var i = 0; i < s.Length; i++)
                if (s[i] == '1')
                {
                    var tmpMod = Mod.FromLegacyMod((OsuGameMod) (1 << i));
                    if (mode.HasValue)
                        tmpMod.CheckAndSetForMode(GameMode.FromLegacyMode(mode.Value));
                    if (tmpMod != null) 
                        mods.Add(tmpMod);
                }
            return mods;
        }

        /// <summary>
        ///     默认的排序方法
        /// </summary>
        public void Sort()
        {
            _mods.Sort();
        }

        /// <summary>
        ///     使用指定的比较方法对Mod进行排序
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<Mod> comparison)
        {
            _mods.Sort(comparison);
        }

        /// <summary>
        ///     使用指定的比较器对Mod进行排序
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<Mod> comparer)
        {
            _mods.Sort(comparer);
        }

        /// <summary>
        ///     使用指定的比较器对Mod进行排序
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<Mod> comparer)
        {
            _mods.Sort(index, count, comparer);
        }

        /// <summary>
        ///     移除指定位置的Mod
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _mods.RemoveAt(index);
            RecalculateProperties();
        }

        /// <summary>
        ///     清除列表中的所有Mod
        /// </summary>
        public void ClearMod()
        {
            _mods.Clear();
            ScoreMultiplier = 1;
            IsRanked = true;
            TimeRate = 1;
            AllowsFail = true;
        }

        /// <summary>
        ///     将<see cref="OsuGameMod" />分解成多个Mod并返回添加后的列表
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ModList FromLegacyMods(OsuGameMod mod,OsuGameMode? mode = null)
        {
            var mods = new ModList();
            var s = Convert.ToString((int) mod, 2);
            for (var i = 0; i < s.Length; i++)
                if (s[i] == '1')
                {
                    var tmpMod = Mod.FromLegacyMod((OsuGameMod) (1 << i));
                    if (mode.HasValue)
                        tmpMod.CheckAndSetForMode(GameMode.FromLegacyMode(mode.Value));
                    if (tmpMod != null)
                        if (tmpMod is IHasConflictMods spMod)
                        {
                            var conflict = spMod.ConflictMods;
                            if (!mods._mods.Any(m => conflict.Contains(m)))
                                mods.Add(tmpMod);
                        }
                }
            return mods;
        }

        /// <summary>
        ///     将Mod数组转换成ModList
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ModList FromModArray(Mod[] arr,OsuGameMode? mode = null)
        {
            var m = new ModList();
            foreach (var mod in arr)
            {
                m.Add(mod,mode);
            }
            m.RecalculateProperties();
            return m;
        }
        void RecalculateProperties()
        {
            CalcScoreMul();
            CalcTimeRate();
            GetAllowsFail();
        }

        /// <summary>
        ///     获取列表的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Mod> GetEnumerator()
        {
            return _mods.GetEnumerator();
        }

        /// <summary>
        ///     获取列表中所有Mod的名称
        /// </summary>
        /// <returns></returns>
        public string GetModsString()
        {
            if (_mods.Count == 0) return "None";
            var builder = new StringBuilder();
            for (var i = 0; i < _mods.Count; i++)
            {
                builder.Append(_mods[i].Name);
                if (i != _mods.Count - 1)
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
            if (_mods.Count == 0) return "None";
            var builder = new StringBuilder();
            for (var i = 0; i < _mods.Count; i++)
            {
                builder.Append(_mods[i].ShortName);
                if (i != _mods.Count - 1)
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
            _mods.ForEach(mod => mod.Apply(b));
            return b;
        }

        /// <summary>
        ///     将Mod转化成int的形式
        /// </summary>
        /// <returns></returns>
        public int ToIntMod()
        {
            var i = 0;
            foreach (var m in _mods)
                if (m is ILegacyMod mod)
                    i |= (int) mod.LegacyMod;
            return i;
        }
        /// <summary>
        /// 在当前的Mod下是否会失败
        /// </summary>
        public bool AllowsFail { get; private set; }
        void GetAllowsFail() => AllowsFail = _mods.Aggregate(true, (current, mod) => current && mod.AllowsFail());
    }
}