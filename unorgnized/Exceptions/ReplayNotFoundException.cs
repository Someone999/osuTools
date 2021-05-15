namespace osuTools.Exceptions
{
    /// <summary>
    ///     找不到与指定条件匹配的回放时引发的异常。
    /// </summary>
    public class ReplayNotFoundException : OsuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个ReplayNotFoundException
        /// </summary>
        /// <param name="message">信息</param>
        public ReplayNotFoundException(string message) : base(message)
        {
        }
    }
}