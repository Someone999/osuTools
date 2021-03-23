using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class HalfTimeMod : Mod, ILegacyMod, IChangeTimeRateMod
    {
        public override bool IsRankedMod => true;
        public override string Name => "HalfTime";
        public override string ShortName => "HT";
        public override double ScoreMultiplier => 0.3;
        public override ModType Type => ModType.DifficultyReduction;
        public override string Description => "0.75倍速";
        public Mod[] ConflictMods => new Mod[] {new DoubleTimeMod(), new NightCoreMod()};
        public double TimeRate => 0.75d;
        public OsuGameMod LegacyMod => OsuGameMod.HalfTime;

        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.HitObjects.ForEach(h => h.Offset = (int) (h.Offset / 0.75));
            return beatmap;
        }
    }
}