using osuTools.osuToolsException;

namespace osuTools
{
    namespace Replay
    {
        /// <summary>
        ///     未将FileInfo初始化却使用了FileInfo时触发的异常
        /// </summary>
        public class FileInfoHasNoValue : osuToolsExceptionBase
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
}