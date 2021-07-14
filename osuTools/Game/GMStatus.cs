using System;
using OsuRTDataProvider.Listen;

namespace osuTools.Game
{
    /// <summary>
    ///     记录游戏的状态
    /// </summary>
    [Serializable]
    public class GMStatus
    {
        /// <summary>
        ///     使用两个<see cref="OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus" />构造一个GMStatus
        /// </summary>
        /// <param name="last"></param>
        /// <param name="now"></param>
        public GMStatus(OsuListenerManager.OsuStatus last, OsuListenerManager.OsuStatus now)
        {
            LastStatus = (OsuGameStatus) last;
            CurrentStatus = (OsuGameStatus) now;
        }

        /// <summary>
        ///     上一次的状态
        /// </summary>
        public OsuGameStatus LastStatus { get; internal set; }

        /// <summary>
        ///     当前状态
        /// </summary>
        public OsuGameStatus CurrentStatus { get; internal set; }
    }
}