using System;
using System.IO.MemoryMappedFiles;
using InfoReaderPlugin;
using InfoReaderPlugin.I18nTool;
using RealTimePPDisplayer;
using Sync.Tools;

namespace osuTools
{
    /// <summary>
    ///     请使用osuTools.ORTDP类
    /// </summary>
    [Obsolete]
    public partial class SyncPPInfo
    {
        private double accpp, aimpp, speedpp, fcaim, fcacc, fcspeed, maccpp, maimpp, mspeedpp, rpp, fpp, mpp;

        private int C300g, C300, C200, C100, C50, Cmiss, fc, objcount, maxc, pmaxc, cc;
        private TimeSpan d, s;
        private TimeSpan durat;
        private readonly MemoryMappedFile PPInfomfs;
        private double pt, du, timep;
        private RealTimePPDisplayerPlugin rpg;
        private readonly RtppdInfo rtppi;
        private double tmper = 0;

        /// <summary>
        ///     使用内存映射的名称，<see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin" />，<see cref="InfoReaderPlugin.RtppdInfo" />及
        ///     <see cref="ORTDPWrapper" />构造一个SyncPPInfo对象
        /// </summary>
        /// <param name="mmfName"></param>
        /// <param name="rp"></param>
        /// <param name="rti"></param>
        /// <param name="or"></param>
        public SyncPPInfo(string mmfName = "rtpp", RealTimePPDisplayerPlugin rp = null, RtppdInfo rti = null,
            ORTDPWrapper or = null)
        {
            try
            {
                /*if (Sync != "" && System.IO.File.Exists(Sync))
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Sync").Length == 0)
                    {
                        System.Diagnostics.Process.Start(Sync);
                    }
                }
                else
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Sync").Length == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("请启动Sync");
                    }
                }*/
                if (or != null) ot = or;
                if (!(rti is null) && !(rp is null))
                {
                    rpg = rp;
                    rtppi = rti;
                }

                PPInfomfs = MemoryMappedFile.OpenExisting(mmfName);
                var format = NI18n.GetLanguageElement("LANG_INFO_STH_INITED");
                IO.CurrentIO.Write(string.Format(format, "SyncPPInfo"));
            }

            catch (Exception x)
            {
            }
        }
    }
}