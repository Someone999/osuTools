using System;
using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// <seealso cref="CatchBeatmap"/>的难度属性
    /// </summary>
    public class CatchDifficultyAttribute:IEnumerable<MKeyValuePair<string,double>>
    {
        /// <summary>
        /// 掉落速度
        /// </summary>
        public double ApprochRate { get; set; } = double.NaN;
        /// <summary>
        /// 整体难度
        /// </summary>
        public double OverallDifficulty { get; set; } = double.NaN;
        /// <summary>
        /// 水果大小
        /// </summary>
        public double CircleSize { get; set; } = double.NaN;
        /// <summary>
        /// 掉血/回血的速度
        /// </summary>
        public double HpDrain { get; set; } = double.NaN;
        /// <summary>
        /// 滑条倍率
        /// </summary>
        public double SliderMultiplier { get; set; } = double.NaN;
        /// <summary>
        /// 小豆豆的倍率
        /// </summary>
        public double SliderTickRate { get; set; } = double.NaN;
        /// <summary>
        /// 使用指定的字符串获取相应的难度
        /// </summary>
        /// <param name="s">与难度相关的字符串</param>
        /// <returns>数值</returns>
        public double this[string s]
        {
            get
            {
                if (s.Equals("ApproachRate", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    return ApprochRate;
                if (s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    return OverallDifficulty;
                if (s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    return CircleSize;
                if (s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase))
                    return HpDrain;
                if (s.Equals("SliderMul", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("SliderMultiplier", StringComparison.OrdinalIgnoreCase))
                    return SliderMultiplier;
                if (s.Equals("SliderTickRate", StringComparison.OrdinalIgnoreCase))
                    return SliderTickRate;
                throw new ArgumentException();
            }
            set
            {
                if (s.Equals("ApproachRate", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("ApprochRate", StringComparison.OrdinalIgnoreCase))
                    ApprochRate = value;
                else if (s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("OverallDifficulty", StringComparison.OrdinalIgnoreCase))
                    OverallDifficulty = value;
                else if (s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("CircleSize", StringComparison.OrdinalIgnoreCase))
                    CircleSize = value;
                else if (s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("HPDrain", StringComparison.OrdinalIgnoreCase))
                    HpDrain = value;
                else if (s.Equals("SliderMul", StringComparison.OrdinalIgnoreCase) ||
                         s.Equals("SliderMultiplier", StringComparison.OrdinalIgnoreCase))
                    SliderMultiplier = value;
                else if (s.Equals("SliderTickRate", StringComparison.OrdinalIgnoreCase))
                    SliderTickRate = value;
                else throw new ArgumentException();
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerator<MKeyValuePair<string, double>> GetEnumerator() => new DifficultyEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new DifficultyEnumerator(this);
    }
}