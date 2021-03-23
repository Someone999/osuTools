using System;
using OsuRTDataProvider.Listen;

namespace osuTools
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
        /// <param name="Last"></param>
        /// <param name="Now"></param>
        public GMStatus(OsuListenerManager.OsuStatus Last, OsuListenerManager.OsuStatus Now)
        {
            LastStatus = (OsuGameStatus) Last;
            CurrentStatus = (OsuGameStatus) Now;
        }

        /// <summary>
        ///     上一次的状态
        /// </summary>
        public OsuGameStatus LastStatus { get; }

        /// <summary>
        ///     当前状态
        /// </summary>
        public OsuGameStatus CurrentStatus { get; }
    }
}