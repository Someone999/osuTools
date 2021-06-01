using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    /// 将列分成两个部分供双人游玩
    /// </summary>
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