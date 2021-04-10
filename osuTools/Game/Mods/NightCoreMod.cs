using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class NightCoreMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "NightCore";
        /// <inheritdoc />
        public override string ShortName => "NC";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.12d;
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
            if (mode == OsuGameMode.Catch) ScoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) ScoreMultiplier = 1d;
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