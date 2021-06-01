namespace osuTools.Game.Mods
{
    /// <summary>
    /// 自动转转盘
    /// </summary>
    public class SpunOutMod : Mod, ILegacyMod, IHasConflictMods
    {
        ///<inheritdoc/>
        public override string Description { get; protected set; } = "可以自动转转盘的Mod";

        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "SpunOut";
        /// <inheritdoc />
        public override string ShortName => "SP";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.9;
        /// <inheritdoc />
        public override ModType Type => ModType.Automation;
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[]
        {
            new AutoPilotMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod(), new NoFailMod()
        };
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.SpunOut;
    }
}