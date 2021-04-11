namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 缩放倍率
    /// </summary>
    public class ScaleMultiplier
    {
        /// <summary>
        /// 使用整体缩放倍率初始化ScaleMultiplier
        /// </summary>
        /// <param name="overall">整体缩放倍率</param>
        public ScaleMultiplier(double overall)
        {
            Overall = overall;
        }

        /// <summary>
        ///     整体缩放倍率
        /// </summary>
        public double Overall { get; set; } = 1;
    }
}