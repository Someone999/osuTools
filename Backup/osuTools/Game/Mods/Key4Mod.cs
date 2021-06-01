namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到4k
    /// </summary>
    public class Key4Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key4";
        /// <inheritdoc />
        public override string ShortName => "Key4";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key4;
    }
}