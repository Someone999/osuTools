using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class EasyMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get; protected set; } = true;
        public override string Name => "Easy";
        public override string ShortName => "EZ";
        public override double ScoreMultiplier => 0.5;
        public override ModType Type => ModType.DifficultyReduction;
        public override string Description => "所有的难度参数都降低一点，并有3次满血复活的机会";
        public Mod[] ConflictMods => new Mod[] {new HardRockMod()};
        public OsuGameMod LegacyMod => OsuGameMod.Easy;

        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.AR /= 2;
            beatmap.HP /= 2;
            beatmap.OD /= 2;
            if (beatmap.Mode == OsuGameMode.Osu || beatmap.Mode == OsuGameMode.Catch)
                beatmap.CS /= 2;
            return beatmap;
        }
    }
}