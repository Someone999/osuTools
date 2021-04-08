namespace osuTools.Game.Mods
{
    public class KeyMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;

        /// <inheritdoc />
        public override string Name => "KeyMod";

        /// <inheritdoc />
        public override string ShortName => "KeyMod";

        /// <inheritdoc />
        public override ModType Type => ModType.Conversion;

        /// <inheritdoc />
        public override string Description => "将osu!谱转成指定键数的Mania谱";

        /// <inheritdoc />
        public virtual OsuGameMod LegacyMod => OsuGameMod.KeyMod;
    }
}