namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 矢量缩放倍率
    /// </summary>
    public class VectorScaleMultiplier
    {
        public VectorScaleMultiplier(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double x { get; set; } = 1;
        public double y { get; set; } = 1;
    }
}