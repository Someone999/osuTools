﻿using System;
using System.Collections.Generic;
using System.Text;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Beatmaps.HitObject.Std;
using osuTools.Beatmaps.HitObject.Tools;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Taiko
{
    /// <summary>
    ///     表示一个Taiko的连打
    /// </summary>
    public class DrumRoll : IHasEndHitObject
    {
        private string _curvetype;
        private int _type;

        /// <summary>
        ///     连打物件的类型
        /// </summary>
        public DrumRollTypes DrumRollType { get; set; }
        /// <summary>
        /// <inheritdoc cref="Slider.StartingHitSound"/>
        /// </summary>
        public SliderHitSound StartingHitSound { get; set; } = new SliderHitSound();
        /// <summary>
        /// <inheritdoc cref="Slider.DuringHitSound"/>
        /// </summary>
        public SliderHitSound DuringHitSound { get; set; } = new SliderHitSound();
        /// <summary>
        /// <inheritdoc cref="Slider.EndingHitSound"/>
        /// </summary>
        public SliderHitSound EndingHitSound { get; set; } = new SliderHitSound();
        private List<OsuPixel> curvePoints { get; } = new List<OsuPixel>();
        /// <summary>
        /// <inheritdoc cref="Slider.CurvePoints"/>
        /// </summary>
        public IReadOnlyList<OsuPixel> CurvePoints => curvePoints.AsReadOnly();
        /// <summary>
        /// <inheritdoc cref="Slider.CurveType"/>
        /// </summary>
        public CurveTypes CurveType { get; set; }
        /// <summary>
        /// <inheritdoc cref="Slider.RepeatTime"/>
        /// </summary>
        public int RepeatTime { get; set; }
        private double Length { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public double EndTime { get; private set; }

        /// <summary>
        ///     打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.DrumRoll;

        /// <summary>
        ///     打击物件相对于开始的偏移
        /// </summary>
        public double Offset { get; set; } = -1;

        /// <summary>
        ///     音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     会出现该打击物件的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Taiko;

        /// <summary>
        ///     此属性对Drumroll无效
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     将字符串解析为Drumroll
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data) //(x,y)_,time,type,hitSound,endTime,hitSample
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            _type = int.Parse(info[3]);
            var types = new HitObjectTypesConverter().Convert(_type, out var maybeBestVal);
            if (maybeBestVal != HitObjectTypes.Slider && maybeBestVal != HitObjectTypes.Spinner)
                throw new ArgumentException("该行的数据不适用。");

            if (maybeBestVal == HitObjectTypes.Spinner)
            {
                DrumRollType = DrumRollTypes.Spinner;
                if (!types.Contains(HitObjectTypes.Spinner))
                    throw new ArgumentException("该行的数据不适用。");

                HitSound = new HitSoundsConverter().Convert(int.Parse(info[4]),out _)[0];
                var eval = double.Parse(info[5]);
                EndTime = double.IsNaN(eval) || double.IsInfinity(eval) ? 0 : (int) eval;
                if (info.Length > 6)
                    HitSample = new HitSample(info[6]);
            }

            if (maybeBestVal == HitObjectTypes.Slider)
            {
                DrumRollType = DrumRollTypes.Slider;
                if (!types.Contains(HitObjectTypes.Slider))
                {
                    throw new ArgumentException("该行的数据不适用。");
                }

                HitSound = new HitSoundsConverter().Convert(int.Parse(info[4]),out _)[0];
                var sliderinfo = info[5];
                var typeAndPoint = sliderinfo.Split('|');
                _curvetype = typeAndPoint[0];
                CurveType = Slider.GetCurveTypeByString(_curvetype);
                for (var i = 1; i < typeAndPoint.Length; i++)
                {
                    var point = typeAndPoint[i].Split(':');
                    if (point.Length == 2)
                    {
                        var x = int.Parse(point[0]);
                        var y = int.Parse(point[1]);
                        curvePoints.Add(new OsuPixel(x, y));
                    }
                }

                RepeatTime = int.Parse(info[6]);
                Length = double.Parse(info[7]);
                if (info.Length > 8)
                {
                    var sampleSets = new List<SampleSets>();
                    var additionSampleSets = new List<SampleSets>();
                    var hitSounds = new List<HitSounds>();
                    var hitSoundstrs = info[8].Split('|');
                    foreach (var str in hitSoundstrs)
                        hitSounds.Add(new HitSoundsConverter().Convert(int.Parse(str),out _)[0]);
                    if (hitSoundstrs.Length > 0)
                        StartingHitSound = new SliderHitSound(hitSounds[0]);
                    if (hitSoundstrs.Length > 1)
                        DuringHitSound = new SliderHitSound(hitSounds[1]);
                    if (hitSoundstrs.Length > 2)
                        EndingHitSound = new SliderHitSound(hitSounds[2]);
                    if (info.Length > 9)
                    {
                        var sampleSetstrs = info[9].Split('|');
                        foreach (var sampleSetstr in sampleSetstrs)
                        {
                            var samples = sampleSetstr.Split(':');
                            var sampleSet = int.Parse(samples[0]);
                            var addionSampleSet = int.Parse(samples[1]);
                            sampleSets.Add((SampleSets) sampleSet);
                            additionSampleSets.Add((SampleSets) addionSampleSet);
                        }

                        if (sampleSets.Count > 1)
                            StartingHitSound = new SliderHitSound(hitSounds[0],
                                new EdgeSound(sampleSets[0], additionSampleSets[0]));
                        if (sampleSets.Count > 2)
                            DuringHitSound = new SliderHitSound(hitSounds[1],
                                new EdgeSound(sampleSets[1], additionSampleSets[1]));
                        if (sampleSets.Count > 3)
                            EndingHitSound = new SliderHitSound(hitSounds[2],
                                new EdgeSound(sampleSets[2], additionSampleSets[2]));
                    }
                }
            }
        }

        /// <summary>
        ///     返回一个以osu文件格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            if (DrumRollType == DrumRollTypes.Slider)
            {
                var b = new StringBuilder($"{Position.x},{Position.y},{Offset},{2},{_curvetype}");
                for (var i = 0; i < curvePoints.Count; i++)
                {
                    if (curvePoints.Count == 1)
                    {
                        b.Append("|" + curvePoints[i].GetData() + ",");
                        break;
                    }

                    if (i == curvePoints.Count - 1)
                        b.Append("|" + curvePoints[i].GetData() + ",");
                    else
                        b.Append($"|{curvePoints[i].GetData()}");
                }

                b.Append(
                    $"{RepeatTime},{Length},{1 << (int) StartingHitSound.HitSound}|{1 << (int) DuringHitSound.HitSound}|{1 << (int) EndingHitSound.HitSound},");
                b.Append(
                    $"{StartingHitSound.Sound.GetData()}|{DuringHitSound.Sound.GetData()}|{EndingHitSound.Sound.GetData()},");
                b.Append($"{HitSample.GetData()}");
                return b.ToString();
            }

            return $"256,192,{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{EndTime},{HitSample.GetData()}";
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}