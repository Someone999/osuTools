using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OsuRTDataProvider;
using OsuRTDataProvider.Listen;
using OsuRTDataProvider.Mods;
using osuTools.Attributes;
using osuTools.Beatmaps;
using osuTools.ExtraMethods;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.osuToolsException;
using Sync.Tools;
using Beatmap = OsuRTDataProvider.BeatmapInfo.Beatmap;
using Timer = System.Timers.Timer;

namespace osuTools
{
    partial class ORTDPWrapper
    {
        private int allhit;
        private OsuBeatmap bp;
        private BreakTimeCollection breaktimes = new BreakTimeCollection();
        private int cBtime;
        private int cTmpoint;
        private int i;
        private MD5String md5str = new MD5String(), md5str1 = new MD5String();
        private bool noFailTriggered;
        private TimePointCollection timepoints = new TimePointCollection();
        private Timer timer1 = new Timer();
        private int tmpHitObjectCount;
        private double tmpTime;

        /// <summary>
        ///     OsuRTDataProvider提供的Beatmap
        /// </summary>
        public Beatmap ORTDPBeatmap { get; private set; }

        /// <summary>
        ///     当前BreakTime剩余的时间
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "LANG_VAR_REMAINBREAKTIME")]
        public TimeSpan RemainingBreakTime { get; private set; }

        /// <summary>
        ///     BreakTime的数量
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "LANG_VAR_CBREAKTIME")]
        public int BreakTimeCount => Beatmap is null ? 0 : Beatmap.BreakTimes.Count;

        /// <summary>
        ///     距离下一个BreakTime的时间
        /// </summary>
        [AvailableVariable("TimeToNextBreakTime", "LANG_VAR_TIMETONEXTBREAKTIME")]
        public TimeSpan TimeToNextBreakTime { get; private set; }

        /// <summary>
        ///     当前BreakTime剩余的时间，以秒为单位
        /// </summary>
        [AvailableVariable("RemainingBreakTimeStr", "LANG_VAR_REMAINBREAKTIME_STR")]
        public string RemainingBreakTimeStr => RemainingBreakTime.TotalSeconds.ToString("f2");

        /// <summary>
        ///     距离下一个BreakTime的时间，以秒为单位
        /// </summary>
        [AvailableVariable("TimeToNextBreakTimeStr", "LANG_VAR_TIMETONEXTBREAKTIME_STR")]
        public string TimeToNextBreakTimeStr => TimeToNextBreakTime.TotalSeconds.ToString("f2");

        /// <summary>
        ///     谱面当前的非继承时间线(编辑器中红色时间线)后的BPM
        /// </summary>
        [AvailableVariable("CurrentBPM", "LANG_VAR_CURRENTBPM")]
        public double CurrentBPM { get; private set; }

        /// <summary>
        ///     CurrentBPM保留两位小数
        /// </summary>
        [AvailableVariable("CurrentBPMStr", "LANG_VAR_CURRENTBPM_STR")]
        public string CurrentBPMStr => CurrentBPM.ToString("f2");

        private bool InDebugMode()
        {
            return DebugMode;
        }

        private void HitChanged()
        {
            Task.Run(new Action(() =>
            {
                while (true)
                {
                    if (GameMode.CurrentMode == OsuGameMode.Mania)
                    {
                        allhit = c300g + c300 + c200 + c100 + c50 + cMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = c300g + c300 + c200 + c100 + c50 + cMiss - allhit;
                    }

                    if (GameMode.CurrentMode == OsuGameMode.Osu)
                    {
                        allhit = c300 + c100 + c50 + cMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = c300 + c100 + c50 + cMiss - allhit;
                    }

                    if (GameMode.CurrentMode == OsuGameMode.Catch)
                    {
                        allhit = c300 + c50 + cMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = c300 + c50 + cMiss - allhit;
                    }
                }
            }));
        }

        private void InitLisenter()
        {
            //InfoReaderPlugin.InfoReader reader = new InfoReaderPlugin.InfoReader();
            
            p = new OsuRTDataProviderPlugin();
            p.OnEnable();
            lm = p.ListenerManager;
            lm.OnCountGekiChanged += Lm_OnCountGekiChanged;
            lm.OnCount300Changed += Lm_OnCount300Changed;
            lm.OnCountKatuChanged += Lm_OnCountKatuChanged;
            lm.OnCount100Changed += Lm_OnCount100Changed;
            lm.OnCount50Changed += Lm_OnCount50Changed;
            lm.OnCountMissChanged += Lm_OnCountMissChanged;
            lm.OnComboChanged += Lm_OnComboChanged;
            lm.OnScoreChanged += Lm_OnScoreChanged;
            lm.OnPlayingTimeChanged += Lm_OnPlayingTimeChanged;
            lm.OnHealthPointChanged += Lm_OnHealthPointChanged;
            lm.OnAccuracyChanged += Lm_OnAccuracyChanged;
            lm.OnBeatmapChanged += Lm_OnBeatmapChanged;
            lm.OnModsChanged += Lm_OnModsChanged;
            lm.OnPlayModeChanged += Lm_OnPlayModeChanged;
            lm.OnStatusChanged += Lm_OnStatusChanged;
            lm.OnPlayerChanged += Lm_OnPlayerChanged;
            GameMode = new GMMode(OsuPlayMode.Unknown, OsuPlayMode.Unknown);
            GameStatus = new GMStatus(OsuListenerManager.OsuStatus.Unkonwn, OsuListenerManager.OsuStatus.Unkonwn);
            HitChanged();
            Beatmap = null;
        }

        private void Lm_OnPlayerChanged(string player)
        {
            PlayerName = player.Trim();
        }

        private void InitLisenter(OsuRTDataProviderPlugin pl)
        {
            
            p = pl;
            lm = p.ListenerManager;
            lm.OnCountGekiChanged += Lm_OnCountGekiChanged;
            lm.OnCount300Changed += Lm_OnCount300Changed;
            lm.OnCountKatuChanged += Lm_OnCountKatuChanged;
            lm.OnCount100Changed += Lm_OnCount100Changed;
            lm.OnCount50Changed += Lm_OnCount50Changed;
            lm.OnCountMissChanged += Lm_OnCountMissChanged;
            lm.OnComboChanged += Lm_OnComboChanged;
            lm.OnScoreChanged += Lm_OnScoreChanged;
            lm.OnPlayingTimeChanged += Lm_OnPlayingTimeChanged;
            lm.OnHealthPointChanged += Lm_OnHealthPointChanged;
            lm.OnAccuracyChanged += Lm_OnAccuracyChanged;
            lm.OnBeatmapChanged += Lm_OnBeatmapChanged;
            lm.OnModsChanged += Lm_OnModsChanged;
            lm.OnPlayModeChanged += Lm_OnPlayModeChanged;
            lm.OnStatusChanged += Lm_OnStatusChanged;
            lm.OnHitEventsChanged += Lm_OnHitEventsChanged;
            GameMode = new GMMode(OsuPlayMode.Unknown, OsuPlayMode.Unknown);
            GameStatus = new GMStatus(OsuListenerManager.OsuStatus.Unkonwn, OsuListenerManager.OsuStatus.Unkonwn);
            HitChanged();
            Beatmap = null;
        }

        private void Lm_OnHitEventsChanged(PlayType playType, List<HitEvent> hitEvents)
        {
            //IO.CurrentIO.Write(hitEvents.Last().TimeStamp.ToString());
        }

        private void Lm_OnPlayModeChanged(OsuPlayMode last, OsuPlayMode mode)
        {
            GameMode = new GMMode(last, mode);
            CanFail = !Mods.Mods.Any(innerMod =>
                innerMod is NoFailMod || innerMod is AutoPilotMod || innerMod is RelaxMod);
            /*if (CurrentMode is IHasPerformanceCalculator m)
            {
                if (Beatmap.Mode == OsuGameMode.Mania)
                    tmpPP = new ManiaMode().GetMaxPerformance(this);
                else
                    tmpPP = m.GetMaxPerformance(this);
            }*/
        }

        private void Lm_OnModsChanged(ModsInfo mods)
        {
            //MessageBox.Show(((int)mods.Mod).ToString());
            Mods = ModList.FromInteger((int) mods.Mod).Mods.Where(m => m.CheckAndSetForMode(CurrentMode)).ToArray()
                .ToModList();
            if (CurrentMode == OsuGameMode.Mania && Mods.Mods.Any(mod => mod is ScoreV2Mod))
                tmpHitObjectCount = hitObjects.Count +
                                    hitObjects.Where(h => h.HitObjectType == HitObjectTypes.ManiaHold).Count();
            else tmpHitObjectCount = hitObjects.Count;
            if (DebugMode)
                if (tmpHitObjectCount != hitObjects.Count)
                    IO.CurrentIO.Write($"[osuTools] HitObject Count after applied Mods: {tmpHitObjectCount}.");
        }

        private void Lm_OnStatusChanged(OsuListenerManager.OsuStatus last_status, OsuListenerManager.OsuStatus status)
        {
            GameStatus = new GMStatus(last_status, status);
            if (status == OsuListenerManager.OsuStatus.SelectSong || status == OsuListenerManager.OsuStatus.Rank)
                RetryCount = 0;
            while (status != OsuListenerManager.OsuStatus.Rank && status != OsuListenerManager.OsuStatus.Playing)
            {
                if (c300 != 0 || c300g != 0 || c200 != 0 || c100 != 0 || c50 != 0 || cMiss != 0 || Score != 0 ||
                    acc != 0)
                    setzero();
                else
                    break;
                if (!(Beatmap is null))
                {
                    if (!string.IsNullOrEmpty(NowPlaying))
                        NowPlaying = $"{Beatmap.Artist} - {Beatmap.Title} [{Beatmap.Difficulty}]";
                    stars = Beatmap.Stars;
                }
            }
        }

        private void setzero()
        {
            c300 = 0;
            c300g = 0;
            c200 = 0;
            c100 = 0;
            c50 = 0;
            cMiss = 0;
            Score = 0;
            acc = 0;
            i = 0;
            cBtime = 0;
            cTmpoint = 0;
            maxcb = 0;
            Ranking = "???";
        }

        private void ReadFromORTDP(Beatmap beatmap)
        {
            NowPlaying = "Loading...";
            b_status = OsuBeatmapStatus.Loading;
            if (beatmap == null || beatmap == OsuRTDataProvider.BeatmapInfo.Beatmap.Empty)
                IO.CurrentIO.WriteColor("Beatmap is null or empty.", ConsoleColor.Red, true, false);
            Beatmap = new Beatmaps.Beatmap(beatmap);
            if (Beatmap == null)
            {
                IO.CurrentIO.Write("Fail to read beatmap.", true, false);
            }
            else
            {
                NowPlaying = Beatmap.ToString();
                Thread.Sleep(100);
                stars = Beatmap.Stars;
                hitObjects = Beatmap.HitObjects;
                b_status = OsuBeatmapStatus.Unknown;
                dur = hitObjects.LastOrDefault() == null
                    ? TimeSpan.FromMilliseconds(0)
                    : TimeSpan.FromMilliseconds(hitObjects.LastOrDefault().Offset);
                if (DebugMode) IO.CurrentIO.Write("Beatmap read from ORTDP.", true, false);
            }
        }

        private void ReadFromOsudb(Beatmap map)
        {
            NowPlaying = "Loading...";
            b_status = OsuBeatmapStatus.Loading;
            if (md5str is null || md5str1 is null) return;
            md5str = new MD5String("");
            if (md5str is null || md5str1 is null) return;
            if (md5str != md5str1)
            {
                bd = new OsuBeatmapDB();
                md5str1 = md5str;
            }

            try
            {
                if (map != null)
                {
                    var md5 = MD5String.GetString(
                        new MD5CryptoServiceProvider().ComputeHash(File.ReadAllBytes(map.FilenameFull)));
                    var beatmapa = bd.Beatmaps.FindByMD5(md5);
                    if (!(beatmapa is null))
                    {
                        Beatmap = new Beatmaps.Beatmap(beatmapa);
                        bp = beatmapa;
                        NowPlaying = Beatmap.ToString();
                        hitObjects = Beatmap.HitObjects;
                        b_status = beatmapa.BeatmapStatus;
                        dur = beatmapa.TotalTime;
                        if (CurrentMode is ILegacyMode le)
                            stars = beatmapa.ModStarPair[le.LegacyMode][0];
                        else stars = 0;
                        if (DebugMode) IO.CurrentIO.Write("Beatmap read from OsuDB.", true, false);
                    }
                    else
                    {
                        IO.CurrentIO.WriteColor("Fail to get beatmap info.", ConsoleColor.Red, true, false);
                    }
                }
            }
            catch (BeatmapNotFoundException)
            {
                var m5 = bd.MD5;
                bd = new OsuBeatmapDB();
                var m51 = bd.MD5;
                if (m5 == m51)
                    ReadFromORTDP(map);
                //IO.CurrentIO.WriteColor("Can not find this beatmap by MD5 in osu!DataBase.Beatmap has read from osu file,Info may be correct after re-read OsuDataBase.", ConsoleColor.Red);
            }
            catch (Exception)
            {
            }
        }

        private int getDifficultyMul(double val)
        {
            if (val < 0) throw new ArgumentOutOfRangeException("Difficulty must greater than or equals to 0.");
            if (val >= 0 && val <= 5) return 2;
            if (val >= 6 && val <= 12) return 3;
            if (val >= 13 && val <= 17) return 4;
            if (val >= 18 && val <= 24) return 5;
            if (val >= 25 && val <= 30) return 6;
            if (val > 30) return 6;
            return -1;
        }

        private void Lm_OnBeatmapChanged(Beatmap map)
        {
            try
            {
                ORTDPBeatmap = map;
                Beatmap = new Beatmaps.Beatmap();
                if (DebugMode)
                    IO.CurrentIO.Write("[osuTools] Beatmap Changed");
                if (BeatmapReadMethod == BeatmapReadMethods.OsuRTDataProvider) ReadFromORTDP(map);
                if (BeatmapReadMethod == BeatmapReadMethods.OsuDB) ReadFromOsudb(map);
                PreCalculatedPP = 0;
                if (CurrentMode is IHasPerformanceCalculator m)
                {
                    if (Beatmap.Mode != OsuGameMode.Osu)
                    {
                        if (Game.Modes.GameMode.FromLegacyMode(Beatmap.Mode) is IHasPerformanceCalculator bm)
                            PreCalculatedPP = bm.GetMaxPerformance(this);
                    }
                    else
                    {
                        PreCalculatedPP = m.GetMaxPerformance(this);
                    }
                }

                i = 0;
                timepoints.TimePoints = Beatmap.TimePoints.TimePoints.Where(tp => tp.Uninherited).ToList();
                breaktimes = Beatmap.BreakTimes;
                NowPlaying = Beatmap.ToString();
                var OsuMode = CurrentMode == OsuGameMode.Osu && Beatmap.Mode == OsuGameMode.Osu;
                var CtbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Catch;
                var compatibleCtbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Osu;
                if (OsuMode || CtbMode || compatibleCtbMode)
                    DifficultyMultiplier = getDifficultyMul(Beatmap.CS + Beatmap.OD + Beatmap.HP);
                if (DebugMode)
                {
                    if (!(Beatmap is null))
                    {
                        var hit = Beatmap.HitObjects;
                        var tm = Beatmap.TimePoints;
                        var uinh = Beatmap.TimePoints.TimePoints.Where(t => t.Uninherited).ToArray();
                        var inh = Beatmap.TimePoints.TimePoints.Where(t => !t.Uninherited).ToArray();
                        var btm = Beatmap.BreakTimes;
                        var builder = new StringBuilder();
                        builder.AppendLine($"Beatmap: {Beatmap}\nHitObject Count: {hit.Count}");
                        builder.AppendLine($"Mode:{Beatmap.Mode}");
                        builder.AppendLine($"BreakTime: {btm.Count}");
                        builder.AppendLine(
                            $"TimePoints: {tm.Count} Inherited TimePoint Count: {inh.Count()} UnInherited TimePoint Count: {uinh.Count()}");
                        builder.AppendLine($"[Beatmap BreakTime Detector] BreakTime:{breaktimes.Count}");
                        builder.AppendLine(
                            $"[Beatmap Uninherited TimePoint Detector] Uninherited TimePoint:{timepoints.Count}");
                        IO.CurrentIO.Write(builder.ToString(), true, false);
                    }
                    else
                    {
                        IO.CurrentIO.Write("[osuTools] Beatmap is null", true, false);
                    }
                }
            }
            catch (KeyNotFoundException)
            {
            }
            catch (Exception ex)
            {
                IO.CurrentIO.WriteColor($"[osuTools] Exception:{ex}", ConsoleColor.Red);
            }
        }

        private void Lm_OnCountGekiChanged(int hit)
        {
            c300g = hit;
        }

        private void Lm_OnCount300Changed(int hit)
        {
            c300 = hit;
        }

        private void Lm_OnCountKatuChanged(int hit)
        {
            c200 = hit;
        }

        private void Lm_OnCount100Changed(int hit)
        {
            c100 = hit;
        }

        private void Lm_OnCount50Changed(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    RetryCount++;
            c50 = hit;
        }

        private void Lm_OnCountMissChanged(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("SD"))
                    OnFail(this);
            cMiss = hit;
        }

        private void Lm_OnComboChanged(int combo)
        {
            Combo = combo;
        }

        private void Lm_OnPlayingTimeChanged(int ms)
        {
            if (CurrentStatus == OsuGameStatus.Playing)
                calcHitObjectPercent();
            if (bp != null)
            {
                tmper = ms / SongDuration.TotalMilliseconds;
                if (CurrentStatus == OsuGameStatus.Playing)
                {
                    if (timepoints.Count > 0)
                    {
                        if (ms < timepoints[0].Offset)
                        {
                            cTmpoint = 0;
                            CurrentBPM = timepoints[cTmpoint].BPM;
                        }

                        if (cTmpoint < timepoints.Count)
                        {
                            CurrentBPM = timepoints[cTmpoint].BPM;
                            foreach (var m in Mods)
                                if (m is IChangeTimeRateMod changeTimeRateMod)
                                    CurrentBPM *= changeTimeRateMod.TimeRate;
                            if (ms > timepoints[cTmpoint + 1 < timepoints.Count - 1 ? cTmpoint + 1 : cTmpoint].Offset)
                                cTmpoint = cTmpoint == timepoints.Count - 1 ? cTmpoint : cTmpoint + 1;
                        }
                    }

                    if (breaktimes.Count > 0)
                        try
                        {
                            var curBreaktime = breaktimes[cBtime > breaktimes.Count - 1 ? breaktimes.Count : cBtime];
                            if (breaktimes.Count > 0)
                            {
                                if (ms < breaktimes[0].Start)
                                {
                                    cBtime = 0;
                                    curBreaktime = breaktimes[0];
                                    TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                }

                                if (ms > breaktimes.BreakTimes.Last().End)
                                {
                                    curBreaktime = BreakTime.ZeroBreakTime;
                                    RemainingBreakTime = TimeSpan.Zero;
                                    TimeToNextBreakTime = TimeSpan.Zero;
                                }
                                else
                                {
                                    if (ms > breaktimes[cBtime > breaktimes.Count - 1 ? breaktimes.Count - 1 : cBtime]
                                        .End)
                                    {
                                        if (cBtime < breaktimes.Count)
                                        {
                                            TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                            cBtime = cBtime == breaktimes.Count - 1 ? cBtime : cBtime + 1;
                                        }
                                        else
                                        {
                                            RemainingBreakTime = TimeSpan.Zero;
                                            TimeToNextBreakTime = TimeSpan.Zero;
                                        }
                                    }

                                    if (curBreaktime.InBreakTime(ms))
                                    {
                                        RemainingBreakTime = TimeSpan.FromMilliseconds(curBreaktime.End - ms);
                                        TimeToNextBreakTime = TimeSpan.Zero;
                                    }
                                    else
                                    {
                                        RemainingBreakTime = TimeSpan.Zero;
                                        TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                    }
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            IO.CurrentIO.Write("BreakTime List Index out of range.");
                            cBtime = breaktimes.Count - 1;
                            RemainingBreakTime = TimeSpan.Zero;
                            TimeToNextBreakTime = TimeSpan.Zero;
                        }
                    else
                        RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                }
                else
                {
                    RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                    if (timepoints.Count > 0) CurrentBPM = timepoints[0].BPM;
                }
            }

            if (tmper > 1) tmper = 1;
            if (CurrentStatus != OsuGameStatus.Playing) tmper = 0;
            if (CurrentStatus == OsuGameStatus.Rank && LastStatus == OsuGameStatus.Playing) tmper = 1;
            PlayTime = ms;
            cur = TimeSpan.FromMilliseconds(ms);
        }

        private void Lm_OnScoreChanged(int obj)
        {
            try
            {
                double current = obj - Score;
                double retryflag = 0;
                Score = obj;
                var ScoreZeroed = obj == 0;
                if (CurrentStatus == OsuGameStatus.SelectSong)
                    retryflag++;
                if (ScoreZeroed && retryflag >= 0 && PlayerMaxCombo != 0)
                    if (CurrentStatus != OsuGameStatus.Rank)
                    {
                        if (InDebugMode())
                            IO.CurrentIO.WriteColor($"[osuTools] Retry at {PlayTime}ms", ConsoleColor.Yellow);
                        RetryCount++;
                        PlayTime = 0;
                        OnScoreReset(this, RetryCount);
                    }
            }
            catch
            {
            }
        }

        private void Lm_OnHealthPointChanged(double hp)
        {
            HP = hp;
            var NoFail = !CanFail;
            var IsPlaying = GameStatus.CurrentStatus == OsuGameStatus.Playing &&
                            GameStatus.LastStatus != OsuGameStatus.Playing;
            var ModsAreValid = Mods.Count == 0 ? false : !ModShortNames.Contains("Unknown");
            if (hp <= 0 && Score > 0 && CanFail && IsPlaying && ModsAreValid) OnFail(this);
            if (hp <= 0 && Score > 0 && NoFail && IsPlaying)
                Task.Run(() =>
                {
                    if (!noFailTriggered)
                    {
                        OnNoFail(this);
                        noFailTriggered = true;
                        Thread.Sleep(3000);
                        noFailTriggered = false;
                    }
                });
        }

        private void Lm_OnAccuracyChanged(double acc)
        {
            this.acc = acc;
        }
    }
}