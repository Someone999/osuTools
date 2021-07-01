using System;
using System.Linq;
using osuTools.Attributes;
using osuTools.Beatmaps;
using osuTools.Game;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using RealTimePPDisplayer;

namespace osuTools.OrtdpWrapper
{
    public partial class OrtdpWrapper
    {
        private double _lastAcc;
        private double _lastC300GRate;
        private double _lastC300Rate;
        private double _tmp;
        /// <summary>
        ///     300g或激的数量
        /// </summary>
        [AvailableVariable("CountGeki", "LANG_VAR_C300G")]
        [Alias("c300g")]
        [Alias("Count300g")]
        public int CountGeki { get; private set; }

        /// <summary>
        ///     300的数量
        /// </summary>
        [AvailableVariable("Count300", "LANG_VAR_C300")]
        [Alias("c300")]
        public int Count300 { get; private set; }

        /// <summary>
        ///     200或喝的数量
        /// </summary>
        [AvailableVariable("CountKatu", "LANG_VAR_C200")]
        [Alias("c200")]
        [Alias("Count200")]
        public int CountKatu { get; private set; }

        /// <summary>
        ///     100的数量
        /// </summary>
        [AvailableVariable("Count100", "LANG_VAR_C100")]
        [Alias("c100")]
        public int Count100 { get; private set; }

        /// <summary>
        ///     50的数量
        /// </summary>
        [AvailableVariable("Count50", "LANG_VAR_C50")]
        [Alias("c50")]
        public int Count50 { get; private set; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        [AvailableVariable("CountMiss", "LANG_VAR_CMISS")]
        [Alias("cMiss")]
        public int CountMiss { get; private set; }

        /// <summary>
        ///     从游戏开始到当前已经出现过了的HitObject的数量
        /// </summary>
        [AvailableVariable("PassedHitObjectCount", "LANG_VAR_CPASSEDHITOBJECT")]
        [Alias("cPassedHitObject")]
        [Alias("cPastHitObject")]
        public int PassedHitObjectCount => CurrentMode.GetPassedHitObjectCount(this);

        /// <summary>
        ///     已经出现过了的HitObject在总HitObject中的占比
        /// </summary>
        [AvailableVariable("HitObjectPercent", "LANG_VAR_HITOBJECTPERCENT")]
        public double HitObjectPercent => TestPreviousHitObjPercent;

        double TestTargetHitObjPercent { get; set; }

        double TestPreviousHitObjPercent => CalcHitObjectPercent();
        /// <summary>
        /// 预计算的pp保留两位小数字
        /// </summary>
        [AvailableVariable("PreCalculatedPP", "LANG_PRECALC_PP")]
        public double PreCalculatedPp { get; private set; }
        /// <summary>
        /// 预计算的pp保留两位小数
        /// </summary>
        [AvailableVariable("PreCalculatedPPStr", "LANG_PRECALC_PP")]
        public string PreCalculatedPpStr => PreCalculatedPp.ToString("f2");
        /// <summary>
        /// 测试用Catch pp
        /// </summary>

        //[AvailableVariable("TestCatchPP", "NONE")]
        public double TestCatchPp
        {
            get
            {
                if (CurrentMode is CatchMode c)
                    return GetTestPp(c.TestPerformanceCalculator(this));
                return 0;
            }
        }

        private double _tPp;
        double GetTestPp(double pp)
        {
            double speed = pp - _tPp;
            return _tPp = SmoothMath.SmoothDamp(_tPp, pp, ref speed, 0.02, 0.33);
        }
        /// <summary>
        ///     已经过了的HitObject在总HitObject中的占比的格式化后的字符串，百分数，精确到两位小数
        /// </summary>
        [AvailableVariable("HitObjectPercentStr", "LANG_VAR_HITOBJECTPERCENT_STR")]
        public string HitObjectPercentStr => HitObjectPercent.ToString("p2");

        /// <summary>
        ///     当前的ModList
        /// </summary>
        [AvailableVariable("Mods", "LANG_VAR_MOD")]
        public ModList Mods { get; } = new ModList();

        /// <summary>
        ///     当前的连击数
        /// </summary>
        [AvailableVariable("Combo", "LANG_VAR_COMBO")]
        public int Combo { get; private set; }

        /// <summary>
        /// 从RealTimePPDisplayer获取的Stars
        /// </summary>
        [AvailableVariable("RealTimeStars","LANG_VAR_RTSTARS")]
        public double RealTimeStars => _rtppi.BeatmapTuple.Stars;
        /// <summary>
        ///     当前的分数
        /// </summary>
        [AvailableVariable("Score", "LANG_VAR_SCORE")]
        public int Score { get; private set; }

        /// <summary>
        ///     游戏进行的时间，以毫秒为单位
        /// </summary>
        [AvailableVariable("PlayTime", "LANG_VAR_PLAYTIME")]
        public int PlayTime { get; private set; }

        /// <summary>
        ///     当前剩余的血量，满值为200
        /// </summary>
        [AvailableVariable("HP", "LANG_VAR_HP")]
        [Alias("Hp")]
        public double Hp { get; private set; }

        /// <summary>
        ///     谱面的状态，详见<seealso cref="OsuDB.OsuBeatmapStatus" />
        /// </summary>
        [AvailableVariable("BeatmapStatus", "LANG_VAR_BEATMAPSTATUS")]
        public OsuBeatmapStatus BeatmapStatus
        {
            get => _bStatus;
            internal set => _bStatus = value;
        }

        /// <summary>
        ///     准度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AccuracyPPStr", "LANG_VAR_ACCURACYPP_STR")]
        public string AccuracyPpStr => AccuracyPp.ToString("f2");

        /// <summary>
        ///     定位pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AimPPStr", "LANG_VAR_AIMPP_STR")]
        public string AimPpStr => AimPp.ToString("f2");

        /// <summary>
        /// 谱面最后一个HitObject的时间
        /// </summary>
        [AvailableVariable("DrainTime", "NONE")]
        public TimeSpan DrainTime => _drainTime;

        /// <summary>
        ///     速度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("SpeedPPStr", "LANG_VAR_SPEEDPP_STR")]
        public string SpeedPpStr => SpeedPp.ToString("f2");

        /// <summary>
        ///     当前PP
        /// </summary>
        [AvailableVariable("CurrentPPStr", "LANG_VAR_CURRENTPP_STR")]
        public string CurrentPpStr => CurrentPp.ToString("f2");

        /// <summary>
        ///     分数难度加成的倍率
        /// </summary>
        [AvailableVariable("DiffcultyMultiply", "LANG_VAR_DIFFMUL")]
        public int DifficultyMultiplier { get; private set; }

        /// <summary>
        ///     最大PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxPPStr", "LANG_VAR_MAXPP_STR")]
        public string MaxPpStr => MaxPp.ToString("f2");

        /// <summary>
        ///     最大定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAimPPStr", "LANG_VAR_MAXAIMPP_STR")]
        public string MaxAimPpStr => MaxAimPp.ToString("f2");

        /// <summary>
        ///     最大速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxSpeedPPStr", "LANG_VAR_MAXSPEEDPP_STR")]
        public string MaxSpeedPpStr => MaxSpeedPp.ToString("f2");
        /// <summary>
        ///     最大精度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAccuracyPPStr", "LANG_VAR_MAXACCURACYPP_STR")]
        public string MaxAccuracyPpStr => MaxAccuracyPp.ToString("f2");

        /// <summary>
        ///     在当前状态下全连后能获得的PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcPPStr", "LANG_VAR_FCPP_STR")]
        public string FcPpStr => FcPp.ToString("f2");

        /// <summary>
        ///     在当前状态下全连后能获得的定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAimPPStr", "LANG_VAR_FCAIMPP_STR")]
        public string FcAimPpStr => FcAimPp.ToString("f2");

        /// <summary>
        ///     全连后速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcSpeedPPStr", "LANG_VAR_FCSPEEDPP_STR")]
        public string FcSpeedPpStr => FcSpeedPp.ToString("f2");

        /// <summary>
        ///     全连后准度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAccuracyPPStr", "LANG_VAR_FCACCURACYPP_STR")]
        public string FcAccuracyPpStr => FcAccuracyPp.ToString("f2");

        /// <summary>
        ///     血量的百分比形式，保留两位小数
        /// </summary>
        [AvailableVariable("HPStr", "LANG_VAR_HPSTR")]
        public string HpStr
        {
            get
            {
                double hh = Hp, ff = 0;
                hh = SmoothMath.SmoothDamp(hh, Hp, ref ff, 0.2, 0.033);
                return (hh / 200).ToString("p");
            }
        }

        /// <summary>
        ///     准度
        /// </summary>
        [AvailableVariable("Accuracy", "LANG_VAR_ACCURACY")]
        public double Accuracy => CalcAcc();

        /// <summary>
        ///     准度的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("AccuracyStr", "LANG_VAR_ACCURACY_STR")]
        public string AccuracyStr => Accuracy.ToString("p2");

        /// <summary>
        ///     当前选中的谱面
        /// </summary>
        [AvailableVariable("Beatmap", "LANG_VAR_BEATMAP")]
        public Beatmap Beatmap { get; private set; }

        /// <summary>
        ///     每秒HitObject的个数
        /// </summary>
        //[AvailableVariable("JudgementPerSecond", "LANG_VAR_JUDGEMENTPERSECOND")]
        [BugPresented("Value is zero.")]
        public double JudgementPerSecond { get; private set; }

        /// <summary>
        ///     当前开启的Mod的分别的简写
        /// </summary>
        [AvailableVariable("ModShortNames", "LANG_VAR_MODSHORTNAME")]
        public string ModShortNames => Mods.GetShortModsString();

        /// <summary>
        ///     当前开启的Mod的分别的名字
        /// </summary>
        [AvailableVariable("ModNames", "LANG_VAR_MODLONGNAME")]
        public string ModNames => Mods.GetModsString();

        /// <summary>
        ///     当前开启Mod对分数倍率的影响
        /// </summary>
        [AvailableVariable("ModScoreMultiplier", "LANG_VAR_MODSCOREMULTIPLIER")]
        public double ModScoreMultiplier => Mods.ScoreMultiplier;
        /// <summary>
        ///     血量降到0以下时会触发Fail
        /// </summary>
        [AvailableVariable("CanFail", "LANG_VAR_CANFAIL")]
        public bool CanFail { get; private set; }

        /// <summary>
        ///     当前游玩的谱面
        /// </summary>
        [AvailableVariable("NowPlaying", "LANG_VAR_NOWPLAYING")]
        public string NowPlaying { get; private set; } = "";

        /// <summary>
        ///     游戏模式
        /// </summary>
        public GmMode GameMode { get; private set; }

        /// <summary>
        ///     重试的次数，请先不要使用
        /// </summary>
        public int RetryCount { get; private set; }

        /// <summary>
        ///     游玩次数，不准确
        /// </summary>
        public int PlayCount { get; } = 0;

        /// <summary>
        ///     300gRate的字符串形式。
        /// </summary>
        [AvailableVariable("CountGekiRateStr", "LANG_VAR_C300GRATE_STR")]
        [Alias("c300gRateStr")]
        public string CountGekiRateStr => CountGekiRate.ToString("p");

        /// <summary>
        ///     300在300,200,100,50,Miss中占的百分比的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("Count300RateStr", "LANG_VAR_C300RATE_STR")]
        [Alias("c300Rate")]
        public string Count300RateStr
        {
            get
            {
                if (!double.IsNaN(Count300Rate) && !double.IsInfinity(Count300Rate))
                    return Count300Rate.ToString("p");
                return "0%";
            }
        }

        /// <summary>
        ///     300在有效Note数量中所占的比例
        /// </summary>
        [AvailableVariable("c300Rate", "LANG_VAR_C300RATE")]
        public double Count300Rate => CalcC300Rate();

        /// <summary>
        ///     玩家名
        /// </summary>
        public string PlayerName { get; private set; } = "";

        /// <summary>
        ///     Mania中表示c300g在所有300中所占的比例，std及ctb中表示激在激、喝之和中所占的比例
        /// </summary>
        [AvailableVariable("c300gRate", "LANG_VAR_C300GRATE")]
        [Alias("Count300gRate")]
        [Alias("cGekiRate")]
        public double CountGekiRate => CalcC300GRate();

        /// <summary>
        ///     谱面的标签
        /// </summary>
        [AvailableVariable("Tags", "LANG_VAR_TAGS")]
        public string Tags => Beatmap == null ? "" : Beatmap.Tags;

        /// <summary>
        ///     谱面的来源
        /// </summary>
        [AvailableVariable("Source", "LANG_VAR_SOURCE")]
        public string Source => Beatmap == null ? "" : Beatmap.Source;

        //public int MaxCombo { get => mco; }
        /// <summary>
        ///     当前的结算等级
        /// </summary>
        [AvailableVariable("CurrentRank", "LANG_VAR_CURRENTRANKING")]
        public string CurrentRank =>
            // System.Windows.Forms.MessageBox.Show(OsuListenerManager.OsuStatus.CurrentStatus.Contains("Playing").ToString());
            GameStatus.CurrentStatus == OsuGameStatus.Playing ? CurrentMode.GetRanking(this).ToString() : "???";

        /// <summary>
        ///     触发改变前的模式
        /// </summary>
        [AvailableVariable("LastMode", "LANG_VAR_LASTMODE")]
        public GameMode LastMode => GameMode.LastMode;

        /// <summary>
        ///     触发改变后的模式
        /// </summary>
        [AvailableVariable("CurrentMode", "LANG_VAR_CURRENTMODE")]
        public GameMode CurrentMode => GameMode.CurrentMode;

        /// <summary>
        ///     游戏状态
        /// </summary>
        public GMStatus GameStatus { get; private set; }

        /// <summary>
        ///     改变触发前的游戏状态
        /// </summary>
        [AvailableVariable("LastStatus", "LANG_VAR_LASTSTATUS")]
        public OsuGameStatus LastStatus => GameStatus.LastStatus;

        /// <summary>
        ///     当前的游戏状态
        /// </summary>
        [AvailableVariable("CurrentStatus", "LANG_VAR_CURRENTSTATUS")]
        public OsuGameStatus CurrentStatus => GameStatus.CurrentStatus;

        //GMMod NMod { get => mo; }
        /// <summary>
        ///     系统时间
        /// </summary>
        [AvailableVariable("SysTime", "LANG_VAR_SYSTIME")]
        public DateTime SysTime => DateTime.Now;

        /// <summary>
        ///     谱面最大连击
        /// </summary>
        public int FullCombo => _rtppi.HitCount.FullCombo;

        /// <summary>
        ///     谱面打击物件的数量
        /// </summary>
        public int ObjectCount => _rtppi.BeatmapTuple.ObjectsCount;

        /// <summary>
        ///     最大连击
        /// </summary>
        [AvailableVariable("MaxCombo", "LANG_VAR_MAXCOMBO")]
        public int MaxCombo => _rtppi.HitCount.CurrentMaxCombo;

        /// <summary>
        ///     玩家的最大连击
        /// </summary>
        [AvailableVariable("PlayerMaxCombo", "LANG_VAR_PLAYERMAXCOMBO")]
        public int PlayerMaxCombo => _rtppi.HitCount.PlayerMaxCombo;

        /// <summary>
        ///     当前的连击
        /// </summary>
        [AvailableVariable("CurrentCombo", "LANG_VAR_CURRENTCOMBO")]
        public int CurrentCombo => Combo;

        /// <summary>
        ///     准度pp
        /// </summary>
        [AvailableVariable("AccuracyPP", "LANG_VAR_ACCURACYPP")]
        public double AccuracyPp => _rtppi.SmoothPP.RealTimeAccuracyPP;
            /// <summary>
        ///     定位pp
        /// </summary>
        [AvailableVariable("AimPP", "LANG_VAR_AIMPP")]
        public double AimPp => _rtppi.SmoothPP.RealTimeAimPP;

        /// <summary>
        ///     速度pp
        /// </summary>
        [AvailableVariable("SpeedPP", "LANG_VAR_SPEEDPP")]
        public double SpeedPp=> _rtppi.SmoothPP.RealTimeSpeedPP;

        /// <summary>
        ///     全连后准度pp
        /// </summary>
        [AvailableVariable("FcAccuracyPP", "LANG_VAR_FCACCURACYPP")]
        public double FcAccuracyPp=> _rtppi.SmoothPP.FullComboAccuracyPP;

        /// <summary>
        ///     全连后定位pp
        /// </summary>
        [AvailableVariable("FcAimPP", "LANG_VAR_FCAIMPP")]
        public double FcAimPp => _rtppi.SmoothPP.FullComboAimPP;

        /// <summary>
        ///     全连后速度pp
        /// </summary>
        [AvailableVariable("FcSpeedPP", "LANG_VAR_FCSPEEDPP")]
        public double FcSpeedPp => _rtppi.SmoothPP.FullComboSpeedPP;

        /// <summary>
        ///     最大准度pp
        /// </summary>
        [AvailableVariable("MaxAccuracyPP", "LANG_VAR_MAXACCURACYPP")]
        public double MaxAccuracyPp
            => _rtppi.SmoothPP.MaxAccuracyPP;

        /// <summary>
        ///     最大定位pp
        /// </summary>
        [AvailableVariable("MaxAimPP", "LANG_VAR_MAXAIMPP")]
        public double MaxAimPp => _rtppi.SmoothPP.MaxAimPP;

        /// <summary>
        ///     最大速度pp
        /// </summary>
        [AvailableVariable("MaxSpeedPP", "LANG_VAR_MAXSPEEDPP")]
        public double MaxSpeedPp => _rtppi.SmoothPP.MaxSpeedPP;

        /// <summary>
        ///     全连后pp
        /// </summary>
        [AvailableVariable("FcPP", "LANG_VAR_FCPP")]
        public double FcPp => _rtppi.SmoothPP.FullComboPP;
        /// <summary>
        ///     总pp
        /// </summary>
        [AvailableVariable("MaxPP", "LANG_VAR_MAXPP")]
        public double MaxPp => _rtppi.SmoothPP.MaxPP;

        /// <summary>
        ///     当前pp
        /// </summary>
        [AvailableVariable("CurrentPP", "LANG_VAR_CURRENTPP")]
        public double CurrentPp => _rtppi.SmoothPP.RealTimePP;

        /// <summary>
        ///     标题
        /// </summary>
        [AvailableVariable("Title", "LANG_VAR_TITLE")]
        public string Title => Beatmap == null ? "" : Beatmap.Title;

        /// <summary>
        ///     UTF8编码的标题
        /// </summary>
        [AvailableVariable("TitleUnicode", "LANG_VAR_TITLEUNICODE")]
        public string TitleUnicode => Beatmap == null ? "" : Beatmap.TitleUnicode;

        /// <summary>
        ///     艺术家
        /// </summary>
        [AvailableVariable("Artist", "LANG_VAR_ARTIST")]
        public string Artist => Beatmap == null ? "" : Beatmap.Artist;

        /// <summary>
        ///     UTF8编码的艺术家
        /// </summary>
        [AvailableVariable("ArtistUnicode", "LANG_VAR_ARTISTUNICODE")]
        public string ArtistUnicode => Beatmap == null ? "" : Beatmap.ArtistUnicode;

        /// <summary>
        ///     谱面的作者
        /// </summary>
        [AvailableVariable("Creator", "LANG_VAR_CREATOR")]
        public string Creator => Beatmap == null ? "" : Beatmap.Creator;

        /// <summary>
        ///     谱面难度
        /// </summary>
        [AvailableVariable("Difficulty", "LANG_VAR_DIFFICULTY")]
        public string Difficulty => Beatmap == null ? "" : Beatmap.Difficulty;

        /// <summary>
        ///     谱面难度
        /// </summary>
        [AvailableVariable("Version", "LANG_VAR_VERSION")]
        public string Version => Beatmap == null ? "" : Beatmap.Version;

        /// <summary>
        ///     谱面文件的文件名
        /// </summary>
        [AvailableVariable("FileName", "LANG_VAR_FILENAME")]
        public string FileName => Beatmap == null ? "" : Beatmap.FileName;

        /// <summary>
        ///     谱面文件的全路径
        /// </summary>
        [AvailableVariable("FullPath", "LANG_VAR_FULLPATH")]
        public string FullPath => Beatmap == null ? "" : Beatmap.FullPath;

        /// <summary>
        ///     谱面的下载链接
        /// </summary>
        [AvailableVariable("DownloadLink", "LANG_VAR_DOWNLOADLINK")]
        public string DownloadLink => Beatmap == null ? "" : Beatmap.DownloadLink;

        /// <summary>
        ///     谱面的背景图片的文件名
        /// </summary>
        [AvailableVariable("BackgroundFileName", "LANG_VAR_BACKGROUNDFILENAME")]
        public string BackgroundFileName => Beatmap == null ? "" : Beatmap.BackgroundFileName;

        /// <summary>
        ///     谱面ID
        /// </summary>
        [AvailableVariable("BeatmapId", "LANG_VAR_BEATMAPID")]
        [Alias("BeatmapID")]
        public int BeatmapId => Beatmap == null ? -1 : Beatmap.BeatmapId;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        [AvailableVariable("BeatmapSetId", "LANG_VAR_BEATMAPSETID")]
        public int BeatmapSetId => Beatmap == null ? -1 : Beatmap.BeatmapSetId;

        /// <summary>
        ///     此谱面打击物件的数量
        /// </summary>
        [AvailableVariable("HitObjectCount", "LANG_VAR_CHITOBJECT")]
        [Alias("cHitObject")]
        public int HitObjectCount => GameMode.CurrentMode.GetBeatmapHitObjectCount(Beatmap);

        /// <summary>
        ///     综合难度
        /// </summary>
        [AvailableVariable("OverallDifficulty", "LANG_VAR_OD")]
        [Alias("OD")]
        public double OverallDifficulty => Beatmap == null ? -1 : Beatmap.OverallDifficulty;

        /// <summary>
        ///     掉血速度和回血难度
        /// </summary>
        [AvailableVariable("HPDrain", "LANG_VAR_HPDRAIN")]
        public double HpDrain => Beatmap == null ? -1 : Beatmap.HpDrain;

        /// <summary>
        ///     缩圈速度
        /// </summary>
        [AvailableVariable("ApproachRate", "LANG_VAR_AR")]
        [Alias("AR")]
        public double ApproachRate => Beatmap == null ? -1 : Beatmap.ApproachRate;

        /// <summary>
        ///     圈圈大小或Mania的键位数
        /// </summary>
        [AvailableVariable("CircleSize", "LANG_VAR_CS")]
        [Alias("CS")]
        public double CircleSize => Beatmap == null ? -1 : Beatmap.CircleSize;

        /// <summary>
        ///     音频文件名称
        /// </summary>
        [AvailableVariable("AudioFileName", "LANG_VAR_AUDIOFILENAME")]
        public string AudioFileName => Beatmap == null ? "" : Beatmap.AudioFileName;

        /// <summary>
        ///     歌曲的长度
        /// </summary>
        [AvailableVariable("SongDuration", "LANG_VAR_SONGDURATION")]
        public TimeSpan SongDuration
        {
            get
            {
                if (_osuBeatmap == null) return new TimeSpan();
                if (PlayTime <= _dur.TotalMilliseconds) return _dur;
                return new TimeSpan();
            }
        }

        /// <summary>
        ///     当前的时间
        /// </summary>
        [AvailableVariable("CurrentTime", "LANG_VAR_CURRENTTIME")]
        public TimeSpan CurrentTime => _cur;

        /// <summary>
        ///     当前时间在总时长中的占比
        /// </summary>
        [AvailableVariable("TimePercent", "LANG_VAR_TIMEPERCENT")]
        public double TimePercent => CalcTimePercent();

        /// <summary>
        ///     时间的百分比，保留两位小数
        /// </summary>
        [AvailableVariable("TimePercentStr", "LANG_VAR_TIMEPERCENT_STR")]
        public string TimePercentStr => TimePercent.ToString("p");

        /// <summary>
        ///     当前pp在总pp中占的百分比
        /// </summary>
        [AvailableVariable("PpPercent", "LANG_VAR_PPPERCENT_STR")]
        [Alias("PPpercent")]
        public string PpPercent => (CurrentPp / MaxPp).ToString("p");

        /// <summary>
        ///     谱面的难度星级
        /// </summary>
        [AvailableVariable("Stars", "LANG_VAR_STARS")]
        public double Stars => Beatmap == null ? -1 : Beatmap.Stars;

        /// <summary>
        ///     谱面的难度星级的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("StarsStr", "LANG_VAR_STARS_STR")]
        public string StarsStr => Stars.ToString("f2");
        /// <summary>
        /// 测试用pp
        /// </summary>
        public double TempPp { get; private set; }

        /// <summary>
        ///     是否达到Perfect判定
        /// </summary>
        [AvailableVariable("Perfect", "LANG_VAR_ISPERFECT")]
        public bool Perfect => Math.Abs(Accuracy - 1f) <= double.Epsilon || CurrentMode.IsPerfect(this);

        private int _maxcb;
        /// <summary>
        /// 玩家达到过的最大连击
        /// </summary>
        public int AchievedMaxCombo => _maxcb = Math.Max(_maxcb, Combo);

        /// <summary>
        ///     当前时间对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("CurrentTimeStr", "LANG_VAR_CURRENTTIME_STR")]
        public string CurrentTimeStr => $"{_cur.Hours * 60 + _cur.Minutes:d2}:{_cur.Seconds:d2}";

        /// <summary>
        ///     歌曲长度对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("SongDurationStr", "LANG_VAR_SONGDURATION_STR")]
        public string SongDurationStr => $"{_dur.Hours * 60 + _dur.Minutes:d2}:{_dur.Seconds:d2}";

        private double CalcHitObjectPercent()
        {
            double cur = PassedHitObjectCount;
            TestTargetHitObjPercent = cur / _tmpHitObjectCount;
            var speed = TestTargetHitObjPercent - _tmp;
            if (_tmp < TestTargetHitObjPercent)
            {
                if (_tmp == 0) _tmp = TestTargetHitObjPercent;
                _tmp = SmoothMath.SmoothDamp(_tmp, TestTargetHitObjPercent, ref speed, 0.33, 0.1);
            }
            else if (_tmp > TestTargetHitObjPercent)
            {
                _tmp = TestTargetHitObjPercent;
            }

            return _tmp;
        }

        private double CalcAcc()
        {
            var acc = CurrentMode.AccuracyCalc(this);
            var speed = acc - _lastAcc;
            if (acc == 0) _lastAcc = 0;
            if (Math.Abs(acc - _lastAcc) < double.Epsilon) return acc;
            return _lastAcc = SmoothMath.SmoothDamp(_lastAcc, acc, ref speed, 0.33, 0.25);
        }

        private double CalcC300Rate()
        {
            var currentC300Rate = CurrentMode.GetCount300Rate(this);
            var speed = currentC300Rate - _lastC300Rate;
            if (currentC300Rate == 0) _lastC300Rate = 0;
            if (Math.Abs(currentC300Rate - _lastC300Rate) < double.Epsilon) return currentC300Rate;
            return _lastC300Rate = SmoothMath.SmoothDamp(_lastC300Rate, currentC300Rate, ref speed, 0.3, 0.1);
        }

        private double CalcC300GRate()
        {
            var currentC300GRate = CurrentMode.GetCountGekiRate(this);
            var speed = currentC300GRate - _lastC300GRate;
            if (currentC300GRate == 0) _lastC300GRate = 0;
            if (Math.Abs(currentC300GRate - _lastC300GRate) < double.Epsilon) return currentC300GRate;
            return _lastC300GRate = SmoothMath.SmoothDamp(_lastC300GRate, currentC300GRate, ref speed, 0.3, 0.2);
        }

        private double CalcTimePercent()
        {
            var rslt = PlayTime / SongDuration.TotalMilliseconds;
            var speed = rslt - _tmpTime;
            if (rslt <= 0.0001) _tmpTime = 0;
            if (_tmpTime < rslt) _tmpTime = SmoothMath.SmoothDamp(_tmpTime, rslt, ref speed, 0.33, 0.03);
            return _tmpTime;
        }
    }
}