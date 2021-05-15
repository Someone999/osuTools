namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到1k
    /// </summary>
    public class Key1Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key1";
        /// <inheritdoc />
        public override string ShortName => "Key1";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key1;
    }
}