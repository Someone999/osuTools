namespace osuTools.Game.Mods
{
    /// <summary>
    /// 上隐
    /// </summary>
    public class FadeInMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "FadeIn";
        /// <inheritdoc />
        public override string ShortName => "FI";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1d;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "上隐";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new HiddenMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.FadeIn;

    }
}