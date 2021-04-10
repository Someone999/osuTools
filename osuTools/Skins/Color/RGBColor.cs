namespace osuTools.Skins.Colors
{
    /// <summary>
    ///     RGB颜色
    /// </summary>
    public class RGBColor
    {
        /// <summary>
        ///     构造一个rgb均为0的RGB颜色
        /// </summary>
        public RGBColor()
        {
            R = 0;
            B = 0;
            G = 0;
        }

        /// <summary>
        ///     使用指定的rgb构造一个颜色
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public RGBColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        ///     红色部分
        /// </summary>
        public int R { get; }

        /// <summary>
        ///     绿色部分
        /// </summary>
        public int G { get; }

        /// <summary>
        ///     蓝色部分
        /// </summary>
        public int B { get; }

        private static bool isdig(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        ///     将字符串转换成RGBColor
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RGBColor Parse(string s)
        {
            var spliter = (char) 0;
            foreach (var ch in s)
                if (!isdig(ch) && ch != ' ')
                {
                    spliter = ch;
                    break;
                }

            var vals = s.Split(spliter);
            var c = new RGBColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
            return c;
        }

        public static bool operator ==(RGBColor a, RGBColor b)
        {
            return a.R == b.R && a.B == b.B && a.G == b.G;
        }

        public static bool operator !=(RGBColor a, RGBColor b)
        {
            return a.R != b.R || a.B != b.B || a.G == b.G;
        }

        /// <summary>
        ///     获取RGBColor的Hash，返回R*B*G
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return R * B * G;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}