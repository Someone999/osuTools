using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class NightCoreMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        public override bool IsRankedMod => true;
        public override string Name => "NightCore";
        public override string ShortName => "NC";
        public override double ScoreMultiplier => 1.12;
        public override ModType Type => ModType.DifficultyIncrease;

        public override string Description
        {
            get => "在DoubleTime的基础上加重节奏";
            protected set => base.Description = value;
        }

        public double TimeRate => 1.5d;
        public Mod[] ConflictMods => new Mod[] {new DoubleTimeMod(), new HalfTimeMod()};
        public OsuGameMod LegacyMod => OsuGameMod.NightCore;

        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Catch) ScoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) ScoreMultiplier = 1d;
            return true && base.CheckAndSetForMode(mode);
        }

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