﻿using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 计算<seealso cref="CatchBeatmap"/>的难度
    /// </summary>
    public class CatchDifficultyCalculator
    {
        /// <summary>
        /// 玩家的宽度
        /// </summary>
        public double PlayerWidth { get; }
        /// <summary>
        /// 所有的Tick和HitObject
        /// </summary>
        public List<ICatchHitObject> HitObjectsWithTicks { get; } = new List<ICatchHitObject>();
        /// <summary>
        /// 所有计算难度后的<seealso cref="CatchDifficultyHitObject"/>
        /// </summary>
        public List<CatchDifficultyHitObject> DifficultyHitObjects { get; } = new List<CatchDifficultyHitObject>();
        /// <summary>
        /// 使用的Mod
        /// </summary>
        public ModList Mods { get; }
        /// <summary>
        /// 计算难度的谱面
        /// </summary>
        public CatchBeatmap Beatmap { get; }
        /// <summary>
        /// Mod造成的时间倍率影响
        /// </summary>
        public double TimeRate { get; internal set; }
        /// <summary>
        /// 难度星级
        /// </summary>
        public double Stars { get; internal set; }
        /// <summary>
        /// 使用指定的<seealso cref="CatchBeatmap"/>和要使用的Mod初始化CatchDifficultyCalculator
        /// </summary>
        /// <param name="beatmap"></param>
        /// <param name="mods"></param>
        public CatchDifficultyCalculator(CatchBeatmap beatmap, ModList mods)
        {
            Beatmap = beatmap;
            Mods = mods;
            foreach (var diff in beatmap.Difficulty)
            {
                var scala = diff.Key == "CircleSize" ? 1.3 : 1.4;
                diff.Value = AdjustDifficulty(diff.Value, mods, scala);
            }
            foreach (var hitObject in Beatmap.CatchHitObjects)
            {
                var catchHitObj = (CatchHitObject)hitObject;
                HitObjectsWithTicks.Add(catchHitObj);
                if (catchHitObj.BaseHitObject.HitObjectType == HitObjectTypes.JuiceStream || catchHitObj.BaseHitObject.HitObjectType == HitObjectTypes.Slider)
                {
                    HitObjectsWithTicks.AddRange(catchHitObj.Ticks);
                    HitObjectsWithTicks.AddRange(catchHitObj.EndTicks);
                }
            }
            TimeRate = Mods.TimeRate;
            PlayerWidth = 305 / 1.6 *
                              ((102.4 * (1 - 0.7 * (Beatmap.Difficulty.CircleSize - 5) / 5)) / 128) * 0.7;
            HitObjectsWithTicks.ForEach(hitObject => DifficultyHitObjects.Add(new CatchDifficultyHitObject(hitObject, PlayerWidth * 0.4)));
            UpdateHyperDashDistance();
            DifficultyHitObjects.Sort((x, y) =>
                Math.Abs(x.HitObject.Offset - y.HitObject.Offset) == 0  ? 0 : x.HitObject.Offset > y.HitObject.Offset ? 1 : -1);
            CalcStrainValues();
            Stars = Math.Pow(CalcDifficulty(), 0.5) * Constants.StarScalingFactor;
        }
        double AdjustDifficulty(double diff,ModList mods,double scala)
        {
            if (mods.Contains(new EasyMod()))
            {
                diff = Math.Max(0, diff / 2);
            }
            if(mods.Contains(new HardRockMod()))
            {
                diff = Math.Min(10, diff * scala);
            }
            return diff;
        }
        void UpdateHyperDashDistance()
        {
            double lastDirecetion = 0d;
            double halfPlayerWidth = PlayerWidth / 2;
            halfPlayerWidth *= 0.8;
            double last = halfPlayerWidth;
            for (int i = 0; i < DifficultyHitObjects.Count - 1; i++)
            {
                var cur = DifficultyHitObjects[i];
                var nxt = DifficultyHitObjects[i + 1];
                double direction;
                if (nxt.HitObject.x > cur.HitObject.x)
                    direction = 1;
                else
                {
                    direction = -1;
                }
                double timeToNext = nxt.HitObject.Offset - cur.HitObject.Offset - 4.166667;
                double distanceToNext = Math.Abs(nxt.HitObject.x - cur.HitObject.x);
                if (Math.Abs(lastDirecetion - direction) == 0)
                {
                    distanceToNext -= last;
                }
                else
                    distanceToNext -= halfPlayerWidth;
                if (timeToNext < distanceToNext)
                {
                    cur.HyperDash = true;
                    last = halfPlayerWidth;
                }
                else
                {
                    cur.HyperdashDistance = timeToNext - distanceToNext;
                    last = MathUtlity.Clamp(cur.HyperdashDistance, 0, halfPlayerWidth);
                }
                lastDirecetion = direction;
            }
        }
        void CalcStrainValues()
        {
            var index = 1;
            var cur = DifficultyHitObjects[0];
            while (index < DifficultyHitObjects.Count)
            {
                var nextObject = DifficultyHitObjects[index];
                nextObject.CalcStrain(cur, TimeRate);
                cur = nextObject;
                index++;
            }
        }
        double CalcDifficulty()
        {
            double strainStep = Constants.StrainStep * TimeRate;
            List<double> highestStrain = new List<double>();
            double interval = strainStep;
            double maxStrain = 0;
            CatchDifficultyHitObject last = null;
            foreach (var difficultyHitObject in DifficultyHitObjects)
            {
                while (difficultyHitObject.HitObject.Offset > interval)
                {
                    highestStrain.Add(maxStrain);
                    if (last is null)
                        maxStrain = 0;
                    else
                    {
                        double decay = Math.Pow(Constants.DecayBase, (interval - last.HitObject.Offset) / 1000);
                        maxStrain = last.Strain * decay;
                    }
                    interval += strainStep;
                }
                if(difficultyHitObject.Strain>maxStrain)
                {
                    maxStrain = difficultyHitObject.Strain;
                }
                last = difficultyHitObject;
            }
            double difficulty = 0, weight = 1;
            var revserSortedList = from l in highestStrain orderby (int) l descending select l;
            foreach (var strain in revserSortedList)
            {
                difficulty += weight * strain;
                weight *= Constants.DecayWeight;
            }
            return difficulty;
        }
    }
}
