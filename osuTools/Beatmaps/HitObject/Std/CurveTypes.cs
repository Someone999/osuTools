namespace osuTools.Beatmaps.HitObject.Std
{
    /// <summary>
    ///     曲线类型
    /// </summary>
    public enum CurveTypes
    {
        /// <summary>
        ///     贝塞尔曲线
        /// </summary>
        Bezier,

        /// <summary>
        ///     CRS曲线
        /// </summary>
        CentripetalCatmullRom,

        /// <summary>
        ///     线性曲线
        /// </summary>
        Linear,

        /// <summary>
        ///     完美曲线
        /// </summary>
        PerfectCircle,

        /// <summary>
        ///     未定义
        /// </summary>
        Unknown = -1
    }
}