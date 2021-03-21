namespace osuTools.Game.Mods
{
    public class PerfectMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod => true;
        public override string Name => "Perfect";
        public override string ShortName => "PF";
        public override ModType Type => ModType.DifficultyIncrease;
        public override string Description => "感受痛苦吧";
        public Mod[] ConflictMods => new Mod[] {new SuddenDeathMod(), new NoFailMod()};
        public OsuGameMod LegacyMod => OsuGameMod.Perfect;
    }
}