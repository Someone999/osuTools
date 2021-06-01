namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// 矢量缩放倍率
    /// </summary>
    public class VectorScaleMultiplier
    {
        /// <summary>
        /// 使用指定的参数初始化VectorScaleMultiplier
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public VectorScaleMultiplier(double x = 1, double y = 1)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// x的倍率
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// y的倍率
        /// </summary>
        public double y { get; set; }
    }
}