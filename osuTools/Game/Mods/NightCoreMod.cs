using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    /// 加重节奏的DoubleTime
    /// </summary>
    public class NightCoreMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "NightCore";
        /// <inheritdoc />
        public override string ShortName => "NC";
        /// <inheritdoc />
        public override double ScoreMultiplier => _scoreMultiplier;
        private double _scoreMultiplier = 1.12;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "在DoubleTime的基础上加重节奏";
        /// <inheritdoc />
        public double TimeRate => 1.5d;
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new DoubleTimeMod(), new HalfTimeMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.NightCore;
        /// <inheritdoc />
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Catch) _scoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) _scoreMultiplier = 1d;
            return base.CheckAndSetForMode(mode);
        }
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                ScoreMultiplier = 1;
            var hitObjects = beatmap.HitObjects;
            hitObjects.ForEach(hitObject => hitObject.Offset = (int) (hitObject.Offset / 1.25d));
            beatmap.HitObjects = hitObjects;
            return beatmap;
        }
    }
}