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
    }
}