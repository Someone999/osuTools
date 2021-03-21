namespace osuTools.Game.Mods
{
    public class ScoreV2Mod : Mod, ILegacyMod
    {
        public override bool IsRankedMod => true;
        public override string Name => "ScoreV2";
        public override string ShortName => "V2";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;
        public override string Description => "新版的计分方式";
        public OsuGameMod LegacyMod => OsuGameMod.ScoreV2;
    }
}