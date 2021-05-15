using System;
using System.Linq;
using System.Text;
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
    public class TaikoMode : GameMode, ILegacyMode, IHasPerformanceCalculator
    {
        private TaikoPerformanceCalculator _calculator;
        ///<inheritdoc/>
        public override string ModeName => "Taiko";
        ///<inheritdoc/>
        public override Mod[] AvaliableMods => Mod.TaikoMods;
        ///<inheritdoc/>
        public override string Description => "打鼓";
        ///<inheritdoc/>
        public double GetMaxPerformance(ORTDP.OrtdpWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).MaxPP;
        }
        ///<inheritdoc/>
        public void SetBeatmap(Beatmap b)
        {

        }
        ///<inheritdoc/>
        public PPTuple GetPPTuple(ORTDP.OrtdpWrapper ortdpInfo)
        {
            try
            {
                _calculator = _calculator == null ? new TaikoPerformanceCalculator() : _calculator;
                _calculator.Beatmap = new BeatmapReader(ortdpInfo.OrtdpBeatmap, (int) ortdpInfo.Beatmap.Mode);
                if (ortdpInfo.DebugMode)
                    IO.CurrentIO.Write(
                        $"[osuTools::PrePPCalc::Taiko] Current ORTDP Beatmap:{_calculator.Beatmap.OrtdpBeatmap.Artist} - {_calculator.Beatmap.OrtdpBeatmap.Title} [{_calculator.Beatmap.OrtdpBeatmap.Difficulty}]",
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
                if (ortdpInfo.DebugMode) IO.CurrentIO.Write("[osuTools::PrePPCalc::Taiko] Calc Completed", true, false);
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
        ///<inheritdoc/>
        public double GetPerformance(ORTDP.OrtdpWrapper ortdpInfo)
        {
            return GetPPTuple(ortdpInfo).RealTimePP;
        }
        ///<inheritdoc/>
        public OsuGameMode LegacyMode => OsuGameMode.Taiko;
        ///<inheritdoc/>
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.Count300;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;
            var rawValue = (c300 + c100 * 0.5d) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        ///<inheritdoc/>
        public override double AccuracyCalc(ORTDP.OrtdpWrapper scoreInfo)
        {
            double c300 = scoreInfo.Count300;
            double c100 = scoreInfo.Count100;
            double cMiss = scoreInfo.CountMiss;
            var rawValue = (c300 + c100 * 0.5d) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        ///<inheritdoc/>
        public override bool IsPerfect(ORTDP.OrtdpWrapper info)
        {
            return info.CountMiss <= 0;
        }
        ///<inheritdoc/>
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            var hitObjects = b.HitObjects;
            var normalHit = hitObjects.Where(h => h.HitObjectType != HitObjectTypes.DrumRoll).Count();
            return normalHit;
        }

        public override double GetCountGekiRate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            return GetCount300Rate(info);
        }
        ///<inheritdoc/>
        public override double GetCount300Rate(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return 0;
            return (double) info.Count300 / (info.Count300 + info.Count100 + info.CountMiss);
        }
        ///<inheritdoc/>
        public override GameRanking GetRanking(ORTDP.OrtdpWrapper info)
        {
            if (info is null) return GameRanking.Unknown;
            var noMiss = info.CountMiss == 0;
            double All = info.Count300 + info.Count100 + info.Count50 + info.CountMiss;
            var c100Rate = info.Count100 / All;
            var isHdOrFl = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHdOrFl = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (Math.Abs(AccuracyCalc(info) * 100 - 100) < double.Epsilon && info.Count300 == (int)All)
            {
                if (isHdOrFl) return GameRanking.SSH;
                return GameRanking.SS;
            }

            if (AccuracyCalc(info) * 100 > 93.17 && c100Rate < 0.1 && GetCount300Rate(info) > 0.9 && noMiss)
            {
                if (isHdOrFl) return GameRanking.SH;
                return GameRanking.S;
            }

            if (GetCount300Rate(info) > 0.8 && noMiss || GetCount300Rate(info) > 0.9 && !noMiss)
            {
                return GameRanking.A;
            }

            if (GetCount300Rate(info) > 0.8 && !noMiss || GetCount300Rate(info) > 0.7 && noMiss)
            {
                return GameRanking.B;
            }

            if (GetCount300Rate(info) > 0.6)
            {
                return GameRanking.C;
            }

            return GameRanking.D;
        }
        ///<inheritdoc/>
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

        public override int GetPassedHitObjectCount(ORTDP.OrtdpWrapper info)
        {
            return info.Count300 + info.Count100 + info.CountMiss;
        }
    }
}