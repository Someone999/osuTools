namespace osuTools.Game.Mods
{
    /// <summary>
    /// 掉一个就死的Mod
    /// </summary>
    public class SuddenDeathMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "SuddenDeath";
        /// <inheritdoc />
        public override string ShortName => "SD";
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <summary>
        ///     与这个Mod相冲突的Mod
        /// </summary>
        public Mod[] ConflictMods => new Mod[] {new PerfectMod(), new NoFailMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.SuddenDeath;
    }
}