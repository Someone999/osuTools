using System;

namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 表示一个播放器的异常
    /// </summary>
    public class PlayerException:Exception
    {
        /// <inheritdoc/>
        public PlayerException(string msg) : base(msg)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public PlayerException(string msg,Exception innerException) : base(msg,innerException)
        {
        }
    }
}
