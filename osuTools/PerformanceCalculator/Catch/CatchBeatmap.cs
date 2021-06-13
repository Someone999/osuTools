using System;
using System.Collections.Generic;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Beatmaps.HitObject.Catch;
using osuTools.Beatmaps.HitObject.Std;
using osuTools.Game.Modes;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 用于Catch模式pp计算器的谱面
    /// </summary>
    public class CatchBeatmap
    {
        /// <summary>
        /// 被包装的谱面
        /// </summary>
        public Beatmap BaseBeatmap { get; set; }
        /// <summary>
        /// 谱面所有的时间点
        /// </summary>
        public Dictionary<CatchTimePointType,SortedDictionary<double,double>> CatchTimePoints { get; } = new Dictionary<CatchTimePointType, SortedDictionary<double, double>>
        {
            {CatchTimePointType.Bpm, new SortedDictionary<double, double>()},
            {CatchTimePointType.RawBpm, new SortedDictionary<double, double>()},
            {CatchTimePointType.Spm, new SortedDictionary<double, double>()},
            {CatchTimePointType.RawSpm, new SortedDictionary<double, double>()}
        };

        void AddOrAssign(CatchTimePointType type,double offset, double value)
        {
            if (CatchTimePoints[type].ContainsKey(offset))
                CatchTimePoints[type][offset] = value;
            else
                CatchTimePoints[type].Add(offset,value);

        }
        /// <summary>
        /// 谱面的所有处理后的HitObject
        /// </summary>
        public List<CatchHitObject> CatchHitObjects { get; } = new List<CatchHitObject>();
        /// <summary>
        /// 谱面的难度信息
        /// </summary>
        public CatchDifficultyAttribute Difficulty { get; } = new CatchDifficultyAttribute();
        /// <summary>
        /// 谱面的最大连击
        /// </summary>
        public int MaxCombo { get; private set; }
        /// <summary>
        /// 使用一个<seealso cref="Beatmap"/>初始化一个CatchBeatmap
        /// </summary>
        /// <param name="baseBeatmap">谱面</param>

        public CatchBeatmap(Beatmap baseBeatmap)
        {
            if (baseBeatmap is null)
                throw new NullReferenceException("Beatmap is null.");
            if (baseBeatmap.Mode != OsuGameMode.Catch && baseBeatmap.Mode != OsuGameMode.Osu)
                throw new ArgumentException("This mode is not and can not be converted to Catch Mode.");
            BaseBeatmap = baseBeatmap;
            Difficulty.SliderMultiplier = baseBeatmap.SliderMultiplier;
            Difficulty.SliderTickRate = baseBeatmap.SliderTickRate;
            Difficulty.ApprochRate = baseBeatmap.ApproachRate;
            Difficulty.CircleSize = baseBeatmap.CircleSize;
            Difficulty.OverallDifficulty = baseBeatmap.OverallDifficulty;
            Difficulty.HpDrain = baseBeatmap.HpDrain;
            if (baseBeatmap.ApproachRate == 0)
                Difficulty.ApprochRate = Difficulty.CircleSize;
            HandleTimePoints();
            HandleHitObject();
        }

        void HandleTimePoints()
        {
            var tmpts = BaseBeatmap.TimePoints.TimePoints;
            foreach (var t in tmpts)
            {
                double timefocus = t.BeatLength;
                double offset = t.Offset;
                if (!t.Uninherited && t.BeatLength >= 0)
                    timefocus = -100;
                if (timefocus < 0)
                {
                    AddOrAssign(CatchTimePointType.Spm,t.Offset,-100 / t.BeatLength);
                    AddOrAssign(CatchTimePointType.RawSpm,t.Offset, t.BeatLength);
                }
                else
                {
                    if (CatchTimePoints[CatchTimePointType.Bpm].Count == 0)
                        offset = 0;
                    AddOrAssign(CatchTimePointType.Bpm,offset,t.Bpm);
                    AddOrAssign(CatchTimePointType.RawBpm,offset,t.BeatLength);
                    AddOrAssign(CatchTimePointType.Spm,offset, 1);
                    AddOrAssign(CatchTimePointType.RawSpm,offset, -100);
                }
            }
        }

        Dictionary<CatchTimePointType,double> GetAllTimePoints(double time)
        {
            Dictionary<CatchTimePointType, double> dict = new Dictionary<CatchTimePointType, double>();

            double bpmVal = GetTimePoint(time, CatchTimePointType.Bpm);
            double rawBpmVal = GetTimePoint(time, CatchTimePointType.RawBpm);
            double spmVal = GetTimePoint(time, CatchTimePointType.Spm);
            double rawSpmVal = GetTimePoint(time, CatchTimePointType.RawSpm);
            dict.Add(CatchTimePointType.Bpm, double.IsNaN(bpmVal) ? 100 : bpmVal);
            dict.Add(CatchTimePointType.RawBpm, double.IsNaN(rawBpmVal) ? 600:rawBpmVal);
            dict.Add(CatchTimePointType.Spm, double.IsNaN(spmVal) ? 1 : spmVal);
            dict.Add(CatchTimePointType.RawSpm, double.IsNaN(rawSpmVal) ? -100 : rawSpmVal);
            return dict;
        }

        double GetTimePoint(double time, CatchTimePointType timePointType)
        {
            double r = double.NaN;
            try
            {
                foreach (var tmpt in CatchTimePoints[timePointType].Keys)
                {
                    if (tmpt <= time)
                        r = CatchTimePoints[timePointType][tmpt];
                    else
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return r;
        }

        void HandleHitObject()
        {
            var hitObjs = BaseBeatmap.HitObjects;
            foreach (var hitObject in hitObjs)
            {
                CatchHitObject catchHitObject;
                if (hitObject.HitObjectType == HitObjectTypes.Spinner || hitObject.HitObjectType==HitObjectTypes.BananaShower)
                    continue;
                if (hitObject.HitObjectType == HitObjectTypes.Slider || hitObject.HitObjectType==HitObjectTypes.JuiceStream)
                {
                    dynamic j;
                    if (hitObject is JuiceStream stream)
                        j = stream;
                    else
                        j = hitObject as Slider;
                    double repeat = j?.RepeatTime ?? throw new InvalidOperationException();
                    double pLen = j.Length;
                    var tmPt = GetAllTimePoints(hitObject.Offset);
                    ValueObserver<double> tickDistance = ValueObserver<double>.FromValue((100 * BaseBeatmap.SliderMultiplier) / BaseBeatmap.SliderTickRate);
                    if(BaseBeatmap.BeatmapVersion >= 8)
                        tickDistance /= (MathUtlity.Clamp(-1 * tmPt[CatchTimePointType.RawSpm], 10, 1000) / 100);
                    var curvePoints = new List<OsuPixel>(j.CurvePoints);

                    var sliderType = j.CurveType;
                    if(BaseBeatmap.BeatmapVersion <= 6 && curvePoints.Count >= 2)
                        if (sliderType == CurveTypes.Linear)
                            sliderType = CurveTypes.Bezier;
                    if (curvePoints.Count == 2)
                    {
                        if (Math.Abs((int) (j.Position.x) - curvePoints[0].x) == 0 && Math.Abs((int) (j.Position.y) - curvePoints[0].y) == 0 || 
                            (Math.Abs(curvePoints[0].x - curvePoints[1].x) == 0 && Math.Abs(curvePoints[0].y - curvePoints[1].y) == 0))
                        {
                            curvePoints.RemoveAt(0);
                            sliderType = CurveTypes.Linear;
                        }
                    }

                    j.curvePoints = curvePoints;

                    j.CurveType = sliderType;
                    catchHitObject = curvePoints.Count == 0 ? new CatchHitObject(hitObject) : new CatchHitObject(hitObject, tmPt, Difficulty, tickDistance);
                }
                else
                {
                    catchHitObject = new CatchHitObject(hitObject);
                }
                CatchHitObjects.Add(catchHitObject);
                MaxCombo += catchHitObject.GetCombo();
            }
        }
    }

}