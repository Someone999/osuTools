namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到5k
    /// </summary>
    public class Key5Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key5";
        /// <inheritdoc />
        public override string ShortName => "Key5";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key5;
    }
}