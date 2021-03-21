namespace osuTools.Game.Mods
{
    public class MirrorMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod => true;
        public override string Name => "Mirror";
        public override string ShortName => "MR";
        public override string Description => "左右翻转Mania谱面";
        public OsuGameMod LegacyMod => OsuGameMod.Mirror;
    }
}