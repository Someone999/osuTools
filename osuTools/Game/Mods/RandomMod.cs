namespace osuTools.Game.Mods
{
    /// <summary>
    /// 适用于Mania模式，随机排布所有Note
    /// </summary>
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