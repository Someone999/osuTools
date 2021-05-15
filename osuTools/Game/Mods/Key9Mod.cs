namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到9k
    /// </summary>
    public class Key9Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key9";
        /// <inheritdoc />
        public override string ShortName => "Key9";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key9;
    }
}