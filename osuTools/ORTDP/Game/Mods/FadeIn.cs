namespace osuTools.Game.Mods
{
    public class FadeInMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod => true;
        public override string Name => "FadeIn";
        public override string ShortName => "FI";
        public override double ScoreMultiplier => 1d;
        public override ModType Type => ModType.DifficultyIncrease;
        public override string Description => "上隐";
        public Mod[] ConflictMods => new Mod[] {new HiddenMod()};
        public OsuGameMod LegacyMod => OsuGameMod.FadeIn;
    }
}