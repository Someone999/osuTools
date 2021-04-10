using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class FlashlightMod : Mod, ILegacyMod, IHasConflictMods
    {
        private double _scoreMultiplier = 1.12d;
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "Flashlight";
        /// <inheritdoc />
        public override string ShortName => "FL";
        /// <inheritdoc />
        public override double ScoreMultiplier => _scoreMultiplier;

        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />

        public override string Description => "极限视野";
        /// <inheritdoc />
        public Mod[] ConflictMods { get; set; } = new Mod[0];
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Flashlight;
        /// <inheritdoc />
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Mania)
            {
                _scoreMultiplier = 1d;
                ConflictMods = new Mod[] {new HiddenMod(), new FadeInMod()};
            }

            if (mode == OsuGameMode.Catch) _scoreMultiplier = 1.06d;
            return base.CheckAndSetForMode(mode) && true;
        }
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            CheckAndSetForMode(GameMode.FromLegacyMode(beatmap.Mode));
            return beatmap;
        }
    }
}