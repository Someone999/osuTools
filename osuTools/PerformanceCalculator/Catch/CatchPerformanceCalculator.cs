using System;
using osuTools.Game.Mods;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// Catch模式的pp计算器，pp算法已经过时
    /// </summary>
    [Obsolete("这个类使用的计算方法已经过时")]
    public class CatchPerformanceCalculator
    {
        /// <summary>
        /// 难度计算器
        /// </summary>
        public CatchDifficultyCalculator DifficultyCalculator { get; internal set; }
        /// <summary>
        /// 计算pp的谱面
        /// </summary>
        public CatchBeatmap Beatmap { get; internal set; }
        /// <summary>
        /// 使用的Mod
        /// </summary>
        public ModList Mods { get; internal set; }
        /// <summary>
        /// 使用一个难度计算器初始化pp计算器
        /// </summary>
        /// <param name="calculator"></param>
        public CatchPerformanceCalculator(CatchDifficultyCalculator calculator)
        {
            DifficultyCalculator = calculator;
            Beatmap = calculator.Beatmap;
            Mods = calculator.Mods;
        }
        /// <summary>
        /// 使用要计算的谱面和要使用的Mod初始化pp计算器
        /// </summary>
        /// <param name="beatmap"></param>
        /// <param name="mods"></param>
        public CatchPerformanceCalculator(CatchBeatmap beatmap, ModList mods)
        {
            Beatmap = beatmap;
            Mods = mods;
            DifficultyCalculator = new CatchDifficultyCalculator(beatmap, mods);

        }
        /// <summary>
        /// 计算pp
        /// </summary>
        /// <param name="accuracy">准确度</param>
        /// <param name="combo">达到过的最大连击</param>
        /// <param name="cMiss">Miss的数量</param>
        /// <returns></returns>
        public double CalculatePerformance(double accuracy, int combo, int cMiss)
        {
            double pp = Math.Pow(((5 * DifficultyCalculator.Stars / 0.0049) - 4), 2) / 100000;
            double lenBonus = 0.95 + 0.4 * Math.Min(1, combo / 3000d);
            
            if (combo > 3000)
                lenBonus += Math.Log10(combo / 3000d) * 0.5;
            pp *= lenBonus;
            pp *= Math.Pow(0.97, cMiss);
            
            pp *= Math.Min(Math.Pow(combo, 0.8) / Math.Pow(DifficultyCalculator.Beatmap.MaxCombo, 0.8), 1);
            if (Beatmap.BaseBeatmap.ApproachRate > 9)
                pp *= 1 + 0.1 * (Beatmap.BaseBeatmap.ApproachRate - 9);
            if (Beatmap.BaseBeatmap.ApproachRate < 8)
                pp *= 1 + 0.025 * (8 - Beatmap.BaseBeatmap.ApproachRate);
            if (Mods.Contains(typeof(HiddenMod)))
                pp *= 1.05 + 0.075 * (10 - Math.Min(10, Beatmap.BaseBeatmap.ApproachRate));
            if (Mods.Contains(typeof (FlashlightMod)))
                pp *= 1.35 * lenBonus;
            pp *= Math.Pow(accuracy, 5.5);
            if (Mods.Contains(typeof (NoFailMod)))
                pp *= 0.9;
            if (Mods.Contains(typeof(SpunOutMod)))
                pp *= 0.95;
            return pp;
        }

    }
}
