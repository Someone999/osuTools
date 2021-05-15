using System;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.Game.Mods;
using RealTimePPDisplayer.Beatmap;
using RealTimePPDisplayer.Calculator;
using RealTimePPDisplayer.Displayer;
using Sync.Tools;

namespace osuTools.Game.Modes
{
    public class ManiaMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private static ManiaPerformanceCalculator _calculator;
        /// <inheritdoc />
        public override string ModeName => "Mania";
        /// <inheritdoc/>
        public override Mod[] AvaliableMods => Mod.ManiaMods;
        /// <inheritdoc/>
        public override string Description => "砸键盘";
        /// <inheritdoc/>
        public double GetMaxPerformance(ORTDP.OrtdpWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).MaxPP;
        } 
        /// <inheritdoc/>

        public void SetBeatmap(Beatmap b)
        {
        }
        /// <inheritdoc/>
        public PPTuple GetPPTuple(ORTDP.OrtdpWrapper ortdpInfo)
        {
            try
            {
                if (ortdpInfo.CurrentMode == OsuGameMode.Mania && ortdpInfo.Beatmap.Mode != OsuGameMode.Mania)
                    return new PPTuple
                    {
                        FullComboAccuracyPP = 0,
                        FullComboAimPP = 0,
                        FullComboPP = 0,
                        FullComboSpeedPP = 0,
                        MaxAccuracyPP = 0,
                        MaxAimPP = 0,
                        MaxPP = 0,
                        MaxSpeedPP = 0,
                        RealTimeAccuracyPP = 0,
                        RealTimeAimPP = 0,
                        RealTimePP = 0,
                        RealTimeSpeedPP = 0
                    };
                _calculator = _calculator ?? new ManiaPerformanceCalculator();
                _calculator.ClearCache();
                _calculator.Beatmap = new BeatmapReader(ortdpInfo.OrtdpBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Mania] Current ORTDP Beatmap:{_calculator.Beatmap.OrtdpBeatmap.Artist} - {_calculator.Beatmap.OrtdpBeatmap.Title} [{_calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
                        true, false);
                _calculator.Count50 = 0;
                _calculator.CountGeki = ortdpInfo.Beatmap.HitObjects.Count;
                _calculator.CountKatu = 0;
                _calculator.Count100 = 0;
                _calculator.CountMiss = 0;
                _calculator.Mods = (uint) ortdpInfo.Mods.ToIntMod();
                _calculator.MaxCombo = ortdpInfo.Beatmap.HitObjects.Count;
                _calculator.Count300 = 0;
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Mania] Calc Completed", true, false);
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
        /// <inheritdoc/>
        public double GetPerformance(ORTDP.OrtdpWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).RealTimePP;
        }
        /// <inheritdoc/>
        public OsuGameMode LegacyMode => OsuGameMode.Mania;
        /// <inheritdoc/>
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300g = scoreInfo.CountGeki;
            double c300 = scoreInfo.Count300;
            double c200 = scoreInfo.CountKatu;
            double c50 = scoreInfo.Count50;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;
            var rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1 / 3.0) + c50 * (1 / 6.0)) /
                           (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        /// <inheritdoc/>
        public override double AccuracyCalc(ORTDP.OrtdpWrapper scoreInfo)
        {
            double c300g = scoreInfo.CountGeki;
            double c300 = scoreInfo.Count300;
            double c200 = scoreInfo.CountKatu;
            double c50 = scoreInfo.Count50;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;
            var rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1.0 / 3) + c50 * (1 / 6.0)) /
                           (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        /// <inheritdoc/>
        public override int GetPassedHitObjectCount(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            return info.CountGeki + info.Count300 + info.CountKatu + info.Count100 + info.Count50 + info.CountMiss;
        }
        /// <inheritdoc/>
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            return b.HitObjects.Count;
        }
        /// <inheritdoc/>
        public override double GetCountGekiRate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            var rawValue = info.CountGeki / (double) (info.Count300 + info.CountGeki);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }
        /// <inheritdoc/>
        public override double GetCount300Rate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            double rawValue;
            if (info.CountGeki > 0 && info.Count300 == 0)
                rawValue = GetCountGekiRate(info);
            else
                rawValue = (info.Count300 + info.CountGeki) /
                           (double) (info.CountGeki + info.Count300 + info.CountKatu + info.Count100 + info.Count50 + info.CountMiss);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }
        /// <inheritdoc/>
        public override GameRanking GetRanking(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            bool isHdOrFl = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHdOrFl = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            return AccuracyCalc(info) * 100 >= 100 ? isHdOrFl ? GameRanking.SSH : GameRanking.SS :
                AccuracyCalc(info) * 100 > 95 ? isHdOrFl ? GameRanking.SH : GameRanking.S :
                AccuracyCalc(info) * 100 > 90 ? GameRanking.A :
                AccuracyCalc(info) * 100 > 80 ? GameRanking.B :
                AccuracyCalc(info) * 100 > 70 ? GameRanking.C :
                GameRanking.D;
        }
        /// <inheritdoc/>
        public override IHitObject CreateHitObject(string data, int maniaColumn)
        {
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            IHitObject hitobject = null;
            if (types.Contains(HitObjectTypes.HitCircle))
                hitobject = new ManiaHit();
            if (types.Contains(HitObjectTypes.ManiaHold))
                hitobject = new ManiaHold();
            if (hitobject == null)
                throw new IncorrectHitObjectException(this, types[0]);
            ((IManiaHitObject) hitobject).BeatmapColumn = maniaColumn;
            hitobject.Parse(data);
            return hitobject;
        }
        /// <inheritdoc/>
        public override bool IsPerfect(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return false;
            return info.Count100 + info.Count50 + info.CountMiss == 0;
        }
    }
}