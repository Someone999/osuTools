using osuTools.Game.Modes.ScoreCalculators;

namespace osuTools.Game.Modes
{
    public interface IHasScoreCalculator
    {
        ScoreCalculator GetScoreCalculator();
    }
}