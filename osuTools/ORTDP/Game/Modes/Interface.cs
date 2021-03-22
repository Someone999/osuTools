﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes.ScoreCalculators;
using osuTools.Game.Mods;
using RealTimePPDisplayer.Displayer;

namespace osuTools.Game
{
    public enum GameRanking
    {
        Unknown = -1, D, C, B, A, S, SS, SH, SSH
    }
}
namespace osuTools.Game.Modes
{

    /// <summary>
    ///     代表这个游戏模式存在于<see cref="OsuGameMode" />
    /// </summary>
    public interface ILegacyMode
    {
        /// <summary>
        ///     对应的<see cref="OsuGameMode" />
        /// </summary>
        OsuGameMode LegacyMode { get; }
    }

    public interface IHasPerformanceCalculator
    {
        void SetBeatmap(Beatmap b);
        double GetMaxPerformance(ORTDPWrapper ortdpInfo);
        double GetPerformance(ORTDPWrapper ortdpInfo);
        PPTuple GetPPTuple(ORTDPWrapper ortdpInfo);
    }

    public interface IHasScoreCalculator
    {
        ScoreCalculator GetScoreCalculator();
    }

    /// <summary>
    ///     分数的组成
    /// </summary>
    public class ScoreInfo
    {
        /// <summary>
        ///     300g的数量
        /// </summary>
        public int c300g { get; set; }

        /// <summary>
        ///     300的数量
        /// </summary>
        public int c300 { get; set; }

        /// <summary>
        ///     200的数量
        /// </summary>
        public int c200 { get; set; }

        /// <summary>
        ///     100的数量
        /// </summary>
        public int c100 { get; set; }

        /// <summary>
        ///     50的数量
        /// </summary>
        public int c50 { get; set; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        public int cMiss { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is ScoreInfo info)
            {
                return info.c300g == this.c300g && info.c300 == this.c300 && info.c200 == this.c200 && info.c100 == this.c100 && info.c50 == this.c50 &&
                       info.cMiss == this.cMiss;
            }
            return obj.Equals(this);
        }

        public override int GetHashCode()
        {
            return c300g * 6 + c300 * 5 + c200 * 4 + c100 * 3 + c50 * 2 + cMiss;
        }

        public static bool operator ==(ScoreInfo a, ScoreInfo b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.GetHashCode() == b.GetHashCode() && a.Equals(b);
        }

        public static bool operator !=(ScoreInfo a, ScoreInfo b)
        {

            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return a.GetHashCode() != b.GetHashCode() || !a.Equals(b);
        }
    }

    /// <summary>
    ///     表示一个游戏模式
    /// </summary>
    public abstract class GameMode : IEqualityComparer<GameMode>
    {
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
        public virtual double AccuracyCalc(ORTDPWrapper scoreInfo)
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

        public static bool operator ==(GameMode a, GameMode b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(a, b);
        }

        public static bool operator !=(GameMode a, GameMode b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return !a.Equals(a, b);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is GameMode) return Equals(this, obj);
            return obj.Equals(this);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        /// <summary>
        ///     将<see cref="ILegacyMode" />转换成GameMode
        /// </summary>
        /// <param name="legacyMode"></param>
        /// <returns></returns>
        public static GameMode FromLegacyMode(OsuGameMode legacyMode)
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
                if (type.GetInterfaces().Any(i => i == typeof(ILegacyMode)))
                {
                    var mode = (ILegacyMode) type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    if (mode.LegacyMode == legacyMode)
                        return (GameMode) mode;
                }

            return null;
        }

        public static bool operator ==(GameMode mode, OsuGameMode enumMode)
        {
            if (mode is ILegacyMode gamemode) return gamemode.LegacyMode == enumMode;
            return false;
        }

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
        public virtual int GetPassedHitObjectCount(ORTDPWrapper info)
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
        public virtual bool IsPerfect(ORTDPWrapper info)
        {
            return false;
        }

        /// <summary>
        ///     300g出现率的计算方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetC300gRate(ORTDPWrapper info)
        {
            return 0;
        }

        /// <summary>
        ///     300出现率的计算方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetC300Rate(ORTDPWrapper info)
        {
            return 0;
        }

        /// <summary>
        ///     当前的评级的判定方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual GameRanking GetRanking(ORTDPWrapper info)
        {
            return GameRanking.Unknown;
        }
    }
}