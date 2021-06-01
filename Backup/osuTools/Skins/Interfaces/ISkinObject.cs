namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     表示一个皮肤元素文件
    /// </summary>
    public interface ISkinObject
    {
        /// <summary>
        ///     文件名
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     全路径
        /// </summary>
        string FullPath { get; }
    }
}