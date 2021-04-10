namespace osuTools.Game.Mods
{
    public class RelaxMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => false;
        /// <inheritdoc />
        public override string Name => "Relax";
        /// <inheritdoc />
        public override string ShortName => "Relax";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.0;
        /// <inheritdoc />
        public override ModType Type => ModType.Automation;
        /// <inheritdoc />
        public override string Description => "自动按键，只需要定位";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[]
        {
            new AutoPilotMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod(), new NoFailMod()
        };
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Relax;
    }
}