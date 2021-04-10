using System;

namespace osuTools.Skins.Colors
{
    /// <summary>
    ///     连击颜色的集合
    /// </summary>
    public class ComboColorCollection
    {
        /// <summary>
        ///     上一个连击的颜色
        /// </summary>
        public ComboColor LastCombo { get; private set; } = new ComboColor(255, 192, 0);

        /// <summary>
        ///     第一个连击的颜色
        /// </summary>
        public ComboColor FirstCombo { get; private set; } = new ComboColor(0, 202, 0);

        /// <summary>
        ///     第二个连击的颜色
        /// </summary>
        public ComboColor SecondCombo { get; private set; } = new ComboColor(181, 24, 255);

        /// <summary>
        ///     第三个连击的颜色
        /// </summary>
        public ComboColor ThirdCombo { get; private set; } = new ComboColor(242, 24, 57);

        /// <summary>
        ///     第四个连击的颜色
        /// </summary>
        public ComboColor FourthCombo { get; private set; }

        /// <summary>
        ///     第五个连击的颜色
        /// </summary>
        public ComboColor FifthCombo { get; private set; }

        /// <summary>
        ///     第六个连击的颜色
        /// </summary>
        public ComboColor SixthCombo { get; private set; }

        /// <summary>
        ///     第七个连击的颜色
        /// </summary>
        public ComboColor SeventhCombo { get; private set; }

        internal void setColor(ComboNumber comboNum, ComboColor color)
        {
            switch (comboNum)
            {
                case ComboNumber.Last:
                    LastCombo = color;
                    break;
                case ComboNumber.First:
                    FirstCombo = color;
                    break;
                case ComboNumber.Second:
                    SecondCombo = color;
                    break;
                case ComboNumber.Third:
                    ThirdCombo = color;
                    break;
                case ComboNumber.Fourth:
                    FourthCombo = color;
                    break;
                case ComboNumber.Fifth:
                    FifthCombo = color;
                    break;
                case ComboNumber.Sixth:
                    SixthCombo = color;
                    break;
                case ComboNumber.Seventh:
                    SeventhCombo = color;
                    break;
                default: throw new ArgumentException("输入的序号错误。");
            }
        }
    }
}