namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// Catch pp计算器专用的HitObject.因为需要的信息有限，没有继承IHitObject
    /// </summary>
    public interface ICatchHitObject
    {
        /// <summary>
        /// x坐标
        /// </summary>
        double x { get; }
        /// <summary>
        /// y坐标
        /// </summary>
        double y { get; }
        /// <summary>
        /// 时间偏移
        /// </summary>
        double Offset { get; }
    }
}
