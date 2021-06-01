using System;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     当指定的文件不是谱面文件的时候引发的异常。
    /// </summary>
    public class InvalidBeatmapFileException : OsuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个InvalidBeatmapFileException
        /// </summary>
        /// <param name="msg">信息</param>
        public InvalidBeatmapFileException(string msg) : base(msg)
        {
        }

        /// <summary>
        ///     使用指定的信息和内部异常初始化一个InvalidBeatmapFileException
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="innerException" />
        public InvalidBeatmapFileException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}