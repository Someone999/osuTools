using System;
using InfoReaderPlugin;
using OsuRTDataProvider;
using OsuRTDataProvider.Listen;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using RealTimePPDisplayer;

namespace osuTools.OrtdpWrapper
{
    partial class OrtdpWrapper
    {
        /// <summary>
        ///     指定读取谱面信息的插件
        /// </summary>
        public enum BeatmapReadMethods
        {
            /// <summary>
            ///     通过<see cref="OsuRTDataProviderPlugin" />获取谱面信息
            /// </summary>
            Ortdp,

            /// <summary>
            ///     通过<see cref="osuTools.OsuDB.OsuBeatmapDB" />获取谱面信息
            /// </summary>
            OsuDb
        }

        private double _acc;
        private RealTimePPDisplayerPlugin _arp;
        private OsuBeatmapStatus _bStatus = OsuBeatmapStatus.Unknown;
        private OsuBeatmapDB _beatmapDb;
        private ModList _mods=new ModList();
        private TimeSpan _drainTime;

        /// <summary>
        ///     读取谱面的方式
        /// </summary>
        public BeatmapReadMethods BeatmapReadMethod = BeatmapReadMethods.Ortdp;

        private TimeSpan _dur, _cur;

        //GMMod mo;
        private HitObjectCollection _hitObjects = new HitObjectCollection();

        [NonSerialized] private OsuListenerManager _listenerManager;

        [NonSerialized] private OsuRTDataProviderPlugin _p;

        [NonSerialized] private RtppdInfo _rtppi;

        private double _tmper;

        /// <summary>
        ///     是否处于调试模式
        /// </summary>
        public bool DebugMode { get; set; }
    }
}