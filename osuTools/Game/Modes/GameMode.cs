using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;

namespace osuTools.Game.Modes
{
    /// <summary>
    ///     表示一个游戏模式
    /// </summary>
    public abstract class GameMode : IEqualityComparer<GameMode>
    {
        /// <summary>
        /// 存储<seealso cref="OsuGameMode"/>和<seealso cref="GameMode"/>的字典
        /// </summary>
        public static IReadOnlyDictionary<OsuGameMode, GameMode> LegacyModes
        {
            get
            {
                if(_legacyGameModes is null)
                {
                    Dictionary<OsuGameMode, GameMode> legacyModes = new Dictionary<OsuGameMode, GameMode>();
                    Assembly asm = typeof(GameMode).Assembly;
                    var types = asm.GetTypes();
                    foreach (var type in types)
                    {
                        var interfaces = type.GetInterfaces();
                        if (interfaces.Any(i => i == typeof(ILegacyMode)))
                        {
                            var legacyMode = type.GetConstructor(new Type[0])?.Invoke(new object[0]) as ILegacyMode;
                            GameMode m = legacyMode as GameMode;
                            if (!(legacyMode is null))
                                legacyModes.Add(legacyMode.LegacyMode, m ?? throw new InvalidCastException());
                        }
                    }
                    _legacyGameModes = new ReadOnlyDictionary<OsuGameMode, GameMode>(legacyModes);
                }
                return _legacyGameModes;
            }
        }

        private static IReadOnlyDictionary<OsuGameMode, GameMode> _legacyGameModes;

        /// <summary>
        ///     模式的名字
        /// </summary>
        public virtual string ModeName { get; protected set; } = "";

        /// <summary>
        ///     模式的描述
        /// </summary>
        public virtual string Description { get; protected set; } = "";

        /// <summary>
        ///     可用的Mod
        /// </summary>
        public virtual Mod[] AvaliableMods { get; protected internal set; }

        /// <summary>
        ///     比较两个模式是否为同一个模式
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(GameMode a, GameMode b)
        {
            if (a is ILegacyMode && b is ILegacyMode)
                return ((ILegacyMode) a).LegacyMode == ((ILegacyMode) b).LegacyMode;
            return a.ModeName == b.ModeName;
        }

        /// <summary>
        ///     获取模式的Hash。如果模式为<see cref="ILegacyMode" />则返回对应的枚举值，否则返回模式名称的Hash。
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int GetHashCode(GameMode a)
        {
            if (a is ILegacyMode)
                return (int) (a as ILegacyMode).LegacyMode;
            return a.ModeName.GetHashCode();
        }

        /// <summary>
        ///     创建一个对应模式的<see cref="IHitObject" />
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual IHitObject CreateHitObject(string data)
        {
            throw new NotImplementedException($"模式{ModeName}不使用这个方法创建HitObject");
        }

        /// <summary>
        ///     创建一个包含列数的对应模式的<see cref="IHitObject" />
        /// </summary>
        /// <param name="data"></param>
        /// <param name="stageColumns"></param>
        /// <returns></returns>
        public virtual IHitObject CreateHitObject(string data, int stageColumns)
        {
            throw new NotImplementedException($"模式{ModeName}不使用这个方法创建HitObject");
        }

        /// <summary>
        ///     这个模式的准度计算方法
        /// </summary>
        /// <param name="scoreInfo"></param>
        /// <returns></returns>
        public virtual double AccuracyCalc(OrtdpWrapper.OrtdpWrapper scoreInfo)
        {
            return 0;
        }

        /// <summary>
        ///     这个模式的准度计算方法
        /// </summary>
        /// <param name="scoreInfo"></param>
        /// <returns></returns>
        public virtual double AccuracyCalc(ScoreInfo scoreInfo)
        {
            return 0;
        }
        /// <summary>
        /// 判断两个GameMode是否为同一模式
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(GameMode a, GameMode b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(a, b);
        }
        /// <summary>
        /// 判断两个GameMode是否为同一模式
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(GameMode a, GameMode b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return !a.Equals(a, b);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is GameMode) return Equals(this, obj);
            return obj.Equals(this);
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        /// <summary>
        ///     将<see cref="ILegacyMode" />转换成GameMode
        /// </summary>
        /// <param name="legacyMode"></param>
        /// <returns></returns>
        public static GameMode FromLegacyMode(OsuGameMode legacyMode) => LegacyModes[legacyMode] ?? new UnknownMode();

        /// <summary>
        /// 判断GameMode和指定的OsuGameMode是否为同一模式
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="enumMode"></param>
        /// <returns></returns>
        public static bool operator ==(GameMode mode, OsuGameMode enumMode)
        {
            if (mode is ILegacyMode gamemode) return gamemode.LegacyMode == enumMode;
            return false;
        }
        /// <summary>
        /// 判断GameMode和指定的OsuGameMode是否为同一模式
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="enumMode"></param>
        /// <returns></returns>
        public static bool operator !=(GameMode mode, OsuGameMode enumMode)
        {
            if (mode is ILegacyMode gamemode) return gamemode.LegacyMode != enumMode;
            return true;
        }

        /// <summary>
        ///     获取谱面的HitObject数量
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual int GetBeatmapHitObjectCount(Beatmap b)
        {
            return 0;
        }

        /// <summary>
        ///     获取已经经过了的HitObject的数量
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual int GetPassedHitObjectCount(OrtdpWrapper.OrtdpWrapper info)
        {
            return 0;
        }

        /// <summary>
        ///     返回Mode的名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ModeName;
        }

        /// <summary>
        ///     判断成绩是否达到Perfect判定
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual bool IsPerfect(OrtdpWrapper.OrtdpWrapper info)
        {
            return false;
        }

        /// <summary>
        ///     300g出现率的计算方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetCountGekiRate(OrtdpWrapper.OrtdpWrapper info)
        {
            return 0;
        }

        /// <summary>
        ///     300出现率的计算方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetCount300Rate(OrtdpWrapper.OrtdpWrapper info)
        {
            return 0;
        }

        /// <summary>
        ///     当前的评级的判定方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual GameRanking GetRanking(OrtdpWrapper.OrtdpWrapper info)
        {
            return GameRanking.Unknown;
        }
    }
}