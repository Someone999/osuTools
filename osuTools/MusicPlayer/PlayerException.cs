using System;
using osuTools.Exceptions;

namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 表示一个播放器的异常
    /// </summary>
    public class PlayerException:osuToolsExceptionBase
    {
        /// <inheritdoc/>
        /// <param name="msg">异常信息</param>
        public PlayerException(string msg) : base(msg)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public PlayerException(string msg,Exception innerException) : base(msg,innerException)
        {
        }
    }
}
