namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到8k
    /// </summary>
    public class Key8Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key8";
        /// <inheritdoc />
        public override string ShortName => "Key8";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key8;
    }
}