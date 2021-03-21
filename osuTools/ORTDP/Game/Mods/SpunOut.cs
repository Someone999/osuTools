namespace osuTools.Game.Mods
{
    public class SpunOutMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod
        {
            get => true;
            protected set => IsRankedMod = value;
        }

        public override string Name
        {
            get => "SpunOut";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "SP";
            protected set => ShortName = value;
        }

        public override double ScoreMultiplier
        {
            get => 0.9;
            protected set => ScoreMultiplier = value;
        }

        public override ModType Type => ModType.Automation;

        public Mod[] ConflictMods => new Mod[]
        {
            new RelaxMod(), new AutoPilotMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(),
            new PerfectMod(), new NoFailMod()
        };

        public OsuGameMod LegacyMod => OsuGameMod.SpunOut;
    }
}