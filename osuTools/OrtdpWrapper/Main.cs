using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using InfoReader.Tools.I8n;
using OsuRTDataProvider;
using osuTools.Beatmaps;
using osuTools.OsuDB;
using osuTools.Tools;
using RealTimePPDisplayer;

namespace osuTools.OrtdpWrapper
{
    [Serializable]
    public partial class OrtdpWrapper : IMemoryDataSource
    {
        /// <summary>
        ///     不使用额外类型构建ORTDP
        /// </summary>
        public OrtdpWrapper()
        {
            _beatmapDb = new OsuBeatmapDb();
            Beatmap = new Beatmap();
            InitListener();
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            var format = LocalizationInfo.Current.Translations["LANG_INFO_STH_INITED"];
            OutputHelper.Output(string.Format(format, "ORTDP"));
        }

        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />辅助构建ORTDP类
        /// </summary>
        public OrtdpWrapper(OsuRTDataProviderPlugin ortdp)
        {
            _beatmapDb = new OsuBeatmapDb();
            Beatmap = new Beatmap();
            InitListener(ortdp);
            Application.ThreadException += Application_ThreadException;
            var format = LocalizationInfo.Current.Translations["LANG_INFO_STH_INITED"];
            OutputHelper.Output(string.Format(format, "ORTDP"));
            Application.ThreadException += Application_ThreadException;
        }
        /// <summary>
        ///     使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin" />及
        ///     <see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin" />辅助构建ORTDP类
        /// </summary>
        public OrtdpWrapper(OsuRTDataProviderPlugin ortdp, RealTimePPDisplayerPlugin rtppd, RtppdInfo d)
        {
            _beatmapDb = new OsuBeatmapDb();
            Beatmap = new Beatmap();

            _arp = rtppd ?? new RealTimePPDisplayerPlugin();
            _rtppi = d ?? new RtppdInfo();
            _arp.RegisterDisplayer("osuToolsDisplayer", id => _rtppi = _rtppi ?? new RtppdInfo());
            InitListener(ortdp);
            var format = LocalizationInfo.Current.Translations["LANG_INFO_STH_INITED"];
            OutputHelper.Output(string.Format(format, "ORTDP"));
            Application.ThreadException += Application_ThreadException;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.AppendAllText("osuToolsEx.txt", ((Exception)e.ExceptionObject).ToString());
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            File.AppendAllText("osuToolsEx.txt", e.Exception.ToString());
        }
    }
}