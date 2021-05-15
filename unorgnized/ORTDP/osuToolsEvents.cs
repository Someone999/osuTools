namespace osuTools.ORTDP
{
    /// <summary>
    ///     使用OsuRTDataProvider进行数据收集的类
    /// </summary>
    partial class OrtdpWrapper
    {
        /// <summary>
        ///     失败时触发的的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void FailedEventHandler(OrtdpWrapper currentStatus);

        /// <summary>
        ///     开启NoFail时血量≤0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void Hp0EventHandler(OrtdpWrapper currentStatus);

        /// <summary>
        ///     当分数从非0值变成0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        /// <param name="timesofRetry"></param>
        public delegate void ScoreResetedEventHandler(OrtdpWrapper currentStatus, int timesofRetry);

        /// <summary>
        ///     失败时触发的事件
        /// </summary>
        public event FailedEventHandler OnFail = status =>{} ;

        /// <summary>
        ///     开启NoFail时血量≤0时触发的事件
        /// </summary>
        public event Hp0EventHandler OnNoFail= status =>{} ;

        /// <summary>
        ///     当分数从非0值变成0时触发的时间
        /// </summary>
        public event ScoreResetedEventHandler OnScoreReset = (status, retry) => { };
    }
}