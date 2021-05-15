namespace osuTools.Game.Mods
{
    /// <summary>
    /// 转换std谱面到7k
    /// </summary>
    public class Key7Mod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key7";
        /// <inheritdoc />
        public override string ShortName => "Key7";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.Key7;
    }
}