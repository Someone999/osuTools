namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     表示一个皮肤的音频元素
    /// </summary>
    public interface ISkinSound : ISkinObject
    {
        /// <summary>
        ///     音频在游戏中对应的元素
        /// </summary>
        string SkinSoundTypeName { get; }
    }
}