using System;
using System.Collections.Generic;
using osuTools.Beatmaps.HitObject;
using osuTools.Beatmaps.HitObject.Catch;
using osuTools.Beatmaps.HitObject.Std;
using osuTools.Collections;
using osuTools.Game.Modes;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 用于<seealso cref="CatchBeatmap"/>pp计算的HitObject
    /// </summary>
    public class CatchHitObject:ICatchHitObject
    {
        ///<inheritdoc/>
        public double x => BaseHitObject.Position.x;
        ///<inheritdoc/>
        public double y => BaseHitObject.Position.y;
        ///<inheritdoc/>
        public double Offset => BaseHitObject.Offset;
        /// <summary>
        /// 基础HitObject
        /// </summary>
        public IHitObject BaseHitObject { get; }
        /// <summary>
        /// 该CatchHitObject的Tick
        /// </summary>
        public CloneableObservableList<CatchSliderTick> Ticks { get; } = new CloneableObservableList<CatchSliderTick>();
        /// <summary>
        /// 该<seealso cref="CatchHitObject"/>的EndTick
        /// </summary>
        public CloneableObservableList<CatchSliderTick> EndTicks { get; } = new CloneableObservableList<CatchSliderTick>();

        private CloneableObservableList<OsuPixel> Path { get;  set; } = new CloneableObservableList<OsuPixel>();
        /// <summary>
        /// 该<seealso cref="CatchHitObject"/>对应的时间点
        /// </summary>
        public Dictionary<CatchTimePointType, double> TimePoint { get; }
        /// <summary>
        /// 该<seealso cref="CatchHitObject"/>所在的<seealso cref="CatchBeatmap"/>的难度属性
        /// </summary>
        public CatchDifficultyAttribute Difficulty { get; }
        /// <summary>
        /// Tick间的距离
        /// </summary>
        public double TickDistance { get; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public double Duration { get; }
        /// <summary>
        /// 使用指定的参数初始化一个CatchHitObject
        /// </summary>
        /// <param name="hitobject">基础HitObject</param>
        /// <param name="timePoint">时间点</param>
        /// <param name="difficulty">难度参数</param>
        /// <param name="tickDistance">Tick距离</param>
        public CatchHitObject(IHitObject hitobject, Dictionary<CatchTimePointType, double> timePoint = null,CatchDifficultyAttribute difficulty=null,double tickDistance = 1)
        {
            //TickDistance.OnChanged += (oldVal, val) => {if(val != 1) Console.WriteLine($"Value changed {oldVal}=>{val}"); };

            if (hitobject.SpecifiedMode != OsuGameMode.Catch && hitobject.SpecifiedMode != OsuGameMode.Osu) 
                throw new ArgumentException("Not a catch HitObject");
            BaseHitObject = hitobject;
            TimePoint = timePoint;
            TickDistance = tickDistance;
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
                Duration = ((int) TimePoint[CatchTimePointType.RawBpm] *
                            (j.Length / (difficulty.SliderMultiplier * TimePoint[CatchTimePointType.Spm])) /
                            100) *
                           j.RepeatTime;

                j.curvePoints.Insert(0, j.Position);
                CalcSlider();
            }
        }
        internal void CalcSlider(bool calcPath = false)
        {
            dynamic j;

            if (BaseHitObject is JuiceStream stream)
                j = stream;
            else
                j = BaseHitObject as Slider;
            if (j is null) throw new NullReferenceException("转换HitObject失败");
            if ( j.CurveType == CurveTypes.PerfectCircle && j.curvePoints.Count > 3)
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
                catch (Exception)
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
                    while (l < j.Length)
                        Path.Add((curve as Perfect)?.PointAtDistance(l));
                }
                else
                    throw new NotSupportedException("Slidertype not supported!");
            }

            double currentDis = TickDistance;
            double addTime = Duration * (TickDistance / (j.Length * j.RepeatTime));

            while(currentDis< j.Length - TickDistance / 8)
            {
                var point = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], currentDis) :
                    ((IHasPointProcessor)curve)?.PointAtDistance(currentDis)??throw new InvalidOperationException();
                //Console.WriteLine($"Tick?{point.x}?{point.y}?{j.Offset + addTime * (Ticks.Count + 1)}");
                if (!(point is null))
                    Ticks.Add((new CatchSliderTick(point.x, point.y, j.Offset + addTime * (Ticks.Count + 1))));

                currentDis += TickDistance;
            }
            //Console.WriteLine(Ticks.Count.ToString());
            int repeatId = 1;
            List<CatchSliderTick> repeatBonusTick = new List<CatchSliderTick>();
            while (repeatId < j.RepeatTime)
            {
                double dist = (1 & repeatId) * j.Length;
                double timeOffset = (Duration / j.RepeatTime) * repeatId;
                var point = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], dist) :
                    ((IHasPointProcessor)curve)?.PointAtDistance(dist)??throw new InvalidOperationException();
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

            double distEnd = (1 & j.RepeatTime) * j.Length;
            var tmpPoint = j.CurveType == CurveTypes.Linear ? (OsuPixel) MathUtlity.PointOnLine(j.curvePoints[0], j.curvePoints[1], distEnd) : 
                ((IHasPointProcessor) curve)?.PointAtDistance(distEnd) ?? throw new InvalidOperationException(); 

            var endTick = new CatchSliderTick(tmpPoint.x, tmpPoint.y, Offset + Duration);
            EndTicks.Add(endTick);
            //Ticks.ForEach(tick=>Console.WriteLine($"Tick?{tick.Offset}?{tick.x}?{tick.y}"));
            //EndTicks.ForEach(tick => Console.WriteLine($"EndTick?{tick.Offset}?{tick.x}?{tick.y}"));

        }
        /// <summary>
        /// 获取这个<seealso cref="CatchHitObject"/>可以增加的连击数
        /// </summary>
        /// <returns></returns>
        public int GetCombo()
        {
            int val = 1;
            if (BaseHitObject.HitObjectType == HitObjectTypes.JuiceStream)
            {
                val += Ticks.Count;
                val += ((JuiceStream) BaseHitObject).RepeatTime;
            }
            if (BaseHitObject.HitObjectType == HitObjectTypes.Slider)
            {
                val += Ticks.Count;
                val += ((Slider) BaseHitObject).RepeatTime;
            }
            return val;
        }

    }
}