namespace osuTools.Beatmaps.HitObject.Mania
{
    /// <summary>
    ///     表示Mania模式的HitObject
    /// </summary>
    public interface IManiaHitObject : IHitObject
    {
        /// <summary>
        ///     所在的列数
        /// </summary>
        int Column { get; set; }

        /// <summary>
        ///     谱面总列数
        /// </summary>
        int BeatmapColumn { get; set; }
    }
}