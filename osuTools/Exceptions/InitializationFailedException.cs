using System;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     插件中的任何一个依赖项或插件本身初始化失败时引发的异常
    /// </summary>
    public class InitializationFailedException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个InitializationFailedException
        /// </summary>
        /// <param name="msg">信息</param>
        public InitializationFailedException(string msg) : base(msg)
        {
        }

        /// <summary>
        ///     使用指定的信息和内部异常初始化一个InitializationFailedException
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="innerException" />
        public InitializationFailedException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}