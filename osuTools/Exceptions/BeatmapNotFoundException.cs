namespace osuTools.Exceptions
{
    /// <summary>
    ///     找不到与指定条件匹配的谱面时引发的异常。
    /// </summary>
    public class BeatmapNotFoundException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个BeatmapNotFoundException
        /// </summary>
        /// <param name="message">信息</param>
        public BeatmapNotFoundException(string message) : base(message)
        {
        }
    }
}