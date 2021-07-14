namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 媒体状态
    /// </summary>
    public enum MediaState
    {
        /// <summary>
        /// 已打开文件
        /// </summary>
        Open = 0,
        /// <summary>
        /// 播放
        /// </summary>
        Play = 1,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 2,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 3,
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 4,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 5
    }
}