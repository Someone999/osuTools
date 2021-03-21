using System;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;
using osuTools.osuToolsException;
using RealTimePPDisplayer.Beatmap;
using RealTimePPDisplayer.Calculator;
using RealTimePPDisplayer.Displayer;
using Sync.Tools;

namespace osuTools.Game.Modes
{
    public class ManiaMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private static ManiaPerformanceCalculator calculator;
        public override string ModeName => "Mania";
        public override Mod[] AvaliableMods => Mod.ManiaMods;
        public override string Description => "砸键盘";

        public double GetMaxPerformance(ORTDPWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).MaxPP;
        }

        public PPTuple GetPPTuple(ORTDPWrapper ortdpInfo)
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
                calculator = calculator == null ? new ManiaPerformanceCalculator() : calculator;
                calculator.ClearCache();
                calculator.Beatmap = new BeatmapReader(ortdpInfo.ORTDPBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Mania] Current ORTDP Beatmap:{calculator.Beatmap.OrtdpBeatmap.Artist} - {calculator.Beatmap.OrtdpBeatmap.Title} [{calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
                        true, false);
                calculator.Count50 = 0;
                calculator.CountGeki = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.CountKatu = 0;
                calculator.Count100 = 0;
                calculator.CountMiss = 0;
                calculator.Mods = (uint) ortdpInfo.Mods.ToIntMod();
                calculator.MaxCombo = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.Count300 = 0;
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Mania] Calc Completed", true, false);
                return calculator.GetPerformance();
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

        public double GetPerformance(ORTDPWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).RealTimePP;
        }

        public OsuGameMode LegacyMode => OsuGameMode.Mania;

        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300g = scoreInfo.c300g;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1 / 3.0) + c50 * (1 / 6.0)) /
                           (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override double AccuracyCalc(ORTDPWrapper scoreInfo)
        {
            double c300g = scoreInfo.c300g;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1.0 / 3) + c50 * (1 / 6.0)) /
                           (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override int GetPassedHitObjectCount(ORTDPWrapper info)
        {
            if (info is null) return 0;
            return info.c300g + info.c300 + info.c200 + info.c100 + info.c50 + info.cMiss;
        }

        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            return b.HitObjects.Count;
        }

        public override double GetC300gRate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            var rawValue = info.c300g / (double) (info.c300 + info.c300g);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }

        public override double GetC300Rate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            double rawValue = 0;
            if (info.c300g > 0 && info.c300 == 0)
                rawValue = GetC300gRate(info);
            else
                rawValue = (info.c300 + info.c300g) /
                           (double) (info.c300g + info.c300 + info.c200 + info.c100 + info.c50 + info.cMiss);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }

        public override GameRanking GetRanking(ORTDPWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            bool isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            return AccuracyCalc(info) * 100 >= 100 ? isHDOrFL ? GameRanking.SSH : GameRanking.SS :
                AccuracyCalc(info) * 100 > 95 ? isHDOrFL ? GameRanking.SH : GameRanking.S :
                AccuracyCalc(info) * 100 > 90 ? GameRanking.A :
                AccuracyCalc(info) * 100 > 80 ? GameRanking.B :
                AccuracyCalc(info) * 100 > 70 ? GameRanking.C :
                GameRanking.D;
        }

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

        public override bool IsPerfect(ORTDPWrapper info)
        {
            if (info is null) return false;
            return info.c100 + info.c50 + info.cMiss == 0;
        }
    }
}