namespace osuTools.Game.Modes.ScoreCalculators
{
    /// <summary>
    ///     表示一个分数计算器
    /// </summary>
    public abstract class ScoreCalculator
    {
        public virtual double GetScore(Judgement judgement, ORTDP.OrtdpWrapper ortdpInfo)
        {
            return 0;
        }
    }
}