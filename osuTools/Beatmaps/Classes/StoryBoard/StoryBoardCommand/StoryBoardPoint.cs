namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// 表示StoryBoard图象的位置
    /// </summary>
    public class StoryBoardPoint
    {
        /// <summary>
        /// 使用x和y坐标来初始化一个StoryBoardPoint
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public StoryBoardPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// x坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// y坐标
        /// </summary>
        public double Y { get; set; }
    }
}