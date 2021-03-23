namespace osuTools.Game.Interface
{
    public interface IOsuFileContent
    {
        /// <summary>
        ///     获取这个对象在osu文件中的格式
        /// </summary>
        /// <returns></returns>
        string ToOsuFormat();
    }
}