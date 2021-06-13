using System;
using System.Collections.Generic;
using System.Globalization;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Skins;

namespace osuTools.Beatmaps.TimePoint
{
    /// <summary>
    ///     表示一个时间点
    /// </summary>
    public class TimePoint : IOsuFileContent, IEqualityComparer<TimePoint>
    {
        private readonly int _effect;

        /// <summary>
        ///     通过正确的字符串构造一个TimePoint对象
        /// </summary>
        /// <param name="line">指定的字符串</param>
        public TimePoint(string line)
        {
            var data = line.Split(',');
            Offset = double.Parse(data[0]);
            BeatLength = double.Parse(data[1]);
            Meter = double.Parse(data[2]);
            var b = int.TryParse(data[3], out var sample);
            SampleSet = b ? (SampleSets) sample : SkinTools.StringToEnum<SampleSets>(data[3]);
            SampleIndex = int.Parse(data[4]);
            Volume = double.Parse(data[5]);
            Uninherited = int.Parse(data[6]).ToBool();
            if (!Uninherited)
            {
                var speed = 100 / (BeatLength / 100);
                SliderVelocity *= speed * -1 > 0 ? Math.Abs(speed) : 0;
            }

            _effect = int.Parse(data[7]);
            Bpm = double.Parse((1 / BeatLength * 1000 * 60).ToString(CultureInfo.InvariantCulture));
            Bitprocesser(_effect);
        }

        /// <summary>
        ///     该时间点相对于歌曲开始的时间
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        ///     如果该时间点不是继承的，则为BPM，如果是继承的则为0
        /// </summary>
        public double Bpm { get; }

        /// <summary>
        ///     一个节拍所用的时间，以毫秒为单位
        /// </summary>
        public double BeatLength { get; }

        /// <summary>
        ///     一次测量的节拍数
        /// </summary>
        public double Meter { get; }

        /// <summary>
        ///     指定的音效的类型
        /// </summary>
        public SampleSets SampleSet { get; }

        /// <summary>
        ///     音效的编号
        /// </summary>
        public int SampleIndex { get; }

        /// <summary>
        ///     指定音效的音量
        /// </summary>
        public double Volume { get; }

        /// <summary>
        ///     时间点是否为继承
        /// </summary>
        public bool Uninherited { get; }

        /// <summary>
        ///     是否开始一个KiaiTime
        /// </summary>
        public bool KiaiTime { get; private set; }

        /// <summary>
        ///     是否省略Mania或Taiko的第一条小节线
        /// </summary>
        public bool OmitFirstBarline { get; private set; }

        /// <summary>
        ///     滑条速度，单位为百分比
        /// </summary>
        public double SliderVelocity { get; } = -1;

        /// <summary>
        ///     获取TimePoint对象的Hash，返回Offset + BeatLength * Meter
        /// </summary>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public int GetHashCode(TimePoint timePoint)
        {
            return (int) (Offset + BeatLength * Meter);
        }

        /// <summary>
        ///     比较两个TimePoint是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(TimePoint a, TimePoint b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.GetHashCode(a) == b.GetHashCode(b);
        }

        /// <inheritdoc />
        public string ToOsuFormat()
        {
            return
                $"{Offset},{BeatLength},{Meter},{(int) SampleSet},{SampleIndex},{Volume},{(Uninherited ? 1 : 0)},{_effect}";
        }

        private void Bitprocesser(int num)
        {
            var cur = num;
            if (cur == 0) return;
            while (cur > 0)
            {
                var log2Int = (int) Math.Truncate(Math.Log(cur, 2));
                var value = log2Int;
                if (value == 0) KiaiTime = true;
                if (value == 3) OmitFirstBarline = true;
                cur -= (int) Math.Pow(2, log2Int);
            }
        }

        /// <summary>
        ///     返回TimePoint的部分信息。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                $"Offset:{Offset} BPM:{Bpm} BeatLength:{BeatLength} Uninherited:{Uninherited} KiaiTime:{KiaiTime} OmitFirstBarline:{OmitFirstBarline}";
        }
    }
}