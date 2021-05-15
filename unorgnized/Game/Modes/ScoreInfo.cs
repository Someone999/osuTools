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
        public int CountGeki { get; set; }

        /// <summary>
        ///     300的数量
        /// </summary>
        public int Count300 { get; set; }

        /// <summary>
        ///     200的数量
        /// </summary>
        public int CountKatu { get; set; }

        /// <summary>
        ///     100的数量
        /// </summary>
        public int Count100 { get; set; }

        /// <summary>
        ///     50的数量
        /// </summary>
        public int Count50 { get; set; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        public int CountMiss { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is ScoreInfo info)
            {
                return info.CountGeki == this.CountGeki && info.Count300 == this.Count300 && info.CountKatu == this.CountKatu && info.Count100 == this.Count100 && info.Count50 == this.Count50 &&
                       info.CountMiss == this.CountMiss;
            }
            return obj.Equals(this);
        }

        public override int GetHashCode()
        {
            return CountGeki * 6 + Count300 * 5 + CountKatu * 4 + Count100 * 3 + Count50 * 2 + CountMiss;
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