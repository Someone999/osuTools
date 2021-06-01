namespace osuTools.Game.Modes
{
    /// <summary>
    ///     表示一个判定
    /// </summary>
    public abstract class Judgement
    {
        /// <summary>
        /// 表示一个判定结果
        /// </summary>
        public HitResults HitResult { get; protected set; }
        /// <summary>
        /// 是否为有效判定，子类可以重写该方法
        /// </summary>
        /// <param name="hitresult"></param>
        /// <returns></returns>
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