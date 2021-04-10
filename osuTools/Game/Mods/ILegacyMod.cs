namespace osuTools.Game.Mods
{
    /// <summary>
    ///     表示这个Mod存在于<see cref="OsuGameMod" />中
    /// </summary>
    public interface ILegacyMod
    {
        /// <summary>
        ///     对应的<see cref="OsuGameMod" />
        /// </summary>
        OsuGameMod LegacyMod { get; }
    }
}