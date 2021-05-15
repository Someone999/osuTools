namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到6k
    /// </summary>
    public class Key6Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key6";
        /// <inheritdoc />
        public override string ShortName => "Key6";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key6;
    }
}