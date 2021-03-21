﻿namespace osuTools
{
    /// <summary>
    ///     使用OsuRTDataProvider进行数据收集的类
    /// </summary>
    partial class ORTDPWrapper
    {
        /// <summary>
        ///     失败时触发的的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void FailedEventHandler(ORTDPWrapper currentStatus);

        /// <summary>
        ///     开启NoFail时血量≤0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void HP0EventHandler(ORTDPWrapper currentStatus);

        /// <summary>
        ///     当分数从非0值变成0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        /// <param name="timesofRetry"></param>
        public delegate void ScoreResetedEventHandler(ORTDPWrapper currentStatus, int timesofRetry);

        /// <summary>
        ///     失败时触发的事件
        /// </summary>
        public event FailedEventHandler OnFail;

        /// <summary>
        ///     开启NoFail时血量≤0时触发的事件
        /// </summary>
        public event HP0EventHandler OnNoFail;

        /// <summary>
        ///     当分数从非0值变成0时触发的时间
        /// </summary>
        public event ScoreResetedEventHandler OnScoreReset;
    }
}