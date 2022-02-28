using osuTools.Beatmaps.HitObject;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;
using System;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 代表一个小果粒
    /// </summary>
    public class CatchSliderTick:ICatchHitObject,ICloneable
    {

        /// <summary>
        /// x坐标
        /// </summary>
        public double x { get; internal set; }
        /// <summary>
        /// y坐标
        /// </summary>
        public double y { get; internal set; }
        /// <summary>
        /// 时间偏移
        /// </summary>
        public double Offset { get; internal set; }

        public HitObjectTypes HitObjectType => HitObjectTypes.CatchSliderTick;

        double IHitObject.Offset { get => Offset; set => throw new NotImplementedException(); }

        public HitSample HitSample => null;

        public OsuGameMode SpecifiedMode => OsuGameMode.Catch;

        public HitSounds HitSound => HitSounds.Normal;
        OsuPixel _pixel;
        public OsuPixel Position => _pixel;

        /// <summary>
        /// 使用x,y,时间偏移初始化一个CatchSliderTick
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="offset"></param>
        public CatchSliderTick(double x, double y, double offset)
        {
            this.x = x;
            this.y = y;
            _pixel = new OsuPixel(x, y);
            Offset = offset;
        }
        /// <summary>
        /// 克隆该对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new CatchSliderTick(x, y, Offset);
        }

        public void Parse(string data)
        {
            throw new NotSupportedException("This is not a part of file.");
        }

        public string ToOsuFormat()
        {
            throw new NotSupportedException("This is not a part of file.");
        }
    }
}