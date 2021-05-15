using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Collections;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CatchHitObject:ICatchHitObject
    {
        public double x { get=>BaseHitObject.Position.x; }
        public double y { get => BaseHitObject.Position.y; }
        public double Offset { get => BaseHitObject.Offset; }
        public IHitObject BaseHitObject { get; }
        public CloneableObservableList<CatchSliderTick> Ticks { get; } = new CloneableObservableList<CatchSliderTick>();
        public CloneableObservableList<CatchSliderTick> EndTicks { get; } = new CloneableObservableList<CatchSliderTick>();
        public CloneableObservableList<OsuPixel> Path { get; internal set; } = new CloneableObservableList<OsuPixel>();
        public System.Collections.Generic.Dictionary<CatchTimePointType, double> TimePoint { get; } = null;
        public CatchDifficultyAttribute Difficulty { get; } = null;
        public ValueObserver<double> TickDistance { get; } = new ValueObserver<double>();

        public double Duration { get; }

        public CatchHitObject(IHitObject hitobject, System.Collections.Generic.Dictionary<CatchTimePointType, double> timePoint = null,CatchDifficultyAttribute difficulty=null,double tickDistance = 1)
        {
            //TickDistance.OnChanged += (oldVal, val) => {if(val != 1) Console.WriteLine($"Value changed {oldVal}=>{val}"); };

            if (hitobject.SpecifiedMode != OsuGameMode.Catch && hitobject.SpecifiedMode != OsuGameMode.Osu) 
                throw new ArgumentException("Not a catch HitObject");
            BaseHitObject = hitobject;
            TimePoint = timePoint;
            TickDistance.Value = tickDistance;
            Difficulty = difficulty;

            if (BaseHitObject.HitObjectType == HitObjectTypes.Slider ||
                BaseHitObject.HitObjectType == HitObjectTypes.JuiceStream)
            {
                if (timePoint is null || difficulty is null)
                    throw new ArgumentNullException();
                dynamic j;
                if (BaseHitObject is JuiceStream stream)
                    j = stream;
                else
                    j = BaseHitObject as Slider;
                if (j is null)
                    throw new ArgumentException();
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
            dynamic j;

            if (BaseHitObject is JuiceStream)
                j = BaseHitObject as JuiceStream;
            else
                j = BaseHitObject as Slider;

            if (j.CurveType == CurveTypes.PerfectCircle && j.curvePoints.Count > 3)
                j.CurveType = CurveTypes.Bezier;
            else if (j.curvePoints.Count == 2)
            {
                j.CurveType = CurveTypes.Linear;
                //Console.WriteLine("Converted to Linear");
            }

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

            //string s = curve == null ? "NoneType" : $"{curve.}";
            //Console.WriteLine(s);
            if (calcPath)
            {
                if (j.CurveType == CurveTypes.Linear)
                {
                    Path = new CloneableObservableList<OsuPixel>(new Linear(j.curvePoints).Position) ;
                }

                if (j.CurveType == CurveTypes.PerfectCircle)
                {
                    Path = new CloneableObservableList<OsuPixel>();
                    var l = 0;
                    var step = 5;
                    while (l < j.Length)
                        Path.Add((curve as Perfect).PointAtDistance(l));
                }
                else
                    throw new NotSupportedException("Slidertype not supported!");
            }

            double currentDis = TickDistance.Value;
            double addTime = Duration * (TickDistance / (j.Length * j.RepeatTime));

            while(currentDis< j.Length - TickDistance / 8)
            {
                OsuPixel point;
                point = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], currentDis) : (curve as IHasPointProcessor).PointAtDistance(currentDis);
                //Console.WriteLine($"Tick?{point.x}?{point.y}?{j.Offset + addTime * (Ticks.Count + 1)}");
                Ticks.Add((new CatchSliderTick(point.x, point.y, j.Offset + addTime * (Ticks.Count + 1))));

                currentDis += TickDistance.Value;
            }
            //Console.WriteLine(Ticks.Count.ToString());
            int repeatId = 1;
            List<CatchSliderTick> repeatBonusTick = new List<CatchSliderTick>();
            while (repeatId < j.RepeatTime)
            {
                OsuPixel point;
                double dist = (1 & repeatId) * j.Length;
                double timeOffset = (Duration / j.RepeatTime) * repeatId;
                point = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], dist) : (curve as IHasPointProcessor).PointAtDistance(dist);
                //Console.WriteLine($"{Offset}?{point.x}?{point.y}");
                //Console.WriteLine($"EndTick?{point.x}?{point.y}?{BaseHitObject.Offset + timeOffset}");
                EndTicks.Add(new CatchSliderTick(point.x, point.y, BaseHitObject.Offset + timeOffset));

                var repeatTicks = (CloneableList<CatchSliderTick>)Ticks.Clone();

                double normalizedTimeValue;
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
            tmpPoint = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], distEnd) : (curve as IHasPointProcessor).PointAtDistance(distEnd); 

            var endTick = new CatchSliderTick(tmpPoint.x, tmpPoint.y, Offset + Duration);
            EndTicks.Add(endTick);
            //Ticks.ForEach(tick=>Console.WriteLine($"Tick?{tick.Offset}?{tick.x}?{tick.y}"));
            //EndTicks.ForEach(tick => Console.WriteLine($"EndTick?{tick.Offset}?{tick.x}?{tick.y}"));

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