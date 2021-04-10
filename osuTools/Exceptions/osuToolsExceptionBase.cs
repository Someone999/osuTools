using System;

namespace osuTools.Exceptions
{
    /// <summary>
        ///     osuTools异常的基类
        /// </summary>
        public class osuToolsExceptionBase : Exception
        {
            /// <summary>
            ///     使用指定的信息初始化一个osuToolsExceptionBase
            /// </summary>
            /// <param name="msg">信息</param>
            public osuToolsExceptionBase(string msg) : base(msg)
            {
            }

            /// <summary>
            ///     使用指定的信息和一个内部异常初始化一个osuToolsExceptionBase
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="innerException"></param>
            public osuToolsExceptionBase(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }
}