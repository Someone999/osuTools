using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.ExtraMethods;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchHitObject:ICatchHitObject
    {
        public double x { get=>BaseHitObject.Position.x; }
        public double y { get => BaseHitObject.Position.y; }
        public double Offset { get => BaseHitObject.Offset; }
        public IHitObject BaseHitObject { get; }
        public List<CatchSliderTick> Ticks { get; } = new List<CatchSliderTick>();
        public List<CatchSliderTick> EndTicks { get; } = new List<CatchSliderTick>();
        public List<OsuPixel> Path { get; internal set; } = new List<OsuPixel>();
        public Dictionary<CatchTimePointType, double> TimePoint { get; } = null;
        public CatchDifficultyAttribute Difficulty { get; } = null;
        public double TickDistance { get; }

        public double Duration { get; }

        public CatchHitObject(IHitObject hitobject, Dictionary<CatchTimePointType, double> timePoint = null,CatchDifficultyAttribute difficulty=null,double tickDistance = 1)
        {

            if (hitobject.SpecifiedMode != OsuGameMode.Catch && hitobject.SpecifiedMode != OsuGameMode.Osu) 
                throw new ArgumentException("Not a catch HitObject");
            BaseHitObject = hitobject;
            TimePoint = timePoint;
            TickDistance = tickDistance;
            Difficulty = difficulty;

            if (BaseHitObject.HitObjectType == HitObjectTypes.Slider ||
                BaseHitObject.HitObjectType == HitObjectTypes.JuiceStream)
            {
               
                dynamic j = null;
                if (BaseHitObject is JuiceStream)
                    j = BaseHitObject as JuiceStream;
                else
                    j = BaseHitObject as Slider;
                Duration = ((int) TimePoint[CatchTimePointType.RawBPM] *
                            (j.Length / (difficulty.SliderMultiplier * TimePoint[CatchTimePointType.SPM])) /
                            100) *
                           j.RepeatTime;

                j.curvePoints.Insert(0, j.Position);
                CalcSlider();
            }
        }
        internal void CalcSlider(bool calcPath = false)
        {
            dynamic j = null;
            if (BaseHitObject is JuiceStream)
                j = BaseHitObject as JuiceStream;
            else
                j = BaseHitObject as Slider;
            if (j.CurveType == CurveTypes.PerfectCircle && j.curvePoints.Count > 3)
                j.CurveType = CurveTypes.Bezier;
            else if (j.curvePoints.Count == 2)
                j.CurveType = CurveTypes.Linear;
           
            ICurveAlgorithm curve = null;
            if (j.CurveType == CurveTypes.PerfectCircle)
            {
                try
                {
                    curve = new Perfect(j.curvePoints);
                }
                catch (Exception e)
                {
                    curve = new Bezier(j.curvePoints);
                    j.CurveType = CurveTypes.Bezier;
                }
            }
            else if (j.CurveType == CurveTypes.Bezier)
            {
                curve = new Bezier(j.curvePoints);
            }
            else if (j.CurveType == CurveTypes.CentripetalCatmullRom)
            {
                curve = new Catmull(j.curvePoints);
            }

            if (calcPath)
            {
                if (j.CurveType == CurveTypes.Linear)
                {
                    Path = new Linear(j.curvePoints).Position;
                }

                if (j.CurveType == CurveTypes.PerfectCircle)
                {
                    Path = new List<OsuPixel>();
                    var l = 0;
                    var step = 5;
                    while (l < j.Length)
                        Path.Add((curve as Perfect).PointAtDistance(l));
                }
                else
                    throw new NotSupportedException("Slidertype not supported!");
            }
            OsuPixel point=null;
            double currentDis = TickDistance;
            double addTime = Duration * (TickDistance / (j.Length * j.RepeatTime));
            
            while(currentDis< j.Length - TickDistance / 8)
            {
                if (j.CurveType == CurveTypes.Linear)
                {
                    point = MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], currentDis);
                }
                else
                {
                    
                        point = (curve as IHasPointProcessor).PointAtDistance(currentDis);
                }
                
                Ticks.Add((new CatchSliderTick(point.x, point.y, j.Offset + addTime * (Ticks.Count + 1))));
                
                currentDis += TickDistance;
            }
            

            int repeatId = 1;
            List<CatchSliderTick> repeatBonusTick = new List<CatchSliderTick>();
            while (repeatId < j.RepeatTime)
            {
                double dist = (1 & repeatId) * j.Length;
                double timeOffset = (Duration / j.RepeatTime) * repeatId;
                if (j.CurveType == CurveTypes.Linear)
                    point = MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], dist);
                else
                    point = (curve as IHasPointProcessor).PointAtDistance(dist);
                
                EndTicks.Add(new CatchSliderTick(point.x, point.y, BaseHitObject.Offset + timeOffset));
                List<CatchSliderTick> repeatTicks=new List<CatchSliderTick>();
                Ticks.ForEach(tick=>repeatTicks.Add((CatchSliderTick)tick.Clone()));

                
                double normalizedTimeValue = 0d;
                if ((1 & repeatId) != 0)
                {
                    repeatTicks.Reverse();
                    normalizedTimeValue = j.Offset + (Duration / j.RepeatTime);
                }
                else
                {
                    normalizedTimeValue = j.Offset;
                }

                foreach (var tick in repeatTicks)
                {
                    tick.Offset = j.Offset + timeOffset + Math.Abs(tick.Offset - normalizedTimeValue);
                }
                repeatBonusTick.AddRange(repeatTicks);
                repeatId++;
            }
            
            Ticks.AddRange(repeatBonusTick);
            
            
            OsuPixel tmpPoint;
            double distEnd = (1 & j.RepeatTime) * j.Length;
            if (j.CurveType == CurveTypes.Linear)
                tmpPoint = MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], distEnd);
            else
                tmpPoint = (curve as IHasPointProcessor).PointAtDistance(distEnd); 
            
            var endTick = new CatchSliderTick(tmpPoint.x, tmpPoint.y, Offset + Duration);
            EndTicks.Add(endTick);
            
            
            
        }
        public int GetCombo()
        {
            int val = 1;
            if (BaseHitObject.HitObjectType == HitObjectTypes.JuiceStream)
            {
                val += Ticks.Count;
                val += (BaseHitObject as JuiceStream).RepeatTime;
            }
            if (BaseHitObject.HitObjectType == HitObjectTypes.Slider)
            {
                val += Ticks.Count;
                val += (BaseHitObject as Slider).RepeatTime;
            }
            return val;
        }
        
    }
}