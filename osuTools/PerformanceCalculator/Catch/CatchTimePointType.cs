namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 时间点的类型
    /// </summary>
    public enum CatchTimePointType
    {
        /// <summary>
        /// 滑条的变速点
        /// </summary>
        Spm,
        /// <summary>
        /// 普通的变速点
        /// </summary>
        Bpm,
        /// <summary>
        /// 未经处理的Spm
        /// </summary>
        RawSpm,
        /// <summary>
        /// 未经处理的Bpm
        /// </summary>
        RawBpm
    }
}