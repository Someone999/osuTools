using System;
using System.Linq;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.Game.Mods;
using osuTools.ORTDP;
using osuTools.PerformanceCalculator.Catch;
using RealTimePPDisplayer.Beatmap;
using RealTimePPDisplayer.Calculator;
using RealTimePPDisplayer.Displayer;
using Sync.Tools;

namespace osuTools.Game.Modes
{
    /// <summary>
    /// Catch模式
    /// </summary>
    public class CatchMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private CatchTheBeatPerformanceCalculator _calculator;
        ///<inheritdoc/>
        public override string ModeName => "Catch";
        ///<inheritdoc/>
        public override Mod[] AvaliableMods => Mod.CatchMods;
        ///<inheritdoc/>
        public override string Description => "接水果";
        private CatchBeatmap _innerBeatmap;
        private int _maxCombo;
        ///<inheritdoc/>
        public void SetBeatmap(Beatmap b)
        {
            _innerBeatmap = new CatchBeatmap(b);
            _performanceCalculator = null;
            _maxCombo = _innerBeatmap.MaxCombo;
        }
        ///<inheritdoc/>
        public double GetMaxPerformance(ORTDP.OrtdpWrapper wrapper)
        {
            _innerBeatmap = new CatchBeatmap(wrapper.Beatmap);
            _performanceCalculator =
                _performanceCalculator ?? new CatchPerformanceCalculator(_innerBeatmap, wrapper.Mods);
            return _performanceCalculator.CalculatePerformance(1, _innerBeatmap.MaxCombo, 0);//GetPPTuple(ortdpInfo).MaxPP;
        }
        ///<inheritdoc/>
        public PPTuple GetPPTuple(ORTDP.OrtdpWrapper ortdpInfo)
        {
            try
            {
                _calculator = _calculator ?? new CatchTheBeatPerformanceCalculator();
                _calculator.Beatmap = new BeatmapReader(ortdpInfo.OrtdpBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Catch] Current ORTDP Beatmap:{_calculator.Beatmap.OrtdpBeatmap.Artist} - {_calculator.Beatmap.OrtdpBeatmap.Title} [{_calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
                        true, false);
                _calculator.ClearCache();
                _calculator.Count50 = 0;
                _calculator.CountGeki = ortdpInfo.Beatmap.HitObjects.Count;
                _calculator.CountKatu = 0;
                _calculator.Count100 = 0;
                _calculator.CountMiss = 0;
                _calculator.Mods = (uint) ortdpInfo.Mods.ToIntMod();
                _calculator.MaxCombo = ortdpInfo.Beatmap.HitObjects.Count;
                _calculator.Count300 = 0;
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Catch] Calc Completed", true, false);
                return _calculator.GetPerformance();
            }
            catch (Exception ex)
            {
                IO.CurrentIO.Write("Error when PreCalc PP.");
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write($"[osuTools::PrePPCalc::Taiko] Exception:{ex.Message}");
                return new PPTuple
                {
                    FullComboAccuracyPP = -1,
                    FullComboAimPP = -1,
                    FullComboPP = -1,
                    FullComboSpeedPP = -1,
                    MaxAccuracyPP = -1,
                    MaxAimPP = -1,
                    MaxPP = -1,
                    MaxSpeedPP = -1,
                    RealTimeAccuracyPP = -1,
                    RealTimeAimPP = -1,
                    RealTimePP = -1,
                    RealTimeSpeedPP = -1
                };
            }
        }
        
        private CatchPerformanceCalculator _performanceCalculator;

        public double TestPerformanceCalculator(OrtdpWrapper wrapper)
        {
            _innerBeatmap = _innerBeatmap ?? new CatchBeatmap(wrapper.Beatmap);
            _performanceCalculator =
                _performanceCalculator ?? new CatchPerformanceCalculator(_innerBeatmap, wrapper.Mods);
            return _performanceCalculator.CalculatePerformance(wrapper.Accuracy, wrapper.MaxCombo, wrapper.CountMiss);
        }
        ///<inheritdoc/>
        public double GetPerformance(ORTDP.OrtdpWrapper ortdpInfo)
        {
            /*if (_innerBeatmap == null)
                SetBeatmap(ortdpInfo.Beatmap);
            if (_performanceCalculator == null)
                _performanceCalculator =
                    new CatchPerformanceCalculator(_innerBeatmap, ortdpInfo.Mods);
            return _performanceCalculator.CalculatePerformance(ortdpInfo.Accuracy, ortdpInfo.Combo, ortdpInfo.CountMiss);*/
            return GetPPTuple(ortdpInfo).RealTimePP;
        }
        ///<inheritdoc/>
        public OsuGameMode LegacyMode => OsuGameMode.Catch;
        ///<inheritdoc/>
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.Count300;
            double c200 = scoreInfo.CountKatu;
            double c50 = scoreInfo.Count50;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;
            var rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        ///<inheritdoc/>
        public override double AccuracyCalc(ORTDP.OrtdpWrapper scoreInfo)
        {
            if (scoreInfo is null) return 0;
            double c300 = scoreInfo.Count300;
            double c200 = scoreInfo.CountKatu;
            double c50 = scoreInfo.Count50;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;

            var rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        ///<inheritdoc/>
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b == null) return 0;
            var hitObjects = b.HitObjects;
            var juice = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.JuiceStream);
            var bananaShower = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.BananaShower);
            return hitObjects.Count + juice.Count() - bananaShower.Count();
        }
        ///<inheritdoc/>
        public override bool IsPerfect(ORTDP.OrtdpWrapper info)
        {
            return AccuracyCalc(info) >= 1;
        }
        ///<inheritdoc/>
        public override int GetPassedHitObjectCount(ORTDP.OrtdpWrapper i)
        {
            if (i is null) return 0;
            return i.Count300 + i.Count100;
        }
        ///<inheritdoc/>
        public override double GetCount300Rate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }
        ///<inheritdoc/>
        public override double GetCountGekiRate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }
        ///<inheritdoc/>
        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            if (types.Contains(HitObjectTypes.HitCircle))
                hitobject = new Fruit();
            if (types.Contains(HitObjectTypes.Slider))
                hitobject = new JuiceStream();
            if (types.Contains(HitObjectTypes.Spinner))
                hitobject = new BananaShower();
            if (hitobject == null) throw new IncorrectHitObjectException(this, types[0]);
            hitobject.Parse(data);
            return hitobject;
        }
        ///<inheritdoc/>
        public override GameRanking GetRanking(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            var isHdOrFl = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHdOrFl = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (Math.Abs(AccuracyCalc(info) * 100 - 100) == 0)
            {

                if (isHdOrFl) return GameRanking.SSH;
                return GameRanking.SS;
            }

            if (AccuracyCalc(info) * 100 > 98.01)
            {
                if (isHdOrFl) return GameRanking.SH;
                return GameRanking.S;
            }

            if (AccuracyCalc(info) * 100 > 94.01)
            {
                return GameRanking.A;
            }

            if (AccuracyCalc(info) * 100 > 90)
            {
                return GameRanking.B;
            }

            if (AccuracyCalc(info) * 100 > 85.01)
            {
                return GameRanking.C;
            }

            if (AccuracyCalc(info) * 100 < 85)
            {
                return GameRanking.D;
            }

            return GameRanking.Unknown;
        }
    }
}