using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    /// 所有难度参数都提高一点，std模式会上下翻转谱面
    /// </summary>
    public class HardRockMod : Mod, ILegacyMod, IHasConflictMods
    {
        private bool _isRanked = true;
        private double _scoreMultiplier = 1.06d;

        /// <inheritdoc />
        public override bool IsRankedMod => _isRanked;
        /// <inheritdoc />
        public override string Name => "HardRock";
        /// <inheritdoc />
        public override string ShortName => "HR";
        /// <inheritdoc />
        public override double ScoreMultiplier => _scoreMultiplier;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "所有难度参数都提高一点";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new EasyMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.HardRock;
        /// <inheritdoc />

        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Mania) _scoreMultiplier = 1d;
            if (mode == OsuGameMode.Catch) _scoreMultiplier = 1.12d;
            return base.CheckAndSetForMode(mode) && true;
        }
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
            {
                _isRanked = false;
                _scoreMultiplier = 1d;
            }

            beatmap.ApproachRate *= 1.4;
            beatmap.OverallDifficulty *= 1.4;
            beatmap.HpDrain *= 1.4;
            if (beatmap.Mode == OsuGameMode.Osu || beatmap.Mode == OsuGameMode.Catch)
                beatmap.CircleSize *= 1.3;
            return beatmap;
        }
    }
}