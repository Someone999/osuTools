namespace osuTools
{
    /// <summary>
    /// 表示这个对象是从osu文件中解析的对象
    /// </summary>
    public interface IOsuFileContent
    {
        /// <summary>
        ///     获取这个对象在osu文件中的格式
        /// </summary>
        /// <returns></returns>
        string ToOsuFormat();
    }
}