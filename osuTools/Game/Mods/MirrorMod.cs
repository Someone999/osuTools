namespace osuTools.Game.Mods
{
    /// <summary>
    /// 适用于Mania模式，镜像翻转谱面
    /// </summary>
    public class MirrorMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "Mirror";
        /// <inheritdoc />
        public override string ShortName => "MR";
        /// <inheritdoc />
        public override string Description => "左右翻转Mania谱面";
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Mirror;
    }
}