using System;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     osu!api查询失败时引发的异常。
    /// </summary>
    public class OnlineQueryFailedException : OsuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个OnlineQueryFailedException
        /// </summary>
        /// <param name="info">信息</param>
        public OnlineQueryFailedException(string info) : base(info)
        {
        }

        /// <summary>
        ///     使用指定的信息和内部异常初始化一个OnlineQueryFailedException
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="innerException" />
        public OnlineQueryFailedException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}