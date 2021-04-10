using System;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     处理osu文件时出现错误引发的异常。
    /// </summary>
    public class FailToParseException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个FailToParseException
        /// </summary>
        /// <param name="message">信息</param>
        public FailToParseException(string message) : base(message)
        {
        }

        /// <summary>
        ///     使用指定的信息和内部异常初始化一个FailToParseException
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="innerException" />
        public FailToParseException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}