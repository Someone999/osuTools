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
using osuTools.Beatmaps.Classes.HitObject;
using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using Sync.Tools;
using Beatmap = OsuRTDataProvider.BeatmapInfo.Beatmap;

namespace osuTools.ORTDP
{
    partial class OrtdpWrapper
    {
        private int _allhit;
        private OsuBeatmap _osuBeatmap;
        private BreakTimeCollection _breaktimes = new BreakTimeCollection();
        private int _cBtime;
        private int _cTmpoint;
        private MD5String _md5Str = new MD5String(), _md5Str1 = new MD5String();
        private bool _noFailTriggered;
        private TimePointCollection _timepoints = new TimePointCollection();
        private int _tmpHitObjectCount;
        private double _tmpTime;

        /// <summary>
        ///     OsuRTDataProvider提供的Beatmap
        /// </summary>
        public Beatmap OrtdpBeatmap { get; private set; }

        /// <summary>
        ///     当前BreakTime剩余的时间
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "LANG_VAR_REMAINBREAKTIME")]
        public TimeSpan RemainingBreakTime { get; private set; }

        /// <summary>
        ///     BreakTime的数量
        /// </summary>
        [AvailableVariable("BreakTimeCount", "LANG_VAR_CBREAKTIME")]
        public int BreakTimeCount => Beatmap?.BreakTimes.Count ?? 0;

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
        public double CurrentBpm { get; private set; }

        /// <summary>
        ///     CurrentBPM保留两位小数
        /// </summary>
        [AvailableVariable("CurrentBPMStr", "LANG_VAR_CURRENTBPM_STR")]
        public string CurrentBpmStr => CurrentBpm.ToString("f2");

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
                        _allhit = CountGeki + Count300 + CountKatu + Count100 + Count50 + CountMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = CountGeki + Count300 + CountKatu + Count100 + Count50 + CountMiss - _allhit;
                    }

                    if (GameMode.CurrentMode == OsuGameMode.Osu)
                    {
                        _allhit = Count300 + Count100 + Count50 + CountMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = Count300 + Count100 + Count50 + CountMiss - _allhit;
                    }

                    if (GameMode.CurrentMode == OsuGameMode.Catch)
                    {
                        _allhit = Count300 + Count50 + CountMiss;
                        Thread.Sleep(1000);
                        JudgementPerSecond = Count300 + Count50 + CountMiss - _allhit;
                    }
                }
            }));
        }

        private void InitLisenter()
        {
            GameMode = new GmMode(OsuPlayMode.Unknown, OsuPlayMode.Unknown);
            GameStatus = new GMStatus(OsuListenerManager.OsuStatus.Unkonwn, OsuListenerManager.OsuStatus.Unkonwn);
            HitChanged();
            _p = new OsuRTDataProviderPlugin();
            _p.OnEnable();
            _listenerManager = _p.ListenerManager;
            _listenerManager.OnCountGekiChanged += ListenerManagerOnCountGekiChanged;
            _listenerManager.OnCount300Changed += ListenerManagerOnCount300Changed;
            _listenerManager.OnCountKatuChanged += ListenerManagerOnCountKatuChanged;
            _listenerManager.OnCount100Changed += ListenerManagerOnCount100Changed;
            _listenerManager.OnCount50Changed += ListenerManagerOnCount50Changed;
            _listenerManager.OnCountMissChanged += ListenerManagerOnCountMissChanged;
            _listenerManager.OnComboChanged += ListenerManagerOnComboChanged;
            _listenerManager.OnScoreChanged += ListenerManagerOnScoreChanged;
            _listenerManager.OnPlayingTimeChanged += ListenerManagerOnPlayingTimeChanged;
            _listenerManager.OnHealthPointChanged += ListenerManagerOnHealthPointChanged;
            _listenerManager.OnAccuracyChanged += ListenerManagerOnAccuracyChanged;
            _listenerManager.OnBeatmapChanged += ListenerManagerOnBeatmapChanged;
            _listenerManager.OnModsChanged += ListenerManagerOnModsChanged;
            _listenerManager.OnPlayModeChanged += ListenerManagerOnPlayModeChanged;
            _listenerManager.OnStatusChanged += ListenerManagerOnStatusChanged;
            _listenerManager.OnPlayerChanged += ListenerManagerOnPlayerChanged;
        }

        private void ListenerManagerOnPlayerChanged(string player)
        {
            PlayerName = player.Trim();
        }

        private IHasPerformanceCalculator _performanceCalculator;
        private void InitLisenter(OsuRTDataProviderPlugin pl)
        {
            GameMode = new GmMode(OsuPlayMode.Unknown, OsuPlayMode.Unknown);
            GameStatus = new GMStatus(OsuListenerManager.OsuStatus.Unkonwn, OsuListenerManager.OsuStatus.Unkonwn);
            HitChanged();
            _p = pl;
            _listenerManager = _p.ListenerManager;
            _listenerManager.OnCountGekiChanged += ListenerManagerOnCountGekiChanged;
            _listenerManager.OnCount300Changed += ListenerManagerOnCount300Changed;
            _listenerManager.OnCountKatuChanged += ListenerManagerOnCountKatuChanged;
            _listenerManager.OnCount100Changed += ListenerManagerOnCount100Changed;
            _listenerManager.OnCount50Changed += ListenerManagerOnCount50Changed;
            _listenerManager.OnCountMissChanged += ListenerManagerOnCountMissChanged;
            _listenerManager.OnComboChanged += ListenerManagerOnComboChanged;
            _listenerManager.OnScoreChanged += ListenerManagerOnScoreChanged;
            _listenerManager.OnPlayingTimeChanged += ListenerManagerOnPlayingTimeChanged;
            _listenerManager.OnHealthPointChanged += ListenerManagerOnHealthPointChanged;
            _listenerManager.OnAccuracyChanged += ListenerManagerOnAccuracyChanged;
            _listenerManager.OnBeatmapChanged += ListenerManagerOnBeatmapChanged;
            _listenerManager.OnModsChanged += ListenerManagerOnModsChanged;
            _listenerManager.OnPlayModeChanged += ListenerManagerOnPlayModeChanged;
            _listenerManager.OnStatusChanged += ListenerManagerOnStatusChanged;
            _listenerManager.OnHitEventsChanged += ListenerManagerOnHitEventsChanged;
        }

        private void ListenerManagerOnHitEventsChanged(PlayType playType, List<HitEvent> hitEvents)
        {
            //IO.CurrentIO.Write(hitEvents.Last().TimeStamp.ToString());
        }

        private void ListenerManagerOnPlayModeChanged(OsuPlayMode last, OsuPlayMode mode)
        {
            GameMode = new GmMode(last, mode);
            CanFail = !Mods.Any(innerMod =>
                innerMod is NoFailMod || innerMod is AutoPilotMod || innerMod is RelaxMod);
            if (CurrentMode is IHasPerformanceCalculator has)
            {
                _performanceCalculator = has;
                PreCalculatedPp = has.GetMaxPerformance(this);
            }

            if (CurrentMode is ILegacyMode legacyMode)
            {
                var val = _osuBeatmap?.ModStarPair.GetStars(legacyMode.LegacyMode, Mods.ToIntMod());
                if(Beatmap.Mode  == OsuGameMode.Osu)
                    Beatmap.Stars = val.GetValueOrDefault();
            }
        }

        private void ListenerManagerOnModsChanged(ModsInfo mods)
        {
            //MessageBox.Show(((int)mods.Mod).ToString());
            Mods.ClearMod();
            foreach (var mod in ModList.FromInteger((int) mods.Mod).Mods.Where(m => m.CheckAndSetForMode(CurrentMode)))
            {
                Mods.Add(mod);
            }
            if (CurrentMode == OsuGameMode.Mania && Mods.Any(mod => mod is ScoreV2Mod))
                _tmpHitObjectCount = _hitObjects.Count +
                                     _hitObjects.Count(h => h.HitObjectType == HitObjectTypes.ManiaHold);
            else _tmpHitObjectCount = _hitObjects.Count;
            if (DebugMode)
                if (_tmpHitObjectCount != _hitObjects.Count)
                    IO.CurrentIO.Write($"[osuTools] HitObject Count after applied Mods: {_tmpHitObjectCount}.");
        }

        private void ListenerManagerOnStatusChanged(OsuListenerManager.OsuStatus lastStatus, OsuListenerManager.OsuStatus status)
        {
            GameStatus = new GMStatus(lastStatus, status);
            if (status == OsuListenerManager.OsuStatus.SelectSong || status == OsuListenerManager.OsuStatus.Rank)
                RetryCount = 0;
            while (status != OsuListenerManager.OsuStatus.Rank && status != OsuListenerManager.OsuStatus.Playing)
            {
                if (Count300 != 0 || CountGeki != 0 || CountKatu != 0 || Count100 != 0 || Count50 != 0 || CountMiss != 0 || Score != 0 ||
                    _acc != 0)
                    Setzero();
                else
                    break;
                if (!(Beatmap is null))
                {
                    if (!string.IsNullOrEmpty(NowPlaying))
                        NowPlaying = $"{Beatmap.Artist} - {Beatmap.Title} [{Beatmap.Difficulty}]";
                }
            }
        }

        private void Setzero()
        {
            Count300 = 0;
            CountGeki = 0;
            CountKatu = 0;
            Count100 = 0;
            Count50 = 0;
            CountMiss = 0;
            Score = 0;
            _acc = 0;
            _cBtime = 0;
            _cTmpoint = 0;
            _maxcb = 0;
        }

        private void ReadFromOrtdp(Beatmap beatmap)
        {
            NowPlaying = "Loading...";
            _bStatus = OsuBeatmapStatus.Loading;
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
                _hitObjects = Beatmap.HitObjects;
                _bStatus = OsuBeatmapStatus.Unknown;
                _dur = _drainTime = _hitObjects.LastOrDefault() == null
                    ? TimeSpan.FromMilliseconds(0)
                    : TimeSpan.FromMilliseconds(_hitObjects.LastOrDefault().GetEndTime());
                Beatmap.Stars = _rtppi.BeatmapTuple.Stars;
                if (DebugMode) IO.CurrentIO.Write("Beatmap read from ORTDP.", true, false);
            }
        }

        private void ReadFromOsudb(Beatmap map)
        {
            NowPlaying = "Loading...";
            _bStatus = OsuBeatmapStatus.Loading;
            if (_md5Str is null || _md5Str1 is null) return;
            _md5Str = new MD5String("");
            if (_md5Str != _md5Str1)
            {
                _beatmapDb = new OsuBeatmapDB();
                _md5Str1 = _md5Str;
            }

            try
            {
                if (map != null)
                {
                    var md5 = MD5String.GetString(
                        new MD5CryptoServiceProvider().ComputeHash(File.ReadAllBytes(map.FilenameFull)));
                    var beatmapa = _beatmapDb.Beatmaps.FindByMd5(md5);
                    if (!(beatmapa is null))
                    {
                        Beatmap = new Beatmaps.Beatmap(beatmapa);
                        _osuBeatmap = beatmapa;
                        NowPlaying = Beatmap.ToString();
                        _hitObjects = Beatmap.HitObjects;
                        var lastHitObjectEndTime = TimeSpan.FromMilliseconds(_hitObjects.LastOrDefault().GetEndTime());
                        _bStatus = beatmapa.BeatmapStatus;
                        var totalTime = beatmapa.TotalTime;
                        _dur = totalTime;
                        _drainTime = lastHitObjectEndTime;
                        if (CurrentMode is ILegacyMode le)
                        {
                            if(_osuBeatmap.Mode == OsuGameMode.Osu)
                                Beatmap.Stars = _osuBeatmap.ModStarPair.GetStars(le.LegacyMode, Mods.ToIntMod());
                        }

                        if (DebugMode) IO.CurrentIO.Write("Beatmap read from OsuDb.", true, false);
                    }
                    else
                    {
                        IO.CurrentIO.WriteColor("Fail to get beatmap info.", ConsoleColor.Red, true, false);
                    }
                }
            }
            catch (BeatmapNotFoundException)
            {
                var m5 = _beatmapDb.MD5;
                _beatmapDb = new OsuBeatmapDB();
                var m51 = _beatmapDb.MD5;
                if (m5 == m51)
                    ReadFromOrtdp(map);
                //IO.CurrentIO.WriteColor("Can not find this beatmap by MD5 in osu!DataBase.Beatmap has read from osu file,Info may be correct after re-read OsuDataBase.", ConsoleColor.Red);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private int GetDifficultyMul(double val)
        {
            if (val < 0) throw new ArgumentOutOfRangeException(nameof(val),"Difficulty must greater than or equals to 0.");
            if (val >= 0 && val <= 5) return 2;
            if (val >= 6 && val <= 12) return 3;
            if (val >= 13 && val <= 17) return 4;
            if (val >= 18 && val <= 24) return 5;
            if (val >= 25 && val <= 30) return 6;
            if (val > 30) return 6;
            return -1;
        }

        private void ListenerManagerOnBeatmapChanged(Beatmap map)
        {
            try
            {
                OrtdpBeatmap = map;
                Beatmap = new Beatmaps.Beatmap();
                if (DebugMode)
                    IO.CurrentIO.Write("[osuTools] Beatmap Changed");
                if (BeatmapReadMethod == BeatmapReadMethods.Ortdp) ReadFromOrtdp(map);
                if (BeatmapReadMethod == BeatmapReadMethods.OsuDb) ReadFromOsudb(map);
                PreCalculatedPp = 0;
                if (CurrentMode is IHasPerformanceCalculator m)
                {
                    //m.SetBeatmap(Beatmap);
                    if (Beatmap.Mode != OsuGameMode.Osu)
                    {
                        if (Game.Modes.GameMode.FromLegacyMode(Beatmap.Mode) is IHasPerformanceCalculator bm)
                        {
                            bm.SetBeatmap(Beatmap);
                            PreCalculatedPp = bm.GetMaxPerformance(this);

                        }
                    }
                    else
                    {
                        PreCalculatedPp = m.GetMaxPerformance(this);
                    }
                }

                _timepoints.TimePoints = Beatmap.TimePoints.TimePoints.Where(tp => tp.Uninherited).ToList();
                _breaktimes = Beatmap.BreakTimes;
                NowPlaying = Beatmap.ToString();
                var osuMode = CurrentMode == OsuGameMode.Osu && Beatmap.Mode == OsuGameMode.Osu;
                var ctbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Catch;
                var compatibleCtbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Osu;
                if (osuMode || ctbMode || compatibleCtbMode)
                    DifficultyMultiplier =
                        GetDifficultyMul(Beatmap.CircleSize + Beatmap.OverallDifficulty + Beatmap.HpDrain);
                Task.Run(() =>
                {
                    var bassPlayer = Beatmap?.LoadNewMusicPlayer();
                    _dur = bassPlayer?.Duration ?? _dur;
                    bassPlayer?.Dispose();
                });

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
                        builder.AppendLine($"[Beatmap BreakTime Detector] BreakTime:{_breaktimes.Count}");
                        builder.AppendLine(
                            $"[Beatmap Uninherited TimePoint Detector] Uninherited TimePoint:{_timepoints.Count}");
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

        private void ListenerManagerOnCountGekiChanged(int hit)
        {
            CountGeki = hit;
        }

        private void ListenerManagerOnCount300Changed(int hit)
        {
            Count300 = hit;
        }

        private void ListenerManagerOnCountKatuChanged(int hit)
        {
            CountKatu = hit;
        }

        private void ListenerManagerOnCount100Changed(int hit)
        {
            Count100 = hit;
        }

        private void ListenerManagerOnCount50Changed(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    RetryCount++;
            Count50 = hit;
        }

        private void ListenerManagerOnCountMissChanged(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("SD"))
                    OnFail(this);
            CountMiss = hit;
        }

        private void ListenerManagerOnComboChanged(int combo)
        {
            Combo = combo;
        }

        void ProcessTimePoint(int ms)
        {
            if (_timepoints.Count > 0)
            {
                if (ms < _timepoints[0].Offset)
                {
                    _cTmpoint = 0;
                    CurrentBpm = _timepoints[_cTmpoint].BPM;
                }

                if (_cTmpoint < _timepoints.Count)
                {
                    CurrentBpm = _timepoints[_cTmpoint].BPM;
                    foreach (var m in Mods)
                        if (m is IChangeTimeRateMod changeTimeRateMod)
                            CurrentBpm *= changeTimeRateMod.TimeRate;
                    if (ms > _timepoints[_cTmpoint + 1 < _timepoints.Count - 1 ? _cTmpoint + 1 : _cTmpoint].Offset)
                        _cTmpoint = _cTmpoint == _timepoints.Count - 1 ? _cTmpoint : _cTmpoint + 1;
                }
            }
        }

        void ProcessBreakTime(int ms)
        {
            try
            {
                var curBreaktime = _breaktimes[_cBtime > _breaktimes.Count - 1 ? _breaktimes.Count : _cBtime];
                if (_breaktimes.Count > 0)
                {
                    if (ms < _breaktimes[0].Start)
                    {
                        _cBtime = 0;
                        curBreaktime = _breaktimes[0];
                        TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                    }

                    if (ms > _breaktimes.BreakTimes.Last().End)
                    {
                        RemainingBreakTime = TimeSpan.Zero;
                        TimeToNextBreakTime = TimeSpan.Zero;
                    }
                    else
                    {
                        if (ms > _breaktimes[_cBtime > _breaktimes.Count - 1 ? _breaktimes.Count - 1 : _cBtime]
                            .End)
                        {
                            if (_cBtime < _breaktimes.Count)
                            {
                                TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                _cBtime = _cBtime == _breaktimes.Count - 1 ? _cBtime : _cBtime + 1;
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
                _cBtime = _breaktimes.Count - 1;
                RemainingBreakTime = TimeSpan.Zero;
                TimeToNextBreakTime = TimeSpan.Zero;
            }
        }
        private void ListenerManagerOnPlayingTimeChanged(int ms)
        {
            if (CurrentStatus == OsuGameStatus.Playing)
                CalcHitObjectPercent();
            if (_osuBeatmap != null)
            {
                _tmper = ms / SongDuration.TotalMilliseconds;
                if (CurrentStatus == OsuGameStatus.Playing)
                {
                    
                    ProcessTimePoint(ms);
                    if (_breaktimes.Count > 0)
                       ProcessBreakTime(ms);
                    else
                        RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                }
                else
                {
                    RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                    if (_timepoints.Count > 0) CurrentBpm = _timepoints[0].BPM;
                }
            }

            if (_tmper > 1) _tmper = 1;
            if (CurrentStatus != OsuGameStatus.Playing) _tmper = 0;
            if (CurrentStatus == OsuGameStatus.Rank && LastStatus == OsuGameStatus.Playing) _tmper = 1;
            PlayTime = ms;
            _cur = TimeSpan.FromMilliseconds(ms);
            if ((_cur - _dur).TotalSeconds > 0 &&(_cur - _dur).TotalSeconds <= 1 )
                _dur = _cur;
            if (ms <= 0)
            {
                IHitObject hitObject = _hitObjects.Last();
                int time = 0;
                if (hitObject != null)
                {
                    if (hitObject is IHasEndHitObject hasEnd)
                        time = hasEnd.EndTime;
                    else
                        time = hitObject.Offset;
                }

                _dur = TimeSpan.FromMilliseconds(time);
            }

        }

        private void ListenerManagerOnScoreChanged(int obj)
        {
            try
            {
                double current = obj - Score;
                double retryflag = 0;
                Score = obj;
                var scoreZeroed = obj == 0;
                if (CurrentStatus == OsuGameStatus.SelectSong)
                    retryflag++;
                if (scoreZeroed && retryflag >= 0 && PlayerMaxCombo != 0)
                    if (CurrentStatus != OsuGameStatus.Rank)
                    {
                        if (InDebugMode())
                            IO.CurrentIO.WriteColor($"[osuTools] Retry at {PlayTime}ms", ConsoleColor.Yellow);
                        RetryCount++;
                        PlayTime = 0;
                        OnScoreReset(this, RetryCount);
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ListenerManagerOnHealthPointChanged(double hp)
        {
            Hp = hp;
            var noFail = !CanFail;
            var isPlaying = GameStatus.CurrentStatus == OsuGameStatus.Playing &&
                            GameStatus.LastStatus != OsuGameStatus.Playing;
            var modsAreValid = Mods.Count != 0 && !ModShortNames.Contains("Unknown");
            if (hp <= 0 && Score > 0 && CanFail && isPlaying && modsAreValid) OnFail(this);
            if (hp <= 0 && Score > 0 && noFail && isPlaying)
                Task.Run(() =>
                {
                    if (!_noFailTriggered)
                    {
                        OnNoFail(this);
                        _noFailTriggered = true;
                        Thread.Sleep(3000);
                        _noFailTriggered = false;
                    }
                });
        }

        private void ListenerManagerOnAccuracyChanged(double acc)
        {
            this._acc = acc;
        }
    }
}