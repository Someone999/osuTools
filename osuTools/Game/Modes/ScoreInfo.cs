using osuTools.Game.Mods;

namespace osuTools.Game
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
        /// 开启的Mod
        /// </summary>
        public ModList Mods { get; set; }

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
        /// <summary>
        /// 玩家达到的最大连击
        /// </summary>
        public int PlayerMaxCombo { get; set; }
        /// <summary>
        /// 最大连击
        /// </summary>
        public int MaxCombo { get; set; }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is ScoreInfo info)
            {
                return info.CountGeki == CountGeki && info.Count300 == Count300 && info.CountKatu == CountKatu && info.Count100 == Count100 && info.Count50 == Count50 &&
                       info.CountMiss == CountMiss;
            }
            return obj.Equals(this);
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return CountGeki * 6 + Count300 * 5 + CountKatu * 4 + Count100 * 3 + Count50 * 2 + CountMiss;
        }
        /// <summary>
        /// 判断两个ScoreInfo是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ScoreInfo a, ScoreInfo b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.GetHashCode() == b.GetHashCode() && a.Equals(b);
        }
        /// <summary>
        /// 判断两个ScoreInfo是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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