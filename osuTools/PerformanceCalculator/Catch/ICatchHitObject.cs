using osuTools.Beatmaps.HitObject;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// Catch pp计算器专用的HitObject.
    /// </summary>
    public interface ICatchHitObject: IHitObject
    {
        /// <summary>
        /// x坐标
        /// </summary>
        double x { get; }
        /// <summary>
        /// y坐标
        /// </summary>
        double y { get; }
    }
}
