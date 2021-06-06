using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using InfoReaderPlugin;
using InfoReaderPlugin.I18n;
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
            Application.ThreadException += Application_ThreadException;
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
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
            var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
            IO.CurrentIO.Write(string.Format(format, "ORTDP"));
            Application.ThreadException += Application_ThreadException;
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