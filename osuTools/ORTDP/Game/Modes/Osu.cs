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
    public class OsuMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private StdPerformanceCalculator calculator;
        public override string ModeName => "Osu";
        public override Mod[] AvaliableMods => Mod.OsuMods;
        public override string Description => "戳泡泡";

        public double GetMaxPerformance(ORTDPWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).MaxPP;
        }

        public void SetBeatmap(Beatmap b)
        {
        }
        public PPTuple GetPPTuple(ORTDPWrapper ortdpInfo)
        {
            try
            {
                calculator = calculator == null ? new StdPerformanceCalculator() : calculator;
                calculator.ClearCache();
                calculator.Beatmap = new BeatmapReader(ortdpInfo.ORTDPBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Std] Current ORTDP Beatmap:{calculator.Beatmap.OrtdpBeatmap.Artist} - {calculator.Beatmap.OrtdpBeatmap.Title} [{calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
                        true, false);
                calculator.Count50 = 0;
                calculator.CountGeki = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.CountKatu = 0;
                calculator.Count100 = 0;
                calculator.CountMiss = 0;
                calculator.Mods = (uint) ortdpInfo.Mods.ToIntMod();
                calculator.MaxCombo = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.Count300 = 0;
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Std] Calc Completed", true, false);
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

        public OsuGameMode LegacyMode => OsuGameMode.Osu;

        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double c50 = scoreInfo.c50;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300 + c100 * (1 / 3d) + c50 * (1 / 6d)) / (c300 + c100 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            var type = HitObjectTypes.Unknown;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            if (types.Contains(HitObjectTypes.HitCircle))
                hitobject = new HitCircle();
            if (types.Contains(HitObjectTypes.Slider))
                hitobject = new Slider();
            if (types.Contains(HitObjectTypes.Spinner))
                hitobject = new Spinner();
            type = types[0];
            if (hitobject == null) throw new IncorrectHitObjectException(this, type);
            hitobject.Parse(data);
            return hitobject;
        }

        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            return b is null ? 0 : b.HitObjects.Count;
        }

        public override int GetPassedHitObjectCount(ORTDPWrapper info)
        {
            return info is null ? 0 : info.c300 + info.c100 + info.c50 + info.cMiss;
        }

        public override bool IsPerfect(ORTDPWrapper info)
        {
            return !(info is null) && info.PlayerMaxCombo == info.MaxCombo;
        }

        public override double AccuracyCalc(ORTDPWrapper ortdpInfo)
        {
            if (ortdpInfo is null) return 0;
            double c300 = ortdpInfo.c300;
            double c100 = ortdpInfo.c100;
            double c50 = ortdpInfo.c50;
            double cMiss = ortdpInfo.cMiss;
            var rawValue = (c300 + c100 * (1 / 3d) + c50 * (1 / 6d)) / (c300 + c100 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override GameRanking GetRanking(ORTDPWrapper info)
        {
            if (info is null) return GameRanking.Unknown;

            var NoMiss = info.cMiss == 0;
            double All = info.c300 + info.c100 + info.c50 + info.cMiss;
            var c100Rate = info.c100 / All;
            var c50Rate = info.c50 / All;
            var isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (AccuracyCalc(info) * 100 == 100 && info.c300 == All)
            {
                if (isHDOrFL) return GameRanking.SSH;
                return GameRanking.SS;
            }

            if (AccuracyCalc(info) * 100 > 93.17 && c50Rate < 0.01 && c100Rate < 0.1 && GetC300Rate(info) > 0.9 &&
                NoMiss)
            {
                if (isHDOrFL) return GameRanking.SH;
                return GameRanking.S;
            }

            if (GetC300Rate(info) > 0.8 && NoMiss || GetC300Rate(info) > 0.9 && !NoMiss)
            {
                return GameRanking.A;
            }

            if (GetC300Rate(info) > 0.8 && !NoMiss || GetC300Rate(info) > 0.7 && NoMiss)
            {
                return GameRanking.B;
            }

            if (GetC300Rate(info) > 0.6)
            {
                return GameRanking.C;
            }

            return GameRanking.D;
        }

        public override double GetC300gRate(ORTDPWrapper info)
        {
            var rawValue = info.c300g / (double) (info.c300g + info.c200);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }

        public override double GetC300Rate(ORTDPWrapper info)
        {
            var rawValue = info.c300 / (double) (info.c300 + info.c100 + info.c50 + info.cMiss);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            return rawValue;
        }
    }
}