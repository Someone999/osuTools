namespace osuTools.Game.Modes.ScoreCalculators
{
    public enum HitResults
    {
        Hit300g,Hit300,Hit200,Hit100,Hit50,HitMiss
    }
    /// <summary>
    ///     表示一个判定
    /// </summary>
    public abstract class Judgement
    {
        public HitResults HitResult { get; protected set; }

        public virtual bool IsValidHitResult(HitResults hitresult)
        {
            switch (hitresult)
            {
                case HitResults.Hit300:
                case HitResults.Hit100:
                case HitResults.Hit50:
                case HitResults.HitMiss: return true;
                default: return false;
            }
        }
    }

    /// <summary>
    ///     表示一个分数计算器
    /// </summary>
    public abstract class ScoreCalculator
    {
        public virtual double GetScore(Judgement judgement, ORTDPWrapper ortdpInfo)
        {
            return 0;
        }
    }
}