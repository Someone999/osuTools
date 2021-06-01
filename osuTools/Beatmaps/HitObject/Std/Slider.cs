using System;
using System.Collections.Generic;
using System.Text;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Std
{
    /// <summary>
    ///     表示一个滑条
    /// </summary>
    public class Slider : IHitObject, INoteGrouped
    {
        private string curvetype;
        private int type;

        /// <summary>
        ///     点到滑条开始的位置播放的音效以及自定义的数据
        /// </summary>
        public SliderHitSound StartingHitSound { get; set; } = new SliderHitSound();

        /// <summary>
        ///     经过小豆豆时播放的音效以及自定义的数据
        /// </summary>
        public SliderHitSound DuringHitSound { get; set; } = new SliderHitSound();

        /// <summary>
        ///     滑条结束时播放的音效以及自定义的数据
        /// </summary>
        public SliderHitSound EndingHitSound { get; set; } = new SliderHitSound();

        internal List<OsuPixel> curvePoints { get; set; } = new List<OsuPixel>();

        /// <summary>
        ///     绘制该滑条所需的点
        /// </summary>
        public IReadOnlyList<OsuPixel> CurvePoints => curvePoints.AsReadOnly();

        /// <summary>
        ///     滑条的算法
        /// </summary>
        public CurveTypes CurveType { get; set; }

        /// <summary>
        ///     需要在滑条上走的次数
        /// </summary>
        public int RepeatTime { get; set; }

        /// <summary>
        ///     滑条的长度
        /// </summary>
        public double Length { get; set; }
        /// <inheritdoc />

        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.Slider;

        /// <summary>
        ///     该Slider相对于曲目开始的时间
        /// </summary>
        public int Offset { get; set; } = -1;

        /// <summary>
        ///     滑条在未指定StartingHitSound等时使用的音效
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     滑条在未指定StartingHitSound等时使用的自定义音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     滑条的起始端点
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     滑条出现的特定模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Osu;

        /// <summary>
        ///     使用格式正确的字符串构造一个Slider
        /// </summary>
        /// <param name="data">要使用的字符串</param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            var val = double.Parse(info[2]);
            Offset = double.IsNaN(val) || double.IsInfinity(val) ? 0 : (int) val;
            type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
            if (!types.Contains(HitObjectTypes.Slider))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            if (types.Contains(HitObjectTypes.NewCombo))
                IsNewGroup = true;
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
            var sliderinfo = info[5];
            var typeAndPoint = sliderinfo.Split('|');
            curvetype = typeAndPoint[0];
            CurveType = GetCurveTypeByString(curvetype);
            for (var i = 1; i < typeAndPoint.Length; i++) //这个for循环解析这个滑条的所有的定位点
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
                    hitSounds.Add(HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(str))[0]);
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

                if (info.Length > 10)
                    HitSample = new HitSample(info[10]);
            }
        }

        /// <summary>
        ///     获取osu!格式的数据
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            var b = new StringBuilder(
                $"{Position.x},{Position.y},{Offset},{1 << (int) HitObjectType},{1 << (int) HitSound},{curvetype}");
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
        /// <inheritdoc/>
        public bool IsNewGroup { get; set; }

        internal static CurveTypes GetCurveTypeByString(string str)
        {
            if (str == "B") return CurveTypes.Bezier;
            if (str == "C") return CurveTypes.CentripetalCatmullRom;
            if (str == "L") return CurveTypes.Linear;
            if (str == "P") return CurveTypes.PerfectCircle;
            return CurveTypes.Unknown;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}