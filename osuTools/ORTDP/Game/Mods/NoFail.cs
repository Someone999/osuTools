namespace osuTools.Game.Mods
{
    public class NoFailMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod => true;
        public override string Name { get; protected set; } = "NoFail";
        public override string ShortName { get; protected set; } = "NF";
        public override double ScoreMultiplier => 0.5;
        public override ModType Type => ModType.DifficultyReduction;
        public override string Description => "无论如何都不会失败";
        public Mod[] ConflictMods => new Mod[] {new SuddenDeathMod(), new PerfectMod()};
        public OsuGameMod LegacyMod => OsuGameMod.NoFail;
    }
}