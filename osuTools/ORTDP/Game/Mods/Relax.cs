namespace osuTools.Game.Mods
{
    public class RelaxMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod => true;
        public override string Name => "Relax";
        public override string ShortName => "Relax";
        public override double ScoreMultiplier => 0.0;
        public override ModType Type => ModType.Automation;
        public override string Description => "自动按键，只需要定位";

        public Mod[] ConflictMods => new Mod[]
        {
            new AutoPilotMod(), new SpunOutMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod(), new NoFailMod()
        };

        public OsuGameMod LegacyMod => OsuGameMod.Relax;
    }
}