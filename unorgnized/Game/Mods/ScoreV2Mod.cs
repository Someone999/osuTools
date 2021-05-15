using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class ScoreV2Mod : Mod, ILegacyMod
    {
        private bool _isRanked;
        /// <inheritdoc />
        public override bool IsRankedMod => _isRanked;
        /// <inheritdoc />
        public override string Name => "ScoreV2";
        /// <inheritdoc />
        public override string ShortName => "V2";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1;
        /// <inheritdoc />
        public override ModType Type => ModType.Fun;
        /// <inheritdoc />
        public override string Description => "新版的计分方式";
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.ScoreV2;
        /// <inheritdoc />
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode is ManiaMode)
                _isRanked = true;
            return true;
        }
    }
}