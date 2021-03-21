namespace osuTools.Game.Mods
{
    public class TouchDeviceMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod => true;
        public override string Name { get; protected set; } = "TouchDevice";
        public override string ShortName { get; protected set; } = "TD";
        public override double ScoreMultiplier => 1.0;
        public override ModType Type => ModType.DifficultyReduction;
        public override string Description => "在触屏设备上游玩的时候会自动打开的Mod";
        public OsuGameMod LegacyMod => OsuGameMod.TouchDevice;
    }
}