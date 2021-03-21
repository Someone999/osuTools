using System;
using System.Linq;
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
    public class CatchMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private CatchTheBeatPerformanceCalculator calculator;
        public override string ModeName => "Catch";
        public override Mod[] AvaliableMods => Mod.CatchMods;
        public override string Description => "接水果";

        public double GetMaxPerformance(ORTDPWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).MaxPP;
        }

        public PPTuple GetPPTuple(ORTDPWrapper ortdpInfo)
        {
            try
            {
                calculator = calculator == null ? new CatchTheBeatPerformanceCalculator() : calculator;
                calculator.Beatmap = new BeatmapReader(ortdpInfo.ORTDPBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Catch] Current ORTDP Beatmap:{calculator.Beatmap.OrtdpBeatmap.Artist} - {calculator.Beatmap.OrtdpBeatmap.Title} [{calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
                        true, false);
                calculator.ClearCache();
                calculator.Count50 = 0;
                calculator.CountGeki = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.CountKatu = 0;
                calculator.Count100 = 0;
                calculator.CountMiss = 0;
                calculator.Mods = (uint) ortdpInfo.Mods.ToIntMod();
                calculator.MaxCombo = ortdpInfo.Beatmap.HitObjects.Count;
                calculator.Count300 = 0;
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Catch] Calc Completed", true, false);
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

        public OsuGameMode LegacyMode => OsuGameMode.Catch;

        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override double AccuracyCalc(ORTDPWrapper scoreInfo)
        {
            if (scoreInfo is null) return 0;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;

            var rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b == null) return 0;
            var hitObjects = b.HitObjects;
            var fruits = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.Fruit);
            var juice = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.JuiceStream);
            var bananaShower = hitObjects.Where(h => h.HitObjectType == HitObjectTypes.BananaShower);
            return hitObjects.Count + juice.Count() - bananaShower.Count();
        }

        public override bool IsPerfect(ORTDPWrapper info)
        {
            return AccuracyCalc(info) >= 1;
        }

        public override int GetPassedHitObjectCount(ORTDPWrapper i)
        {
            if (i is null) return 0;
            return i.c300 + i.c100;
        }

        public override double GetC300Rate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }

        public override double GetC300gRate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }

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

        public override GameRanking GetRanking(ORTDPWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            var isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (AccuracyCalc(info) * 100 == 100)
            {

                if (isHDOrFL) return GameRanking.SSH;
                return GameRanking.SS;
            }

            if (AccuracyCalc(info) * 100 > 98.01)
            {
                if (isHDOrFL) return GameRanking.SH;
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