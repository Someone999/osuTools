using System;

namespace osuTools
{
    partial class SyncPPInfo
    {
        /// <summary>
        ///     300g或激的数量
        /// </summary>
        public int c300g => C300g;

        /// <summary>
        ///     300的数量
        /// </summary>
        public int c300 => C300;

        /// <summary>
        ///     200或喝的数量
        /// </summary>
        public int c200 => C200;

        /// <summary>
        ///     100的数量
        /// </summary>
        public int c100 => C100;

        /// <summary>
        ///     50的数量
        /// </summary>
        public int c50 => C50;

        /// <summary>
        ///     Miss的数量
        /// </summary>
        public int cMiss => Cmiss;

        /// <summary>
        ///     谱面的最大连击
        /// </summary>
        public int FullCombo => rtppi.HitCount.FullCombo;

        /// <summary>
        ///     谱面击打物件的数量
        /// </summary>
        public int ObjectCount => rtppi.BeatmapTuple.ObjectsCount;

        /// <summary>
        ///     从RealTimePPDisplayer得到的原始字符串
        /// </summary>
        public string RawStr { get; private set; }

        /// <summary>
        ///     最大连击
        /// </summary>
        public int MaxCombo => rtppi.HitCount.CurrentMaxCombo;

        /// <summary>
        ///     玩家达到的最大连击
        /// </summary>
        public int PlayerMaxCombo => rtppi.HitCount.PlayerMaxCombo;

        /// <summary>
        ///     当前连击
        /// </summary>
        public int CurrentCombo => rtppi.HitCount.CurrentMaxCombo;

        /// <summary>
        ///     准度PP
        /// </summary>
        public double AccuracyPP => rtppi.SmoothPP.RealTimeAccuracyPP;

        /// <summary>
        ///     定位PP
        /// </summary>
        public double AimPP => rtppi.SmoothPP.RealTimeAimPP;

        /// <summary>
        ///     速度PP
        /// </summary>
        public double SpeedPP => rtppi.SmoothPP.RealTimeSpeedPP;

        /// <summary>
        ///     全连时的准度PP
        /// </summary>
        public double FCAccuracyPP => rtppi.SmoothPP.FullComboAccuracyPP;

        /// <summary>
        ///     全连时的定位PP
        /// </summary>
        public double FCAimPP => rtppi.SmoothPP.FullComboAimPP;

        /// <summary>
        ///     全连时的速度PP
        /// </summary>
        public double FCSpeedPP => rtppi.SmoothPP.FullComboSpeedPP;

        /// <summary>
        ///     总准度PP
        /// </summary>
        public double MaxAccuracyPP => rtppi.SmoothPP.MaxAccuracyPP;

        /// <summary>
        ///     总定位PP
        /// </summary>
        public double MaxAimPP => rtppi.SmoothPP.MaxAimPP;

        /// <summary>
        ///     总速度PP
        /// </summary>
        public double MaxSpeedPP => rtppi.SmoothPP.MaxSpeedPP;

        /// <summary>
        ///     全连后的PP
        /// </summary>
        public double FcPP => rtppi.SmoothPP.FullComboPP;

        /// <summary>
        ///     总pp
        /// </summary>
        public double MaxPP => rtppi.SmoothPP.MaxPP;

        /// <summary>
        ///     当前的pp
        /// </summary>
        public double CurrentPP => rtppi.SmoothPP.RealTimePP;

        /// <summary>
        ///     歌曲的长度
        /// </summary>
        public TimeSpan SongDuration
        {
            get
            {
                if (rtppi.Playtime > rtppi.BeatmapTuple.Duration)
                    durat = TimeSpan.FromMilliseconds(rtppi.Playtime);
                else
                    durat = TimeSpan.FromMilliseconds(rtppi.BeatmapTuple.Duration);
                return durat;
            }
        }

        /// <summary>
        ///     歌曲当前的位置
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                var tmptm = rtppi.Playtime;
                /*/ if (tmptm>rtppi.BeatmapTuple.Duration)
                 {
                     tmptm = rtppi.BeatmapTuple.Duration;
                 }*/
                //else
                {
                    tmptm = rtppi.Playtime;
                }
                return TimeSpan.FromMilliseconds(tmptm);
            }
        }

        /// <summary>
        ///     时间的百分比
        /// </summary>
        public double TimePercent => rtppi.Playtime / rtppi.BeatmapTuple.Duration;

        /// <summary>
        ///     格式化后的歌曲时间描述字符串
        /// </summary>
        public string FormatedTimeStr { get; private set; } = "";

        /// <summary>
        ///     格式化后的pp描述字符串
        /// </summary>
        public string FormatedPPStr { get; private set; } = "";

        /// <summary>
        ///     格式化后的判定描述字符串
        /// </summary>
        public string FormatedHitStr { get; private set; } = "";

        /// <summary>
        ///     谱面难度星数
        /// </summary>
        public double Stars => rtppi.BeatmapTuple.Stars;

        /// <summary>
        ///     是否达到Perfect判定
        /// </summary>
        public bool Perfect
        {
            get
            {
                if (ot.GameMode.CurrentMode == OsuGameMode.Mania) return c100 == 0 && c50 == 0 && cMiss == 0;
                if (ot.GameMode.CurrentMode == OsuGameMode.Osu) return FcPP == MaxPP;
                return false;
            }
        }

        /// <summary>
        ///     Mania中300g占所有300的百分比，std和ctb模式中激占激和喝总数的百分比
        /// </summary>
        public double c300gRate => (double) c300g / (c300 + c300g);

        /// <summary>
        ///     准度pp的字符串，保留2位小数
        /// </summary>
        public string AccuracyPPStr => AccuracyPP.ToString("f2");

        /// <summary>
        ///     定位pp的字符串，保留2位小数
        /// </summary>
        public string AimPPStr => AimPP.ToString("f2");

        /// <summary>
        ///     速度pp的字符串，保留2位小数
        /// </summary>
        public string SpeedPPStr => SpeedPP.ToString("f2");

        /// <summary>
        ///     当前pp的字符串，保留2位小数
        /// </summary>
        public string CurrentPPStr => CurrentPP.ToString("f2");

        /// <summary>
        ///     总pp的字符串，保留2位小数
        /// </summary>

        public string MaxPPStr => MaxPP.ToString("f2");

        /// <summary>
        ///     总定位pp的字符串，保留2位小数
        /// </summary>
        public string MaxAimPPStr => MaxAimPP.ToString("f2");

        /// <summary>
        ///     总速度pp的字符串，保留2位小数
        /// </summary>
        public string MaxSpeedPPStr => MaxSpeedPP.ToString("f2");

        /// <summary>
        ///     总准度pp的字符串，保留2位小数
        /// </summary>
        public string MaxAccuracyPPStr => MaxAccuracyPP.ToString("f2");

        /// <summary>
        ///     全连后pp的字符串，保留2位小数
        /// </summary>
        public string FcPPStr => FcPP.ToString("f2");

        /// <summary>
        ///     全连后pp的字符串，保留2位小数
        /// </summary>
        public string FcAimPPStr => FCAimPP.ToString("f2");

        /// <summary>
        ///     全连后定位pp的字符串，保留2位小数
        /// </summary>
        public string FCSpeedPPStr => FCSpeedPP.ToString("f2");

        /// <summary>
        ///     全连后准度pp的字符串，保留2位小数
        /// </summary>
        public string FCAccuracyPPStr => FCAccuracyPP.ToString("f2");

        /// <summary>
        ///     表示歌曲当前位置的TimeSpan的字符串形式
        /// </summary>
        public string CurrentTimeStr
        {
            get
            {
                string temp;
                if (ot.CurrentStatus == OsuGameStatus.Playing)
                {
                    temp = $"{CurrentTime.Minutes:d2}:{CurrentTime.Seconds:d2}";
                    return temp;
                }

                return "00:00";
            }
        }

        /// <summary>
        ///     表示歌曲长度的TimeSpan的字符串形式
        /// </summary>
        public string SongDurationStr
        {
            get
            {
                string temp;
                if (ot.CurrentStatus == OsuGameStatus.Playing)
                {
                    temp = $"{SongDuration.Minutes:d2}:{SongDuration.Seconds:d2}";
                    return temp;
                }

                return "00:00";
            }
        }
    }
}