using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    /// 怎样都失败不了
    /// </summary>
    public class NoFailMod : Mod, ILegacyMod, IHasConflictMods
    {
        /// <inheritdoc />
        public override bool AllowsFail() => false;
        /// <inheritdoc />
        public override bool IsRankedMod => true;
        /// <inheritdoc />
        public override string Name => "NoFail";
        /// <inheritdoc />
        public override string ShortName => "NF";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.5;
        /// <inheritdoc />
        public override ModType Type => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "无论如何都不会失败";
        /// <inheritdoc />
        public Mod[] ConflictMods => new Mod[] {new SuddenDeathMod(), new PerfectMod()};
        /// <inheritdoc />
        public OsuGameMod LegacyMod => OsuGameMod.NoFail;
    }
}