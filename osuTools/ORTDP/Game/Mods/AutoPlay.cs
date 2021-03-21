namespace osuTools.Game.Mods
{
    public class AutoPlayMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get; protected set; } = false;
        public override string Name { get; protected set; } = "AutoPlay";
        public override string ShortName { get; protected set; } = "Auto";
        public override double ScoreMultiplier { get; protected set; } = 1.0d;
        public override ModType Type => ModType.Automation;
        public override string Description { get; protected set; } = "全自动游玩";

        public Mod[] ConflictMods => new Mod[]
        {
            new RelaxMod(), new AutoPilotMod(), new SpunOutMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod()
        };

        public OsuGameMod LegacyMod => OsuGameMod.AutoPlay;
    }
}