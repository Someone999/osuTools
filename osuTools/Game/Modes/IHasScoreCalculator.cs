using osuTools.Game.Modes.ScoreCalculators;

namespace osuTools.Game.Modes
{
    /// <summary>
    /// 有分数计算器的Mode
    /// </summary>
    public interface IHasScoreCalculator
    {
        /// <summary>
        /// 分数计算器
        /// </summary>
        /// <returns></returns>
        ScoreCalculator GetScoreCalculator();
    }
}