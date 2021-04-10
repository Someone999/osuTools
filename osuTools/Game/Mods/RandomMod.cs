namespace osuTools.Game.Mods
{
    public class RandomMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => false;
        /// <inheritdoc />
        public override string Name => "Random";
        /// <inheritdoc />
        public override string ShortName => "RD";
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "随机排列Mania Note";
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Random;
    }
}