using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class HiddenMod : Mod, ILegacyMod, IHasConflictMods
    {
        private double _scoreMultiplier = 1.06d;
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "Hidden";
        /// <inheritdoc />
        public override string ShortName => "HD";
        /// <inheritdoc />
        public override double ScoreMultiplier
        {
            get => _scoreMultiplier;
            protected set => _scoreMultiplier = value;
        }
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "渐隐";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new FadeInMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.Hidden;
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                _scoreMultiplier = 1;
            return beatmap;
        }
    }
}