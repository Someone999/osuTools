namespace osuTools.Game.Mods
{
    public class AutoPilotMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => false;
        /// <inheritdoc />
        public override string Name => "AutoPilot";
        /// <inheritdoc />
        public override string ShortName => "AP";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0d;
        /// <inheritdoc />
        public override string Description => "光标会自动移动，只需要按键";
        /// <inheritdoc />
        public override ModType Type => ModType.Automation;
        /// <inheritdoc />

        public Mod[] ConflictMods => new Mod[]
        {
            new RelaxMod(), new SpunOutMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod(), new NoFailMod()
        };
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.AutoPilot;
    }
}