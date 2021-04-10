namespace osuTools.Game.Modes.ScoreCalculators
{
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
}