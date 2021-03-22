using System;
using System.Linq;
using System.Text;
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
    public class TaikoMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private TaikoPerformanceCalculator calculator;
        public override string ModeName => "Taiko";
        public override Mod[] AvaliableMods => Mod.TaikoMods;
        public override string Description => "打鼓";

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
                calculator = calculator == null ? new TaikoPerformanceCalculator() : calculator;
                calculator.Beatmap = new BeatmapReader(ortdpInfo.ORTDPBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Taiko] Current ORTDP Beatmap:{calculator.Beatmap.OrtdpBeatmap.Artist} - {calculator.Beatmap.OrtdpBeatmap.Title} [{calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
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
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Taiko] Calc Completed", true, false);
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

        public OsuGameMode LegacyMode => OsuGameMode.Taiko;

        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300 + c100 * 0.5d) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override double AccuracyCalc(ORTDPWrapper scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            var rawValue = (c300 + c100 * 0.5d) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }

        public override bool IsPerfect(ORTDPWrapper info)
        {
            return info.cMiss <= 0;
        }

        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            var hitObjects = b.HitObjects;
            var normalHit = hitObjects.Where(h => h.HitObjectType != HitObjectTypes.DrumRoll).Count();
            return normalHit;
        }

        public override double GetC300gRate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            return GetC300Rate(info);
        }

        public override double GetC300Rate(ORTDPWrapper info)
        {
            if (info is null) return 0;
            return (double) info.c300 / (info.c300 + info.c100 + info.cMiss);
        }

        public override GameRanking GetRanking(ORTDPWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            var NoMiss = info.cMiss == 0;
            double All = info.c300 + info.c100 + info.c50 + info.cMiss;
            var c100Rate = info.c100 / All;
            var isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (AccuracyCalc(info) * 100 == 100 && info.c300 == All)
            {
                if (isHDOrFL) return GameRanking.SSH;
                return GameRanking.SS;
            }

            if (AccuracyCalc(info) * 100 > 93.17 && c100Rate < 0.1 && GetC300Rate(info) > 0.9 && NoMiss)
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

        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            var hitSounds = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(d[4]));
            if (types.Contains(HitObjectTypes.Slider) || types.Contains(HitObjectTypes.Spinner))
                hitobject = new DrumRoll();
            if (types.Contains(HitObjectTypes.HitCircle))
            {
                if (hitSounds.Contains(HitSounds.Finish))
                    hitobject = new LargeTaikoRedHit();
                if (hitSounds.Contains(HitSounds.Normal))
                    if (hitSounds.Contains(HitSounds.Finish))
                        hitobject = new LargeTaikoRedHit();
                    else
                        hitobject = new TaikoRedHit();
                if (hitSounds.Contains(HitSounds.Whistle) || hitSounds.Contains(HitSounds.Clap))
                    if (hitSounds.Contains(HitSounds.Finish))
                        hitobject = new LargeTaikoBlueHit();
                    else
                        hitobject = new TaikoBlueHit();
            }

            if (hitobject == null)
            {
                var builder = new StringBuilder("HitObject类型:");
                for (var i = 0; i < types.Count; i++)
                {
                    builder.Append(types[i]);
                    if (i != types.Count - 1)
                        builder.Append(", ");
                }

                builder.Append("  HitSounds:");
                for (var i = 0; i < hitSounds.Count; i++)
                {
                    builder.Append(hitSounds[i]);
                    if (i != hitSounds.Count - 1)
                        builder.Append(", ");
                }

                throw new IncorrectHitObjectException(this, types[0], builder.ToString());
            }

            hitobject.Parse(data);
            return hitobject;
        }

        public override int GetPassedHitObjectCount(ORTDPWrapper info)
        {
            return info.c300 + info.c100 + info.cMiss;
        }
    }
}