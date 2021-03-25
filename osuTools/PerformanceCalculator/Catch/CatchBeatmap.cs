using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using RealTimePPDisplayer.Calculator;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchBeatmap
    {
        public Beatmap BaseBeatmap { get; set; }
        public Dictionary<CatchTimePointType,SortedDictionary<double,double>> CatchTimePoints { get; } = new Dictionary<CatchTimePointType, SortedDictionary<double, double>>
        {
            {CatchTimePointType.BPM, new SortedDictionary<double, double>()},
            {CatchTimePointType.RawBPM, new SortedDictionary<double, double>()},
            {CatchTimePointType.SPM, new SortedDictionary<double, double>()},
            {CatchTimePointType.RawSPM, new SortedDictionary<double, double>()}
        };

        void AddOrAssign(CatchTimePointType type,double offset, double value)
        {
            if (CatchTimePoints[type].ContainsKey(offset))
                CatchTimePoints[type][offset] = value;
            else
                CatchTimePoints[type].Add(offset,value);
            
        }
        public List<CatchHitObject> CatchHitObjects { get; } = new List<CatchHitObject>();
        public CatchDifficultyAttribute Difficulty { get; } = new CatchDifficultyAttribute();
        public int MaxCombo { get; private set; }

        public CatchBeatmap(Beatmap baseBeatmap)
        {
            if (baseBeatmap is null)
                throw new NullReferenceException("Beatmap is null.");
            if (baseBeatmap.Mode != OsuGameMode.Catch && baseBeatmap.Mode != OsuGameMode.Osu)
                throw new ArgumentException("This mode is not and can not be converted to Catch Mode.");
            BaseBeatmap = baseBeatmap;
            Difficulty.SliderMultiplier = baseBeatmap.SliderMultiplier;
            Difficulty.SliderTickRate = baseBeatmap.SliderTickRate;
            Difficulty.ApprochRate = baseBeatmap.AR;
            Difficulty.CircleSize = baseBeatmap.CS;
            Difficulty.OverallDifficulty = baseBeatmap.OD;
            Difficulty.HPDrain = baseBeatmap.HP;
            if (baseBeatmap.AR == 0)
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
                if (!t.Uninherited && t.BeatLength < 0)
                    timefocus = -100;
                if (timefocus < 0)
                {
                    AddOrAssign(CatchTimePointType.SPM,t.Offset,-100 / t.BeatLength);
                    AddOrAssign(CatchTimePointType.RawSPM,t.Offset, t.BeatLength);
                }
                else
                {
                    if (CatchTimePoints[CatchTimePointType.BPM].Count == 0)
                        offset = 0;
                    AddOrAssign(CatchTimePointType.BPM,offset,t.BPM);
                    AddOrAssign(CatchTimePointType.RawBPM,offset,t.BeatLength);
                    AddOrAssign(CatchTimePointType.SPM,offset, 1);
                    AddOrAssign(CatchTimePointType.RawSPM,offset, -100);
                }
            }
        }

        Dictionary<CatchTimePointType,double> GetAllTimePoints(double time)
        {
            Dictionary<CatchTimePointType, double> dict = new Dictionary<CatchTimePointType, double>();
               
            double bpmVal = GetTimePoint(time, CatchTimePointType.BPM);
            double rawBpmVal = GetTimePoint(time, CatchTimePointType.RawBPM);
            double spmVal = GetTimePoint(time, CatchTimePointType.SPM);
            double rawSpmVal = GetTimePoint(time, CatchTimePointType.RawSPM);
            dict.Add(CatchTimePointType.BPM, double.IsNaN(bpmVal) ? 100 : bpmVal);
            dict.Add(CatchTimePointType.RawBPM, double.IsNaN(rawBpmVal) ? 600:rawBpmVal);
            dict.Add(CatchTimePointType.SPM, double.IsNaN(spmVal) ? 1 : spmVal);
            dict.Add(CatchTimePointType.RawSPM, double.IsNaN(rawSpmVal) ? -100 : rawSpmVal);
            return dict;
        }

        double GetTimePoint(double time, CatchTimePointType timePointType)
        {
            double r = double.NaN;
            try
            {
                foreach (var tmpt in CatchTimePoints[timePointType].Keys)
                {
                    if (tmpt < time)
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
                CatchHitObject catchHitObject = null;
                if (hitObject.HitObjectType == HitObjectTypes.Spinner || hitObject.HitObjectType==HitObjectTypes.BananaShower)
                    continue;
                if (hitObject.HitObjectType == HitObjectTypes.Slider || hitObject.HitObjectType==HitObjectTypes.JuiceStream)
                {
                    dynamic j = null;
                    if (hitObject is JuiceStream)
                        j = hitObject as JuiceStream;
                    else
                        j = hitObject as Slider;
                    double repeat = j.RepeatTime;
                    double pLen = j.Length;
                    var tmPt = GetAllTimePoints(hitObject.Offset);
                    ValueObserver<double> tickDistance = ValueObserver<double>.FromValue((100 * BaseBeatmap.SliderMultiplier) / BaseBeatmap.SliderTickRate);
                    if(BaseBeatmap.BeatmapVersion >= 8)
                        tickDistance /= (MathUtlity.Clamp(-1 * tmPt[CatchTimePointType.RawSPM], 10, 1000) / 100);
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
                    if (curvePoints.Count == 0)
                        catchHitObject = new CatchHitObject(hitObject);
                    else
                    {
                       
                        catchHitObject =
                            new CatchHitObject(hitObject, tmPt, Difficulty, tickDistance);
                        

                    }
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