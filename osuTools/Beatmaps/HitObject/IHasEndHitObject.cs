namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     有结束时间的HitObject
    /// </summary>
    public interface IHasEndHitObject:IHitObject
    {
        /// <summary>
        ///     结束时间
        /// </summary>
        int EndTime { get; }
    }
}