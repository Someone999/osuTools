using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class KeyCoopMod : KeyMod
    {
        /// <inheritdoc />
        public override string Name => "KeyCoop";
        /// <inheritdoc />
        public override string ShortName => "Co-op";
        /// <inheritdoc />
        public override OsuGameMod LegacyMod => OsuGameMod.KeyCoop;
        /// <inheritdoc />
        public override double ScoreMultiplier => 1d;
        /// <inheritdoc />
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Osu)
                ScoreMultiplier = 0.9d;
            return beatmap;
        }
    }
}