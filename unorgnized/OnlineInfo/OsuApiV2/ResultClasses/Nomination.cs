namespace osuTools.OnlineInfo.OsuApiV2.ResultClasses
{
    /// <summary>
    ///     谱面的提名状态
    /// </summary>
    public class Nomination
    {
        /// <summary>
        ///     当前被提名的次数
        /// </summary>
        public int CurrentNominations { get; internal set; } = -1;

        /// <summary>
        ///     需要被提名的次数
        /// </summary>
        public int RequiredNominations { get; internal set; } = -1;
    }
}