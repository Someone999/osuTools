namespace osuTools.Game.Mods
{
    public class RandomMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod => false;
        public override string Name => "Random";
        public override string ShortName => "RD";
        public override ModType Type => ModType.DifficultyIncrease;
        public override string Description => "随机排列Mania Note";
        public OsuGameMod LegacyMod => OsuGameMod.Random;
    }
}