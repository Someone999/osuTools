namespace osuTools.Game.Modes.ScoreCalculators
{
    /// <summary>
    ///     表示一个分数计算器
    /// </summary>
    public abstract class ScoreCalculator
    {
        /// <summary>
        /// 计算分数
        /// </summary>
        /// <param name="judgement"></param>
        /// <param name="ortdpInfo"></param>
        /// <returns></returns>
        public virtual double GetScore(Judgement judgement, OrtdpWrapper.OrtdpWrapper ortdpInfo)
        {
            return 0;
        }
    }
}