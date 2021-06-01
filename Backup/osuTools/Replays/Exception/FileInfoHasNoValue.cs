using osuTools.Exceptions;

namespace osuTools.Replays.Exception
{
    /// <summary>
        ///     未将FileInfo初始化却使用了FileInfo时触发的异常
        /// </summary>
        public class FileInfoHasNoValue : OsuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息构建一个FileInfoHasNoValue异常
            /// </summary>
            /// <param name="m"></param>
            public FileInfoHasNoValue(string m) : base(m)
            {
            }
        }
}