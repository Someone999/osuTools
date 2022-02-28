﻿using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    /// 0.75倍速
    /// </summary>
    public class HalfTimeMod : Mod, ILegacyMod, IChangeTimeRateMod,IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "HalfTime";
        /// <inheritdoc />
        public override string ShortName => "HT";
        /// <inheritdoc />
        public override double ScoreMultiplier => _scoreMul;
        double _scoreMul = 0.3d;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "0.75倍速";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new DoubleTimeMod(), new NightCoreMod()};
        /// <inheritdoc />
        public double TimeRate => 0.75d;
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.HalfTime;
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode is ManiaMode)
            {
                _scoreMul = 0.5;
            }
            return true;
        }
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.HitObjects.ForEach(h => h.Offset = (int) (h.Offset / 0.75));
            return beatmap;
        }
    }
}