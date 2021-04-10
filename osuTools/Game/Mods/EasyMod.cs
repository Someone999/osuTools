using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class EasyMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "Easy";
        /// <inheritdoc />
        public override string ShortName => "EZ";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.5d;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "所有的难度参数都降低一点，并有3次满血复活的机会";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new HardRockMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Easy;
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.ApproachRate /= 2;
            beatmap.HPDrain /= 2;
            beatmap.OverallDifficulty /= 2;
            if (beatmap.Mode == OsuGameMode.Osu || beatmap.Mode == OsuGameMode.Catch)
                beatmap.CircleSize /= 2;
            return beatmap;
        }
    }
}