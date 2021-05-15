namespace osuTools.Game.Mods
{

    /// <summary>
    /// 触屏
    /// </summary>
    public class TouchDeviceMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name { get; protected set; } = "TouchDevice";
        /// <inheritdoc />
        public override string ShortName { get; protected set; } = "TD";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.0d;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "在触屏设备上游玩的时候会自动打开的Mod";
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.TouchDevice;
    }
}