using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.Game.Modes;
using osuTools.Game.Mods;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchPerformanceCalculator
    {
        public CatchDifficultyCalculator DifficultyCalculator { get; internal set; }
        public CatchBeatmap Beatmap { get; internal set; }
        public ModList Mods { get; internal set; }
        public CatchPerformanceCalculator(CatchDifficultyCalculator calculator)
        {
            DifficultyCalculator = calculator;
            Beatmap = calculator.Beatmap;
            Mods = calculator.Mods;
        }
        public CatchPerformanceCalculator(CatchBeatmap beatmap, ModList mods)
        {
            Beatmap = beatmap;
            Mods = mods;
            DifficultyCalculator = new CatchDifficultyCalculator(beatmap, mods);

        }

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
            if (Mods.Contains(new HiddenMod()))
                pp *= 1.05 + 0.075 * (10 - Math.Min(10, Beatmap.BaseBeatmap.ApproachRate));
            if (Mods.Contains(new FlashlightMod()))
                pp *= 1.35 * lenBonus;
            pp *= Math.Pow(accuracy, 5.5);
            if (Mods.Contains(new NoFailMod()))
                pp *= 0.9;
            if (Mods.Contains(new SpunOutMod()))
                pp *= 0.95;
            return pp;
        }

    }
}
