namespace osuTools.Game.Mods
{
    /// <summary>
    ///     可以更改谱面速度的Mod
    /// </summary>
    public interface IChangeTimeRateMod
    {
        /// <summary>
        ///     速率
        /// </summary>
        double TimeRate { get; }
    }
}