namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到2k
    /// </summary>
    public class Key2Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key2";
        /// <inheritdoc />
        public override string ShortName => "Key2";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key2;
    }
}