namespace osuTools.Game.Modes
{
    /// <summary>
    ///     代表这个游戏模式存在于<see cref="OsuGameMode" />
    /// </summary>
    public interface ILegacyMode
    {
        /// <summary>
        ///     对应的<see cref="OsuGameMode" />
        /// </summary>
        OsuGameMode LegacyMode { get; }
    }
}