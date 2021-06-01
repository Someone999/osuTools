namespace osuTools.Game
{
    /// <summary>
    /// 游戏的评级
    /// </summary>
    public enum GameRanking
    {
        /// <summary>
        /// 预留项
        /// </summary>
        Unknown = -1, 
        /// <summary>
        /// 当Std和Taiko的300率小于60%，Catch模式的Acc小于85%，Mania的Acc小于60%时的等级
        /// </summary>
        D, 
        /// <summary>
        /// 当Std和Taiko的300率大于60%小于等于70%时的，Catch的Acc大于85%小于等于90%，Mania的Acc大于70%小于等于80%时的等级
        /// </summary>
        C, 
        /// <summary>
        /// 当Std和Taiko的300率大于80%或300率大于70%且没有Miss，Catch的Acc大于90%小于等于94%，Mania的Acc大于80%小于等于90%时的等级
        /// </summary>
        B,
        /// <summary>
        /// 当Std和Taiko的300率大于90%或300率大于80%且没有Miss，Catch的Acc大于95%小于等于98%，Mania的Acc大于90%小于95%时的等级
        /// </summary>
        A,
        /// <summary>
        /// 当Std和Taiko的300率大于90%，50率小于1%且没有Miss，Catch的Acc大于98%小于等于100%，Mania的Acc大于95%小于100%时的等级
        /// </summary>
        S,
        /// <summary>
        /// Acc为100%时的评级
        /// </summary>
        SS, 
        /// <summary>
        /// 在S的基础上开了缩减视野Mod的评级
        /// </summary>
        SH,
        /// <summary>
        /// 在SS的基础上开了缩减视野Mod的评级
        /// </summary>
        SSH
    }
}