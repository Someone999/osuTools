using System;
using InfoReaderPlugin;
using OsuRTDataProvider;
using OsuRTDataProvider.Listen;
using OsuRTDataProvider.Mods;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using RealTimePPDisplayer;

namespace osuTools
{
    partial class ORTDPWrapper
    {
        /// <summary>
        ///     指定读取谱面信息的插件
        /// </summary>
        public enum BeatmapReadMethods
        {
            /// <summary>
            ///     通过<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />获取谱面信息
            /// </summary>
            OsuRTDataProvider,

            /// <summary>
            ///     通过<see cref="osuTools.OsuDB.OsuBeatmapDB" />获取谱面信息
            /// </summary>
            OsuDB
        }

        private double acc, maxpp, fcpp, scc = 0;
        private RealTimePPDisplayerPlugin arp;
        private OsuBeatmapStatus b_status = OsuBeatmapStatus.Unknown;
        private OsuBeatmapDB bd;

        /// <summary>
        ///     读取谱面的方式
        /// </summary>
        public BeatmapReadMethods BeatmapReadMethod = BeatmapReadMethods.OsuRTDataProvider;

        private double bestp;
        private int bests;
        private int mco = 0;
        private TimeSpan dur, cur;
        private string file;

        //GMMod mo;
        private HitObjectCollection hitObjects;
        private OsuInfo info = new OsuInfo();

        [NonSerialized] private OsuListenerManager lm;

        private bool ManiaRanked = false;
        private ModsInfo modsinfo;

        [NonSerialized] private OsuRTDataProviderPlugin p;

        [NonSerialized] private SyncPPInfo ppinfo;

        private bool Ranked;
        private string Ranking = "Unknown";

        [NonSerialized] private RtppdInfo rtppi;

        private double stars;
        private double tmper;
        private bool unRanked = false;

        /// <summary>
        ///     是否处于调试模式
        /// </summary>
        public bool DebugMode { get; set; }
    }
}