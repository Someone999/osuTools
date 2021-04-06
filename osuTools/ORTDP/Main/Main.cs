using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using InfoReaderPlugin;
using InfoReaderPlugin.I18nTool;
using OsuRTDataProvider;
using osuTools.OsuDB;
using osuTools.osuToolsException;
using RealTimePPDisplayer;
using Sync.Tools;

namespace osuTools
{
    [Serializable]
    public partial class ORTDPWrapper
    {
        /// <summary>
        ///     不使用额外类型构建ORTDP
        /// </summary>
        public ORTDPWrapper()
        {
            bd = new OsuBeatmapDB();
            InitLisenter();
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
            ppinfo = new SyncPPInfo("rtpp", null, null, this);
            OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }

        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />辅助构建ORTDP类
        /// </summary>
        public ORTDPWrapper(OsuRTDataProviderPlugin p)
        {
            bd = new OsuBeatmapDB();
            InitLisenter(p);
            Application.ThreadException += Application_ThreadException;
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
            ppinfo = new SyncPPInfo("rtpp", null, null, this);
            OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }

        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />及
        ///     <see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin" />辅助构建ORTDP类
        /// </summary>
        public ORTDPWrapper(OsuRTDataProviderPlugin p, RealTimePPDisplayerPlugin rp, RtppdInfo d)
        {
            bd = new OsuBeatmapDB();
            InitLisenter(p);
            if (rp != null && d != null)
            {
                arp = rp;
                arp.RegisterDisplayer("my", id =>
                {
                    d = new RtppdInfo();
                    rtppi = d;
                    ppinfo = new SyncPPInfo("rtpp", arp, rtppi, this);
                    if (d is null)
                        throw new InitializationFailedException(NI18n.GetLanguageElement(
                            "LANG_ERR_FAIL_TO_REGISTER_NEW_DISPLAYER"));
                    return d;
                });
            }

            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
            OnFail += Failed;
            Application.ThreadException += Application_ThreadException;
        }

        private void Failed(ORTDPWrapper current)
        {
            var con =
                $"Date:{SysTime.ToString("yyyy/MM/dd HH:mm:ss")}\n" +
                $"Song:{current.NowPlaying}(RetryCount:{current.RetryCount})\n" +
                $"Stars:{current.StarsStr} " +
                $"Mode: {current.GameMode.CurrentMode}\n" +
                $"FailedTime:{CurrentTimeStr}\\{SongDurationStr}({TimePercentStr})\n" +
                $"Accuracy:{current.Accuracy}% Score:{current.Score}\n" +
                $"PP: {CurrentPPStr}pp \\ {FcPPStr}pp -> {MaxPPStr}pp({current.PPpercent})\n" +
                $"MaxCombo:{current.PlayerMaxCombo}\n" +
                $"Mod:{(string.IsNullOrEmpty(current.ModShortNames) ? "None" : current.ModShortNames)}\n" +
                $"Rank:{current.CurrentRank}\n" +
                $"{current.c300}xc300 {current.c100}xc100 {current.c50}x50 {current.cMiss}xMiss\n" +
                $"{current.c300g}x300g {current.c200}x200\n\n";
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