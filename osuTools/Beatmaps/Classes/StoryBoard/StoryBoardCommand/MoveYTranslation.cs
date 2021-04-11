namespace osuTools.StoryBoard.Command
{
    /// <summary>
    /// Y轴的运动变换
    /// </summary>
    public class MoveYTranslation : ITranslation
    {
        /// <summary>
        ///     使用变化开始时的Y坐标，变化结束时的Y坐标，开始时间和结束时间初始化一个MoveYTranslation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="sttm"></param>
        /// <param name="edtm"></param>
        public MoveYTranslation(double start, double target, int sttm, int edtm)
        {
            StartPoint = start;
            TargetPoint = target;
            StartTime = sttm;
            EndTime = edtm;
        }

        /// <summary>
        ///     起始点
        /// </summary>
        public double StartPoint { get; set; }

        /// <summary>
        ///     终点
        /// </summary>
        public double TargetPoint { get; set; }
        ///<inheritdoc/>
        public int StartTime { get; set; }
        ///<inheritdoc/>
        public int EndTime { get; set; }
    }
}