using osuTools.Beatmaps;
using RealTimePPDisplayer.Displayer;

namespace osuTools.Game.Modes
{
    public interface IHasPerformanceCalculator
    {
        void SetBeatmap(Beatmap b);
        double GetMaxPerformance(ORTDPWrapper ortdpInfo);
        double GetPerformance(ORTDPWrapper ortdpInfo);
        PPTuple GetPPTuple(ORTDPWrapper ortdpInfo);
    }
}