using osuTools.Beatmaps;
using RealTimePPDisplayer.Displayer;

namespace osuTools.Game.Modes
{
    public interface IHasPerformanceCalculator
    {
        void SetBeatmap(Beatmap b);
        double GetMaxPerformance(ORTDP.OrtdpWrapper ortdpInfo);
        double GetPerformance(ORTDP.OrtdpWrapper ortdpInfo);
        PPTuple GetPPTuple(ORTDP.OrtdpWrapper ortdpInfo);
    }
}