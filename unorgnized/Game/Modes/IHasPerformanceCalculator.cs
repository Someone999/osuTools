using osuTools.Beatmaps;
using RealTimePPDisplayer.Displayer;

namespace osuTools.Game.Modes
{
    public interface IHasPerformanceCalculator
    {
        /// <summary>
        /// 为pp计算器设置谱面
        /// </summary>
        /// <param name="b"></param>
        void SetBeatmap(Beatmap b);
        /// <summary>
        /// 获取当前谱面的最大Pp
        /// </summary>
        /// <param name="ortdpInfo"></param>
        /// <returns></returns>
        double GetMaxPerformance(ORTDP.OrtdpWrapper ortdpInfo);
        /// <summary>
        /// 获取当前Pp
        /// </summary>
        /// <param name="ortdpInfo"></param>
        /// <returns></returns>
        double GetPerformance(ORTDP.OrtdpWrapper ortdpInfo);
        /// <summary>
        /// 获取Pp结构
        /// </summary>
        /// <param name="ortdpInfo"></param>
        /// <returns></returns>
        PPTuple GetPPTuple(ORTDP.OrtdpWrapper ortdpInfo);
    }
}