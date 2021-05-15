namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到3k
    /// </summary>
    public class Key3Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key3";
        /// <inheritdoc />
        public override string ShortName => "Key3";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key3;
    }
}