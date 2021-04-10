namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     一个Mod的图片
    /// </summary>
    public interface IModImage : ISkinObject
    {
        /// <summary>
        ///     对应的Mod
        /// </summary>
        OsuGameMod Mod { get; }
    }
}