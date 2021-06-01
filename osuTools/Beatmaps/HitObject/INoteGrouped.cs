namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     Note是一组一组出现的
    /// </summary>
    public interface INoteGrouped
    {
        /// <summary>
        ///     是否为一组新的HitObject的第一个HitObject
        /// </summary>
        bool IsNewGroup { get; }
    }
}