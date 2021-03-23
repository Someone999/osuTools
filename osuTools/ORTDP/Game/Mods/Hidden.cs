using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class HiddenMod : Mod, ILegacyMod, IHasConflictMods
    {
        private double scoreMultiplier = 1.06;
        public override bool IsRankedMod => true;
        public override string Name => "Hidden";
        public override string ShortName => "HD";

        public override double ScoreMultiplier
        {
            get => scoreMultiplier;
            protected set => scoreMultiplier = value;
        }

        public override ModType Type => ModType.DifficultyIncrease;
        public override string Description => "渐隐";
        public Mod[] ConflictMods => new Mod[] {new FadeInMod()};
        public OsuGameMod LegacyMod => OsuGameMod.Hidden;

        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                scoreMultiplier = 1;
            return beatmap;
        }
    }
}