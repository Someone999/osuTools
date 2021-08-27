using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Beatmaps.HitObject.Catch;
using osuTools.Exceptions;
using osuTools.Game.Mods;
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
        public double GetMaxPerformance(OrtdpWrapper.OrtdpWrapper wrapper)
        {
            _innerBeatmap = new CatchBeatmap(wrapper.Beatmap);
            _performanceCalculator =
                _performanceCalculator ?? new CatchPerformanceCalculator(_innerBeatmap, wrapper.Mods);
            return _performanceCalculator.CalculatePerformance(1, _innerBeatmap.MaxCombo, 0);//GetPPTuple(ortdpInfo).MaxPP;
        }
        ///<inheritdoc/>
        public PPTuple GetPPTuple(OrtdpWrapper.OrtdpWrapper ortdpInfo)
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

        internal double TestPerformanceCalculator(OrtdpWrapper.OrtdpWrapper wrapper)
        {
            _innerBeatmap = _innerBeatmap ?? new CatchBeatmap(wrapper.Beatmap);
            _performanceCalculator =
                _performanceCalculator ?? new CatchPerformanceCalculator(_innerBeatmap, wrapper.Mods);
            return _performanceCalculator.CalculatePerformance(wrapper.Accuracy, wrapper.MaxCombo, wrapper.CountMiss);
        }
        ///<inheritdoc/>
        public double GetPerformance(OrtdpWrapper.OrtdpWrapper ortdpInfo)
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
        public override double AccuracyCalc(OrtdpWrapper.OrtdpWrapper scoreInfo)
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
        public override int GetBeatmapHitObjectCount(Beatmap b, ModList mods)
        {
            if (b == null) return 0;
            var hitObjects = b.HitObjects;
            var juice = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.JuiceStream);
            var bananaShower = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.BananaShower);
            return hitObjects.Count + juice.Count() - bananaShower.Count();
        }
        ///<inheritdoc/>
        public override bool IsPerfect(OrtdpWrapper.OrtdpWrapper info)
        {
            return AccuracyCalc(info) >= 1;
        }
        ///<inheritdoc/>
        public override int GetPassedHitObjectCount(OrtdpWrapper.OrtdpWrapper i)
        {
            if (i is null) return 0;
            return i.Count300 + i.Count100;
        }
        ///<inheritdoc/>
        public override double GetCount300Rate(OrtdpWrapper.OrtdpWrapper info)
        {
            if (info is null) return 0d;
            return AccuracyCalc(info);
        }
        ///<inheritdoc/>
        public override double GetCountGekiRate(OrtdpWrapper.OrtdpWrapper info)
        {
            if (info is null) return 0d;
            return AccuracyCalc(info);
        }
        ///<inheritdoc/>
        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]),out var maybeType);
            if (maybeType == HitObjectTypes.HitCircle || types.Contains(HitObjectTypes.HitCircle))
                hitobject = new Fruit();
            if (maybeType == HitObjectTypes.Slider || types.Contains(HitObjectTypes.Slider))
                hitobject = new JuiceStream();
            if (maybeType == HitObjectTypes.Spinner || types.Contains(HitObjectTypes.Spinner))
                hitobject = new BananaShower();
            if (hitobject == null) throw new IncorrectHitObjectException(this, types[0]);
            hitobject.Parse(data);
            return hitobject;
        }
        ///<inheritdoc/>
        public override GameRanking GetRanking(OrtdpWrapper.OrtdpWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            var isHdOrFl = false;
            if (info.Mods.Count > 0)
                isHdOrFl = info.Mods.Contains(typeof(HiddenMod)) || info.Mods.Contains(typeof(FlashlightMod));
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

            if (AccuracyCalc(info) * 100 > 94)
            {
                return GameRanking.A;
            }

            if (AccuracyCalc(info) * 100 > 90)
            {
                return GameRanking.B;
            }

            if (AccuracyCalc(info) * 100 > 85)
            {
                return GameRanking.C;
            }

            if (AccuracyCalc(info) * 100 < 85)
            {
                return GameRanking.D;
            }

            return GameRanking.Unknown;
        }
       
        /// <inheritdoc/>
        public override int GetBeatmapMaxCombo(OrtdpWrapper.OrtdpWrapper info) =>
            _performanceCalculator.Beatmap.MaxCombo;
        ///<inheritdoc/>
        public override double GetHitObjectPercent(OrtdpWrapper.OrtdpWrapper info) =>
            GetPassedHitObjectCount(info) / (double) GetBeatmapMaxCombo(info);

        
    }
}