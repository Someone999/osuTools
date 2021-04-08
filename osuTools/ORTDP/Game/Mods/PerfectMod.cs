namespace osuTools.Game.Mods
{
    public class PerfectMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "Perfect";
        /// <inheritdoc />
        public override string ShortName => "PF";
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "感受痛苦吧";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new SuddenDeathMod(), new NoFailMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Perfect;
    }
}