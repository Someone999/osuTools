using System;
using System.Linq;
using osuTools.Attributes;
using osuTools.Beatmaps;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using RealTimePPDisplayer;

namespace osuTools
{
    public partial class ORTDPWrapper
    {
        private double lastAcc;
        private double lastC300gRate;
        private double lastC300Rate;
        private double lstHitObjectCount = 0, hitobjectPer = 0;
        private string perfectstr = "&& Perfect";
        private double tmp;
        private double tmpAcc = 0;

        /// <summary>
        ///     300g或激的数量
        /// </summary>
        [AvailableVariable("c300g", "LANG_VAR_C300G")]
        public int c300g { get; private set; }

        /// <summary>
        ///     300的数量
        /// </summary>
        [AvailableVariable("c300", "LANG_VAR_C300")]
        public int c300 { get; private set; }

        /// <summary>
        ///     200或喝的数量
        /// </summary>
        [AvailableVariable("c200", "LANG_VAR_C200")]
        public int c200 { get; private set; }

        /// <summary>
        ///     100的数量
        /// </summary>
        [AvailableVariable("c100", "LANG_VAR_C100")]
        public int c100 { get; private set; }

        /// <summary>
        ///     50的数量
        /// </summary>
        [AvailableVariable("c50", "LANG_VAR_C50")]
        public int c50 { get; private set; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        [AvailableVariable("cMiss", "LANG_VAR_CMISS")]
        public int cMiss { get; private set; }

        /// <summary>
        ///     从游戏开始到当前已经出现过了的HitObject的数量
        /// </summary>
        [AvailableVariable("cPastHitObject", "LANG_VAR_CPASSEDHITOBJECT")]
        public int cPastHitObject => CurrentMode.GetPassedHitObjectCount(this);

        /// <summary>
        ///     已经出现过了的HitObject在总HitObject中的占比
        /// </summary>
        [AvailableVariable("HitObjectPercent", "LANG_VAR_HITOBJECTPERCENT")]
        public double HitObjectPercent => testPreviousHitObjPercent;

        public double testTargetHitObjPercent { get; internal set; }

        public double testPreviousHitObjPercent
        {
            get => calcHitObjectPercent();
            internal set { }
        }

        [AvailableVariable("PreCalculatedPP", "LANG_PRECALC_PP")]
        public double PreCalculatedPP { get; private set; }

        [AvailableVariable("PreCalculatedPPStr", "LANG_PRECALC_PP")]
        public string PreCalculatedPPStr => PreCalculatedPP.ToString("f2");

        /// <summary>
        ///     已经过了的HitObject在总HitObject中的占比的格式化后的字符串，百分数，精确到两位小数
        /// </summary>
        [AvailableVariable("HitObjectPercentStr", "LANG_VAR_HITOBJECTPERCENT_STR")]
        public string HitObjectPercentStr => HitObjectPercent.ToString("p2");

        /// <summary>
        ///     当前的ModList
        /// </summary>
        [AvailableVariable("Mods", "LANG_VAR_MOD")]
        public ModList Mods { get; private set; } = new ModList();

        /// <summary>
        ///     当前的连击数
        /// </summary>
        [AvailableVariable("Combo", "LANG_VAR_COMBO")]
        public int Combo { get; private set; }

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
        public double HP { get; private set; }

        /// <summary>
        ///     谱面的状态，详见<seealso cref="OsuDB.OsuBeatmapStatus" />
        /// </summary>
        [AvailableVariable("BeatmapStatus", "LANG_VAR_BEATMAPSTATUS")]
        public OsuBeatmapStatus BeatmapStatus
        {
            get => b_status;
            internal set => b_status = value;
        }

        /// <summary>
        ///     准度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AccuracyPPStr", "LANG_VAR_ACCURACYPP_STR")]
        public string AccuracyPPStr => ppinfo.AccuracyPPStr;

        /// <summary>
        ///     定位pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AimPPStr", "LANG_VAR_AIMPP_STR")]
        public string AimPPStr => ppinfo.AimPPStr;

        /// <summary>
        ///     速度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("SpeedPPStr", "LANG_VAR_SPEEDPP_STR")]
        public string SpeedPPStr => ppinfo.SpeedPPStr;

        /// <summary>
        ///     当前PP
        /// </summary>
        [AvailableVariable("CurrentPPStr", "LANG_VAR_CURRENTPP_STR")]
        public string CurrentPPStr => ppinfo.CurrentPPStr;

        /// <summary>
        ///     分数难度加成的倍率
        /// </summary>
        [AvailableVariable("DiffcultyMultiply", "LANG_VAR_DIFFMUL")]
        public int DifficultyMultiplier { get; private set; }

        /// <summary>
        ///     最大PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxPPStr", "LANG_VAR_MAXPP_STR")]
        public string MaxPPStr => ppinfo.MaxPPStr;

        /// <summary>
        ///     最大定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAimPPStr", "LANG_VAR_MAXAIMPP_STR")]
        public string MaxAimPPStr => ppinfo.MaxAimPPStr;

        /// <summary>
        ///     最大速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxSpeedPPStr", "LANG_VAR_MAXSPEEDPP_STR")]
        public string MaxSpeedPPStr => ppinfo.MaxSpeedPPStr;

        /// <summary>
        ///     最大精度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAccuracyPPStr", "LANG_VAR_MAXACCURACYPP_STR")]
        public string MaxAccuracyPPStr => ppinfo.MaxAccuracyPPStr;

        /// <summary>
        ///     在当前状态下全连后能获得的PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcPPStr", "LANG_VAR_FCPP_STR")]
        public string FcPPStr => ppinfo.FcPPStr;

        /// <summary>
        ///     在当前状态下全连后能获得的定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAimPPStr", "LANG_VAR_FCAIMPP_STR")]
        public string FcAimPPStr => ppinfo.FcAimPPStr;

        /// <summary>
        ///     全连后速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcSpeedPPStr", "LANG_VAR_FCSPEEDPP_STR")]
        public string FcSpeedPPStr => ppinfo.FCSpeedPPStr;

        /// <summary>
        ///     全连后准度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAccuracyPPStr", "LANG_VAR_FCACCURACYPP_STR")]
        public string FcAccuracyPPStr => ppinfo.FCAccuracyPPStr;

        /// <summary>
        ///     血量的百分比形式，保留两位小数
        /// </summary>
        [AvailableVariable("HPStr", "LANG_VAR_HPSTR")]
        public string HPStr
        {
            get
            {
                double hh = HP, ff = 0;
                hh = SmoothMath.SmoothDamp(hh, HP, ref ff, 0.2, 0.033);
                return (hh / 200).ToString("p");
            }
        }

        public double AccSpeed { get; internal set; } = 0;
        public double lstAccuarcy { get; internal set; }

        /// <summary>
        ///     准度
        /// </summary>
        [AvailableVariable("Accuracy", "LANG_VAR_ACCURACY")]
        public double Accuracy => calcAcc();

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
        public double ModScoreMultiplier
        {
            get
            {
                if (CurrentMode == OsuGameMode.Mania) //如果是Mania模式
                {
                    var lst = new ModList();
                    //将原Mod列表中的降低难度筛选出来
                    var collection = Mods.Mods.Where(mod => mod.Type == ModType.DifficultyReduction);
                    //并添加到新的Mod列表里
                    lst = ModList.FromModArray(collection.ToArray());
                    //计算降低难度的Mod的分数倍率
                    return lst.ScoreMultiplier;
                    //在Mania下，只计算降低难度的Mod的分数倍率
                }

                //其余模式都会计算所有Mod的分数倍率
                return Mods.ScoreMultiplier;
            }
        }

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
        public GMMode GameMode { get; private set; }

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
        [AvailableVariable("c300gRateStr", "LANG_VAR_C300GRATE_STR")]
        public string c300gRateStr
        {
            get
            {
                if (c300g + c300 == 0) return "0%";
                return c300gRate.ToString("p");
            }
        }

        /// <summary>
        ///     300在300,200,100,50,Miss中占的百分比的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("c300RateStr", "LANG_VAR_C300RATE_STR")]
        public string c300RateStr
        {
            get
            {
                if (!double.IsNaN(c300Rate) && !double.IsInfinity(c300Rate))
                    return c300Rate.ToString("p");
                return "0%";
            }
        }

        /// <summary>
        ///     300在有效Note数量中所占的比例
        /// </summary>
        [AvailableVariable("c300Rate", "LANG_VAR_C300RATE")]
        public double c300Rate => calcC300Rate();

        /// <summary>
        ///     玩家名
        /// </summary>
        public string PlayerName { get; private set; } = "";

        /// <summary>
        ///     Mania中表示c300g在所有300中所占的比例，std及ctb中表示激在激、喝之和中所占的比例
        /// </summary>
        [AvailableVariable("c300gRate", "LANG_VAR_C300GRATE")]
        public double c300gRate => calcC300gRate();

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
        public string CurrentRank
        {
            get
            {
                // System.Windows.Forms.MessageBox.Show(OsuListenerManager.OsuStatus.CurrentStatus.Contains("Playing").ToString());
                if (GameStatus.CurrentStatus == OsuGameStatus.Playing) 
                    return CurrentMode.GetRanking(this).ToString();
                return "???";
            }
        }

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
        public int FullCombo => ppinfo.FullCombo;

        /// <summary>
        ///     谱面打击物件的数量
        /// </summary>
        public int ObjectCount => ppinfo.ObjectCount;

        /// <summary>
        ///     最大连击
        /// </summary>
        [AvailableVariable("MaxCombo", "LANG_VAR_MAXCOMBO")]
        public int MaxCombo => ppinfo.MaxCombo;

        /// <summary>
        ///     玩家的最大连击
        /// </summary>
        [AvailableVariable("PlayerMaxCombo", "LANG_VAR_PLAYERMAXCOMBO")]
        public int PlayerMaxCombo => ppinfo.PlayerMaxCombo;

        /// <summary>
        ///     当前的连击
        /// </summary>
        [AvailableVariable("CurrentCombo", "LANG_VAR_CURRENTCOMBO")]
        public int CurrentCombo => ppinfo.CurrentCombo;

        /// <summary>
        ///     准度pp
        /// </summary>
        [AvailableVariable("AccuracyPP", "LANG_VAR_ACCURACYPP")]
        public double AccuracyPP => ppinfo.AccuracyPP;

        /// <summary>
        ///     定位pp
        /// </summary>
        [AvailableVariable("AimPP", "LANG_VAR_AIMPP")]
        public double AimPP => ppinfo.AimPP;

        /// <summary>
        ///     速度pp
        /// </summary>
        [AvailableVariable("SpeedPP", "LANG_VAR_SPEEDPP")]
        public double SpeedPP => ppinfo.SpeedPP;

        /// <summary>
        ///     全连后准度pp
        /// </summary>
        [AvailableVariable("FcAccuracyPP", "LANG_VAR_FCACCURACYPP")]
        public double FcAccuracyPP => ppinfo.FCAccuracyPP;

        /// <summary>
        ///     全连后定位pp
        /// </summary>
        [AvailableVariable("FcAimPP", "LANG_VAR_FCAIMPP")]
        public double FcAimPP => ppinfo.FCAimPP;

        /// <summary>
        ///     全连后速度pp
        /// </summary>
        [AvailableVariable("FcSpeedPP", "LANG_VAR_FCSPEEDPP")]
        public double FcSpeedPP => ppinfo.FCSpeedPP;

        /// <summary>
        ///     最大准度pp
        /// </summary>
        [AvailableVariable("MaxAccuracyPP", "LANG_VAR_MAXACCURACYPP")]
        public double MaxAccuracyPP => ppinfo.MaxAccuracyPP;

        /// <summary>
        ///     最大定位pp
        /// </summary>
        [AvailableVariable("MaxAimPP", "LANG_VAR_MAXAIMPP")]
        public double MaxAimPP => ppinfo.MaxAimPP;

        /// <summary>
        ///     最大速度pp
        /// </summary>
        [AvailableVariable("MaxSpeedPP", "LANG_VAR_MAXSPEEDPP")]
        public double MaxSpeedPP => ppinfo.MaxSpeedPP;

        /// <summary>
        ///     全连后pp
        /// </summary>
        [AvailableVariable("FcPP", "LANG_VAR_FCPP")]
        public double FcPP => ppinfo.FcPP;

        /// <summary>
        ///     总pp
        /// </summary>
        [AvailableVariable("MaxPP", "LANG_VAR_MAXPP")]
        public double MaxPP => ppinfo.MaxPP;

        /// <summary>
        ///     当前pp
        /// </summary>
        [AvailableVariable("CurrentPP", "LANG_VAR_CURRENTPP")]
        public double CurrentPP
        {
            get
            {
                if (CurrentMode is IHasPerformanceCalculator h)
                    return h.GetPerformance(this);
                return ppinfo.CurrentPP;
            }
        }

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
        public int BeatmapID => Beatmap == null ? -1 : Beatmap.BeatmapID;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        [AvailableVariable("BeatmapSetId", "LANG_VAR_BEATMAPSETID")]
        public int BeatmapSetID => Beatmap == null ? -1 : Beatmap.BeatmapSetID;

        /// <summary>
        ///     此谱面打击物件的数量
        /// </summary>
        [AvailableVariable("cHitObject", "LANG_VAR_CHITOBJECT")]
        public int cHitObject => GameMode.CurrentMode.GetBeatmapHitObjectCount(Beatmap);

        /// <summary>
        ///     综合难度
        /// </summary>
        [AvailableVariable("OD", "LANG_VAR_OD")]
        public double OD => Beatmap == null ? -1 : Beatmap.OD;

        /// <summary>
        ///     掉血速度和回血难度
        /// </summary>
        [AvailableVariable("HPDrain", "LANG_VAR_HPDRAIN")]
        public double HPDrain => Beatmap == null ? -1 : Beatmap.HP;

        /// <summary>
        ///     缩圈速度
        /// </summary>
        [AvailableVariable("AR", "LANG_VAR_AR")]
        public double AR => Beatmap == null ? -1 : Beatmap.AR;

        /// <summary>
        ///     圈圈大小或Mania的键位数
        /// </summary>
        [AvailableVariable("CS", "LANG_VAR_CS")]
        public double CS => Beatmap == null ? -1 : Beatmap.CS;

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
                if (bp == null) return new TimeSpan();
                if (PlayTime <= dur.TotalMilliseconds) return dur;
                /*else if (time > dur.TotalMilliseconds + 500)
                {
                    return TimeSpan.FromMilliseconds(time);
                }*/
                return new TimeSpan();
            }
        }

        /// <summary>
        ///     当前的时间
        /// </summary>
        [AvailableVariable("CurrentTime", "LANG_VAR_CURRENTTIME")]
        public TimeSpan CurrentTime => cur;

        /// <summary>
        ///     当前时间在总时长中的占比
        /// </summary>
        [AvailableVariable("TimePercent", "LANG_VAR_TIMEPERCENT")]
        public double TimePercent => calcTimePercent();

        /// <summary>
        ///     时间的百分比，保留两位小数
        /// </summary>
        [AvailableVariable("TimePercentStr", "LANG_VAR_TIMEPERCENT_STR")]
        public string TimePercentStr => TimePercent.ToString("p");

        /// <summary>
        ///     格式化后的歌曲时间描述字符串
        /// </summary>
        public string FormatedTimeStr => ppinfo.FormatedTimeStr;

        /// <summary>
        ///     格式化后的pp描述字符串
        /// </summary>
        public string FormatedPPStr => ppinfo.FormatedPPStr;

        /// <summary>
        ///     格式化后的判定描述字符串
        /// </summary>
        public string FormatedHitStr => ppinfo.FormatedHitStr;

        /// <summary>
        ///     从RealTimePPDisplayer得到的原始字符串
        /// </summary>
        public string RawStr => ppinfo.RawStr;

        /// <summary>
        ///     当前pp在总pp中占的百分比
        /// </summary>
        [AvailableVariable("PPpercent", "LANG_VAR_PPPERCENT_STR")]
        public string PPpercent => (ppinfo.CurrentPP / ppinfo.MaxPP).ToString("p");

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

        public double TempPP { get; private set; }

        /// <summary>
        ///     是否达到Perfect判定
        /// </summary>
        [AvailableVariable("Perfect", "LANG_VAR_ISPERFECT")]
        public bool Perfect
        {
            get
            {
                if (Math.Abs(Accuracy - 1f) <= 0) return true;
                return CurrentMode.IsPerfect(this);
            }
        }

        private int maxcb;

        public int AchievedMaxCombo
        {
            get
            {
                return maxcb = Math.Max(maxcb, Combo);
            }
        }
        /// <summary>
        ///     当前时间对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("CurrentTimeStr", "LANG_VAR_CURRENTTIME_STR")]
        public string CurrentTimeStr => $"{cur.Hours * 60 + cur.Minutes:d2}:{cur.Seconds:d2}";

        /// <summary>
        ///     歌曲长度对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("SongDurationStr", "LANG_VAR_SONGDURATION_STR")]
        public string SongDurationStr => $"{dur.Hours * 60 + dur.Minutes:d2}:{dur.Seconds:d2}";

        private double calcHitObjectPercent()
        {
            double cur = cPastHitObject;
            testTargetHitObjPercent = cur / tmpHitObjectCount;
            var speed = testTargetHitObjPercent - tmp;
            if (tmp < testTargetHitObjPercent)
            {
                if (tmp == 0) tmp = testTargetHitObjPercent;
                tmp = SmoothMath.SmoothDamp(tmp, testTargetHitObjPercent, ref speed, 0.33, 0.1);
            }
            else if (tmp > testTargetHitObjPercent)
            {
                tmp = testTargetHitObjPercent;
            }

            return tmp;
        }

        private double calcAcc()
        {
            var Acc = CurrentMode.AccuracyCalc(this);
            var speed = Acc - lastAcc;
            lstAccuarcy = lastAcc;
            if (Acc == 0) lastAcc = 0;
            if (Acc == lastAcc) return Acc;
            return lastAcc = SmoothMath.SmoothDamp(lastAcc, Acc, ref speed, 0.33, 0.2);
        }

        private double calcC300Rate()
        {
            var currentC300Rate = CurrentMode.GetC300Rate(this);
            var speed = currentC300Rate - lastC300Rate;
            if (currentC300Rate == lastC300Rate) return currentC300Rate;
            return lastC300Rate = SmoothMath.SmoothDamp(lastC300Rate, currentC300Rate, ref speed, 0.1, 0.1);
        }

        private double calcC300gRate()
        {
            var currentC300gRate = CurrentMode.GetC300gRate(this);
            var speed = currentC300gRate - lastC300gRate;
            if (currentC300gRate == lastC300gRate) return currentC300gRate;
            return lastC300gRate = SmoothMath.SmoothDamp(lastC300gRate, currentC300gRate, ref speed, 0.1, 0.1);
        }

        private double calcTimePercent()
        {
            var rslt = PlayTime / SongDuration.TotalMilliseconds;
            var speed = rslt - tmpTime;
            if (rslt <= 0.0001) tmpTime = 0;
            if (tmpTime < rslt) tmpTime = SmoothMath.SmoothDamp(tmpTime, rslt, ref speed, 0.33, 0.03);
            return tmpTime;
        }
    }
}