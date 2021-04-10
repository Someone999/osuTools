namespace osuTools.Game.Modes
{
    /// <summary>
    ///     分数的组成
    /// </summary>
    public class ScoreInfo
    {
        /// <summary>
        ///     300g的数量
        /// </summary>
        public int c300g { get; set; }

        /// <summary>
        ///     300的数量
        /// </summary>
        public int c300 { get; set; }

        /// <summary>
        ///     200的数量
        /// </summary>
        public int c200 { get; set; }

        /// <summary>
        ///     100的数量
        /// </summary>
        public int c100 { get; set; }

        /// <summary>
        ///     50的数量
        /// </summary>
        public int c50 { get; set; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        public int cMiss { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is ScoreInfo info)
            {
                return info.c300g == this.c300g && info.c300 == this.c300 && info.c200 == this.c200 && info.c100 == this.c100 && info.c50 == this.c50 &&
                       info.cMiss == this.cMiss;
            }
            return obj.Equals(this);
        }

        public override int GetHashCode()
        {
            return c300g * 6 + c300 * 5 + c200 * 4 + c100 * 3 + c50 * 2 + cMiss;
        }

        public static bool operator ==(ScoreInfo a, ScoreInfo b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.GetHashCode() == b.GetHashCode() && a.Equals(b);
        }

        public static bool operator !=(ScoreInfo a, ScoreInfo b)
        {

            if (a is null && b is null)
                return false;
            if (a is null || b is null)
                return true;
            return a.GetHashCode() != b.GetHashCode() || !a.Equals(b);
        }
    }
}