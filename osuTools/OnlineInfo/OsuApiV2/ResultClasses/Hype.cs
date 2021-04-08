namespace osuTools.Online.ApiV2.Classes
{
    /// <summary>
    ///     谱面的宣传状态
    /// </summary>
    public class Hype
    {
        /// <summary>
        ///     能否被宣传
        /// </summary>
        public bool CanBeHyped { get; internal set; }

        /// <summary>
        ///     当前被宣传的次数
        /// </summary>
        public int CurrentHyped { get; internal set; } = -1;

        /// <summary>
        ///     需要被宣传的次数
        /// </summary>
        public int RequiredHype { get; internal set; } = -1;
    }
}