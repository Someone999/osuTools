using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class DoubleTimeMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        public override bool IsRankedMod { get; protected set; } = true;
        public override string Name { get; protected set; } = "DoubleTime";
        public override string ShortName { get; protected set; } = "DT";
        public override double ScoreMultiplier { get; protected set; } = 1.12d;
        public override ModType Type => ModType.DifficultyIncrease;
        public override string Description { get; protected set; } = "1.5倍速";
        public double TimeRate => 1.5d;
        public Mod[] ConflictMods => new Mod[] {new NightCoreMod(), new HalfTimeMod()};
        public OsuGameMod LegacyMod => OsuGameMod.DoubleTime;

        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Catch) ScoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) ScoreMultiplier = 1d;
            return true && base.CheckAndSetForMode(mode);
        }

        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                ScoreMultiplier = 1d;
            var hitObjects = beatmap.HitObjects;
            hitObjects.ForEach(hitObject => hitObject.Offset = (int) (hitObject.Offset / 1.25d));
            beatmap.HitObjects = hitObjects;
            return beatmap;
        }
    }
}