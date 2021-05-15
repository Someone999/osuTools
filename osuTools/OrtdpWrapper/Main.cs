using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using InfoReaderPlugin;
using InfoReaderPlugin.Plugin.I18n;
using OsuRTDataProvider;
using osuTools.Beatmaps;
using osuTools.OsuDB;
using RealTimePPDisplayer;
using Sync.Tools;

namespace osuTools.OrtdpWrapper
{
    [Serializable]
    public partial class OrtdpWrapper
    {
        /// <summary>
        ///     不使用额外类型构建ORTDP
        /// </summary>
        public OrtdpWrapper()
        {
            _beatmapDb = new OsuBeatmapDB();
            Beatmap = new Beatmap();
            InitLisenter();
            //_ppinfo = new SyncPPInfo("rtpp", null, null, this);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));

            //OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }

        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />辅助构建ORTDP类
        /// </summary>
        public OrtdpWrapper(OsuRTDataProviderPlugin p)
        {
            _beatmapDb = new OsuBeatmapDB();
            Beatmap = new Beatmap();
            InitLisenter(p);
            //_ppinfo = new SyncPPInfo("rtpp", null, null, this);
            Application.ThreadException += Application_ThreadException;
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));

            //OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }
        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />及
        ///     <see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin" />辅助构建ORTDP类
        /// </summary>
        public OrtdpWrapper(OsuRTDataProviderPlugin p, RealTimePPDisplayerPlugin rp, RtppdInfo d)
        {
            _beatmapDb = new OsuBeatmapDB();
            Beatmap = new Beatmap();

            _arp = rp ?? new RealTimePPDisplayerPlugin();
            _rtppi = new RtppdInfo();
            _arp.RegisterDisplayer("osuToolsDisplayer", id => _rtppi = new RtppdInfo());
            InitLisenter(p);
            /*if (rp != null && d != null)
            {
                _arp = rp;
                _arp.RegisterDisplayer("my", id =>
                {
                    _rtppi = d;
                    _ppinfo = new SyncPPInfo("rtpp", _arp, _rtppi, this);
                    return d;
                });
            }
            else
            {
                _rtppi = new RtppdInfo();
            }*/
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
            //OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }

        private void Failed(OrtdpWrapper current)
        {
            var con =
                $"Date:{SysTime:yyyy/MM/dd HH:mm:ss}\n" +
                $"Song:{current.NowPlaying}(RetryCount:{current.RetryCount})\n" +
                $"Stars:{current.StarsStr} " +
                $"Mode: {current.GameMode.CurrentMode}\n" +
                $"FailedTime:{CurrentTimeStr}\\{SongDurationStr}({TimePercentStr})\n" +
                $"Accuracy:{current.Accuracy}% Score:{current.Score}\n" +
                $"PP: {CurrentPpStr}pp \\ {FcPpStr}pp -> {MaxPpStr}pp({current.PpPercent})\n" +
                $"MaxCombo:{current.PlayerMaxCombo}\n" +
                $"Mod:{(string.IsNullOrEmpty(current.ModShortNames) ? "None" : current.ModShortNames)}\n" +
                $"Rank:{current.CurrentRank}\n" +
                $"{current.Count300}xc300 {current.Count100}xc100 {current.Count50}x50 {current.CountMiss}xMiss\n" +
                $"{current.CountGeki}x300g {current.CountKatu}x200\n\n";
            File.AppendAllText("D:\\osu\\Failed\\Failed.txt", con);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.AppendAllText("osuToolsEx.txt", ((Exception) e.ExceptionObject).ToString());
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            File.AppendAllText("osuToolsEx.txt", e.Exception.ToString());
        }
    }
}