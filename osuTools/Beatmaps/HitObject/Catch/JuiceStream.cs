using System;
using System.Collections.Generic;
using System.Text;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Beatmaps.HitObject.Std;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps.HitObject.Catch
{
    /// <summary>
    ///     表示CTB中一个果汁流
    /// </summary>
    public class JuiceStream : IHitObject, INoteGrouped
    {
        private string _curvetype;

        /// <summary>
        ///     接住头部时播放的音效
        /// </summary>
        public SliderHitSound StartingHitSound { get; set; } = new SliderHitSound();

        /// <summary>
        ///     中途播放的音效
        /// </summary>
        public SliderHitSound DuringHitSound { get; set; } = new SliderHitSound();

        /// <summary>
        ///     接住尾部时播放的音效
        /// </summary>
        public SliderHitSound EndingHitSound { get; set; } = new SliderHitSound();

        internal List<OsuPixel> curvePoints { get; set; } = new List<OsuPixel>();

        /// <summary>
        ///     绘制曲线所用到的点
        /// </summary>
        public IReadOnlyList<OsuPixel> CurvePoints => curvePoints.AsReadOnly();

        /// <summary>
        ///     曲线的类型
        /// </summary>
        public CurveTypes CurveType { get; set; }
        /// <summary>
        /// 折返的次数
        /// </summary>
        public int RepeatTime { get; set; }
        /// <summary>
        /// 像素长度
        /// </summary>

        public double Length { get; set; }

        /// <summary>
        ///     打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.JuiceStream;

        /// <summary>
        ///     打击物件的位置
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     打击物件相对于开始的偏移
        /// </summary>
        public int Offset { get; set; } = -1;

        /// <summary>
        ///     音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     音效
        /// </summary>
        public HitSample HitSample { get; set; } = new HitSample();

        /// <summary>
        ///     会出现该打击物件的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;

        /// <summary>
        ///     将字符串解析为JuiceStream对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            Offset = int.Parse(info[2]);
            var type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type,out _);
            if (!types.Contains(HitObjectTypes.Slider))
            {
                throw new ArgumentException("该行的数据不适用。");
            }

            if (types.Contains(HitObjectTypes.NewCombo))
                IsNewGroup = true;
            HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]),out _)[0];
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
        ///     返回一个以osu文件中格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            var b = new StringBuilder($"{Position.x},192,{Offset},{2},{_curvetype}");
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
            b.Append($"{HitSample}");
            return b.ToString();
        }
        /// <inheritdoc />
        public bool IsNewGroup { get; set; }
        /// <inheritdoc />
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}