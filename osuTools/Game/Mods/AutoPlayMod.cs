namespace osuTools.Game.Mods
{
    /// <summary>
    /// 自动
    /// </summary>
    public class AutoPlayMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool IsRankedMod => false;

        /// <inheritdoc />
        public override string Name => "AutoPlay";

        /// <inheritdoc />
        public override string ShortName => "Auto";

        /// <inheritdoc />
        public override double ScoreMultiplier => 1.0d;
        /// <inheritdoc />
        public override ModType Type => ModType.Automation;
        /// <inheritdoc />
        public override string Description => "全自动游玩";
        /// <inheritdoc />

        public Mod[] ConflictMods => new Mod[]
        {
            new RelaxMod(), new AutoPilotMod(), new SpunOutMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod()
        };
        /// <inheritdoc />

        public OsuGameMod LegacyMod => OsuGameMod.AutoPlay;
    }
}