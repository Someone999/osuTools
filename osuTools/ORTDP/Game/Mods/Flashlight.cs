using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class FlashlightMod : Mod, ILegacyMod, IHasConflictMods
    {
        private double scoreMultiplier = 1.12;
        public override bool IsRankedMod => true;
        public override string Name => "Flashlight";
        public override string ShortName => "FL";

        public override double ScoreMultiplier
        {
            get => scoreMultiplier;
            protected set => scoreMultiplier = value;
        }

        public override ModType Type => ModType.DifficultyIncrease;

        public override string Description
        {
            get => "极限视野";
            protected set { }
        }

        public Mod[] ConflictMods { get; set; } = new Mod[0];
        public OsuGameMod LegacyMod => OsuGameMod.Flashlight;

        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Mania)
            {
                scoreMultiplier = 1d;
                ConflictMods = new Mod[] {new HiddenMod(), new FadeInMod()};
            }

            if (mode == OsuGameMode.Catch) scoreMultiplier = 1.06d;
            return base.CheckAndSetForMode(mode) && true;
        }

        public override Beatmap Apply(Beatmap beatmap)
        {
            CheckAndSetForMode(GameMode.FromLegacyMode(beatmap.Mode));
            return beatmap;
        }
    }
}