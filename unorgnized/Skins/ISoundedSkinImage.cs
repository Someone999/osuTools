namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     一个有图片元素和音频元素的皮肤元素
    /// </summary>
    public interface ISoundedSkinImage : ISkinObjectBase
    {
        /// <summary>
        ///     图片
        /// </summary>
        ISkinImage Image { get; }

        /// <summary>
        ///     音频
        /// </summary>
        ISkinSound Sound { get; }
    }
}