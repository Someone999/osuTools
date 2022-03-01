using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OsuRTDataProvider;
using OsuRTDataProvider.BeatmapInfo;
using OsuRTDataProvider.Listen;
using OsuRTDataProvider.Mods;
using osuTools.Attributes;
using osuTools.Beatmaps.BreakTime;
using osuTools.Beatmaps.HitObject;
using osuTools.Beatmaps.TimingPoint;
using osuTools.Exceptions;
using osuTools.Game;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.MD5Tools;
using osuTools.OsuDB;
using osuTools.OsuDB.Beatmap;
using Sync.Tools;
using osuTools.MemoryCache.Beatmap;

namespace osuTools.OrtdpWrapper
{
    partial class OrtdpWrapper
    {
        private int _allhit;
        private OsuBeatmap _osuBeatmap;
        private BreakTimeCollection _breaktimes = new BreakTimeCollection();
        private int _cTmpoint;
        private MD5String _md5Str = new MD5String(), _md5Str1 = new MD5String();
        private bool _noFailTriggered;
        private TimingPointCollection _timepoints = new TimingPointCollection();
        private double _tmpTime;
        private MemoryBeatmapCollection _beatmapCollection = new MemoryBeatmapCollection();

        /// <summary>
        ///     OsuRTDataProvider提供的Beatmap
        /// </summary>
        public Beatmap OrtdpBeatmap { get; private set; }
        /// <summary>
        ///     当前BreakTime剩余的时间
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "LANG_VAR_REMAINBREAKTIME")]
        public TimeSpan RemainingBreakTime 
        {
            get
            {
                if (PlayTime == 0)
                {
                    _currentBreakTimeIdx = -1;
                }
                if(_breaktimes.Count == 0)
                {
                    return TimeSpan.Zero;
                }
                if (PlayTime > _breaktimes[0].Start && _currentBreakTimeIdx == -1)
                {
                    Interlocked.Add(ref _currentBreakTimeIdx, 1);
                }
                if(_currentBreakTimeIdx == -1)
                {
                    return TimeSpan.Zero;
                }
                                          
                var breakTimes = Beatmap.BreakTimes;
                if (PlayTime > breakTimes[_currentBreakTimeIdx].Start)
                {                 
                    if (breakTimes[_currentBreakTimeIdx].InBreakTime((long)PlayTime))
                    {
                        return TimeSpan.FromMilliseconds(breakTimes[_currentBreakTimeIdx].End - PlayTime);
                    }
                }
                
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        ///     BreakTime的数量
        /// </summary>
        [AvailableVariable("BreakTimeCount", "LANG_VAR_CBREAKTIME")]
        public int BreakTimeCount => Beatmap?.BreakTimes.Count ?? 0;
        int _currentBreakTimeIdx = -1;
        /// <summary>
        ///     距离下一个BreakTime的时间
        /// </summary>
        [AvailableVariable("TimeToNextBreakTime", "LANG_VAR_TIMETONEXTBREAKTIME")]
        public TimeSpan TimeToNextBreakTime 
        {
            get
            {
                
                if(_breaktimes.Count == 0)
                {
                    return TimeSpan.Zero;
                }
                if(PlayTime == 0) 
                {
                    _currentBreakTimeIdx = -1;
                }
                var breakTimes = Beatmap.BreakTimes;

                if (_currentBreakTimeIdx == -1)
                {
                    return TimeSpan.FromMilliseconds(breakTimes[0].Start - PlayTime);
                }            
                if (_currentBreakTimeIdx < BreakTimeCount - 1)
                {
                    if (PlayTime > breakTimes[_currentBreakTimeIdx < 0 ? 0 : _currentBreakTimeIdx].End) 
                    {
                        _currentBreakTimeIdx++;
                    }
                    if (!breakTimes[_currentBreakTimeIdx].InBreakTime((long)PlayTime))
                    {
                        return TimeSpan.FromMilliseconds(breakTimes[_currentBreakTimeIdx].Start - PlayTime);
                    }
                }
                if (_currentBreakTimeIdx == BreakTimeCount - 1)
                {
                    if (PlayTime > _breaktimes.BreakTimes.Last().Start)
                    {
                        return TimeSpan.Zero;
                    }
                    return TimeSpan.FromMilliseconds(breakTimes[_currentBreakTimeIdx].Start - PlayTime);
                }
                return TimeSpan.Zero;
            }
        }

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
            Task.Run(() =>
            {
                Thread.Sleep(1);
                while (CurrentStatus == OsuGameStatus.Playing)
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
            });
        }

        private void InitListener()
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
            if (!string.IsNullOrEmpty(player))
            {
                PlayerName = player.Trim();
            }
        }

        private void InitListener(OsuRTDataProviderPlugin pl)
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
            _listenerManager.OnPlayerChanged += ListenerManagerOnPlayerChanged;
        }

        private void ListenerManagerOnHitEventsChanged(PlayType playType, List<HitEvent> hitEvents)
        {
            //IO.CurrentIO.Write(hitEvents.Last().TimeStamp.ToString());
        }

        private void ListenerManagerOnPlayModeChanged(OsuPlayMode last, OsuPlayMode mode)
        {
            GameMode.CurrentMode = Game.Modes.GameMode.FromLegacyMode((OsuGameMode)mode);
            GameMode.LastMode = Game.Modes.GameMode.FromLegacyMode((OsuGameMode)last);
            if (Beatmap.Mode == OsuGameMode.Osu)
                if (CurrentMode is IHasPerformanceCalculator has && CurrentStatus == OsuGameStatus.SelectSong)
                {
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
            Mods.ClearMod();
            foreach (var mod in ModList.FromInteger((int) mods.Mod).Mods.Where(m => m.CheckAndSetForMode(CurrentMode)))
            {
                OsuGameMode? mode = null;
                if (CurrentMode is ILegacyMode legacyMode)
                    mode = legacyMode.LegacyMode;
                Mods.Add(mod,mode);
            }
            
            CanFail = Mods.AllowsFail;
        }

        private void ListenerManagerOnStatusChanged(OsuListenerManager.OsuStatus lastStatus, OsuListenerManager.OsuStatus status)
        {
            GameStatus.CurrentStatus = (OsuGameStatus) status;
            GameStatus.LastStatus = (OsuGameStatus)lastStatus;
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
            _cTmpoint = 0;
            _maxcb = 0;
            _lastAcc = 0;
            _lastC300GRate = 0;
            _lastC300Rate = 0;
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

        private void ReadFromOsudb(Beatmap beatmap)
        {
            NowPlaying = "Loading...";
            _bStatus = OsuBeatmapStatus.Loading;
            if (_md5Str is null || _md5Str1 is null) return;
            _md5Str = new MD5String("");
            if (_md5Str != _md5Str1)
            {
                _beatmapDb = new OsuBeatmapDb();
                _md5Str1 = _md5Str;
            }

            try
            {
                if (beatmap != null)
                {
                    var md5 = MD5String.GetString(
                        new MD5CryptoServiceProvider().ComputeHash(File.ReadAllBytes(beatmap.FilenameFull)));
                    var osuBeatmap = _beatmapDb.Beatmaps.FindByMd5(md5);
                    if (!(osuBeatmap is null))
                    {
                        Beatmap = new Beatmaps.Beatmap(osuBeatmap);
                        _osuBeatmap = osuBeatmap;
                        NowPlaying = Beatmap.ToString();
                        _hitObjects = Beatmap.HitObjects;
                        var lastHitObjectEndTime = TimeSpan.FromMilliseconds(_hitObjects.LastOrDefault().GetEndTime());
                        _bStatus = osuBeatmap.BeatmapStatus;
                        var totalTime = osuBeatmap.TotalTime;
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
                var m5 = _beatmapDb.Md5;
                _beatmapDb = new OsuBeatmapDb();
                var m51 = _beatmapDb.Md5;
                if (m5 == m51)
                    ReadFromOrtdp(beatmap);
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
        MD5CryptoServiceProvider _globalMd5Calculator = new MD5CryptoServiceProvider();
        private void ListenerManagerOnBeatmapChanged(Beatmap map)
        {
            try
            {
                _listenerManager.OnPlayingTimeChanged -= ListenerManagerOnPlayingTimeChanged;
                OrtdpBeatmap = map;
                var hash = _globalMd5Calculator.ComputeHash(File.ReadAllBytes(map.FilenameFull));
                var currentMd5 = MD5String.GetString(hash);
                if (!_beatmapCollection.ContainsBeatmap(currentMd5))
                {
                    
                    Beatmap = new Beatmaps.Beatmap();
                    lock (Beatmap)
                    {
                        if (DebugMode)
                            IO.CurrentIO.Write("[osuTools] Beatmap Changed");
                        if (BeatmapReadMethod == BeatmapReadMethods.Ortdp) ReadFromOrtdp(map);
                        if (BeatmapReadMethod == BeatmapReadMethods.OsuDb) ReadFromOsudb(map);
                        CacheBeatmap cacheBeatmap = new CacheBeatmap(Beatmap, _dur, _drainTime, _bStatus);
                        _beatmapCollection.Add(currentMd5, cacheBeatmap);
                    }
                }
                else
                {
                    CacheBeatmap currentCacheBeatmap = _beatmapCollection[currentMd5];
                    Beatmap = currentCacheBeatmap.CurrentBeatmap;
                    NowPlaying = Beatmap.ToString();
                    _hitObjects = Beatmap.HitObjects;
                    _dur = currentCacheBeatmap.SongDuration;
                    _drainTime = currentCacheBeatmap.DrainTime;
                    _bStatus = currentCacheBeatmap.Status;
                }
                PreCalculatedPp = 0;
                if (CurrentMode is IHasPerformanceCalculator m)
                {
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

                _timepoints.TimePoints = Beatmap.TimingPoints.TimePoints.Where(tp => tp.Uninherited).ToList();
                _breaktimes = Beatmap.BreakTimes;
                NowPlaying = Beatmap.ToString();
                var osuMode = CurrentMode == OsuGameMode.Osu && Beatmap.Mode == OsuGameMode.Osu;
                var ctbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Catch;
                var compatibleCtbMode = CurrentMode == OsuGameMode.Catch && Beatmap.Mode == OsuGameMode.Osu;
                if (osuMode || ctbMode || compatibleCtbMode)
                    DifficultyMultiplier =
                        GetDifficultyMul(Beatmap.CircleSize + Beatmap.OverallDifficulty + Beatmap.HpDrain);
                if (DebugMode)
                {
                    if (!(Beatmap is null))
                    {
                        var hit = Beatmap.HitObjects;
                        var tm = Beatmap.TimingPoints;
                        var uinh = Beatmap.TimingPoints.TimePoints.Where(t => t.Uninherited).ToArray();
                        var inh = Beatmap.TimingPoints.TimePoints.Where(t => !t.Uninherited).ToArray();
                        var btm = Beatmap.BreakTimes;
                        var builder = new StringBuilder();
                        builder.AppendLine($"Beatmap: {Beatmap}\nHitObject Count: {hit.Count}");
                        builder.AppendLine($"Mode:{Beatmap.Mode}");
                        builder.AppendLine($"BreakTime: {btm.Count}");
                        builder.AppendLine(
                            $"TimingPoints: {tm.Count} Inherited TimingPoint Count: {inh.Length} UnInherited TimingPoint Count: {uinh.Length}");
                        builder.AppendLine($"[Beatmap BreakTime Detector] BreakTime:{_breaktimes.Count}");
                        builder.AppendLine(
                            $"[Beatmap Uninherited TimingPoint Detector] Uninherited TimingPoint:{_timepoints.Count}");
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
            _listenerManager.OnPlayingTimeChanged += ListenerManagerOnPlayingTimeChanged;
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
            if (Mods.Count > 0)
                if (Mods.HasMod(typeof(PerfectMod)))
                    RetryCount++;
            Count50 = hit;
        }

        private void ListenerManagerOnCountMissChanged(int hit)
        {
            if (Mods.Count > 0)
                if (Mods.Contains(new SuddenDeathMod()))
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
                    CurrentBpm = _timepoints[_cTmpoint].Bpm;
                }

                if (_cTmpoint < _timepoints.Count)
                {
                    CurrentBpm = _timepoints[_cTmpoint].Bpm;
                    foreach (var m in Mods)
                        if (m is IChangeTimeRateMod changeTimeRateMod)
                            CurrentBpm *= changeTimeRateMod.TimeRate;
                    if (ms > _timepoints[_cTmpoint + 1 < _timepoints.Count - 1 ? _cTmpoint + 1 : _cTmpoint].Offset)
                        _cTmpoint = _cTmpoint == _timepoints.Count - 1 ? _cTmpoint : _cTmpoint + 1;
                }
            }
        }
        /*void ProcessBreakTime(double ms)
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

                        if (curBreaktime.InBreakTime((long)ms))
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
        }*/
        private void ListenerManagerOnPlayingTimeChanged(int ms)
        {
            if (CurrentStatus == OsuGameStatus.Playing)
            {
                CalcHitObjectPercent();
                ProcessTimePoint(ms);
            }

            if (_osuBeatmap != null)
            {
                _tmper = ms / SongDuration.TotalMilliseconds;
                if (CurrentStatus == OsuGameStatus.Playing)
                {
                    
                    /*ProcessTimePoint(ms);
                    if (_breaktimes.Count > 0)
                        ProcessBreakTime(ms);
                    else
                        RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;*/
                }
                else
                {
                    //RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                    if (_timepoints.Count > 0) CurrentBpm = _timepoints[0].Bpm;
                }
            }

            if (_tmper > 1) _tmper = 1;
            if (CurrentStatus != OsuGameStatus.Playing) _tmper = 0;
            if (CurrentStatus == OsuGameStatus.Rank && LastStatus == OsuGameStatus.Playing) _tmper = 1;
            PlayTime = ms;
            _cur = TimeSpan.FromMilliseconds(ms);
            if (_cur > _dur && CurrentStatus == OsuGameStatus.Playing)
                _dur = _cur;
            if (ms <= 0)
            {
                IHitObject hitObject = _hitObjects.Last();
                int time = 0;
                if (hitObject != null)
                {
                    if (hitObject is IHasEndHitObject hasEnd)
                        time = (int)hasEnd.EndTime;
                    else
                        time = (int)hitObject.Offset;
                }

                _dur = TimeSpan.FromMilliseconds(time);
            }

        }

        private void ListenerManagerOnScoreChanged(int obj)
        {
            try
            {
                double retryFlag = 0;
                Score = obj;
                var scoreZeroed = obj == 0;
                if (CurrentStatus == OsuGameStatus.SelectSong)
                    retryFlag++;
                if (scoreZeroed && retryFlag >= 0 && PlayerMaxCombo != 0)
                    if (CurrentStatus != OsuGameStatus.Rank)
                    {
                        if (InDebugMode())
                            IO.CurrentIO.WriteColor($"[osuTools] Retry at {PlayTime}ms", ConsoleColor.Yellow);
                        RetryCount++;
                        PlayTime = 0;
                        Setzero();
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
            var modsAreValid = Mods.Count != 0;
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
            _acc = acc;
        }
    }
}