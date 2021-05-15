using osuTools.Game.Modes;

namespace osuTools.Skins.Game.Rank
{
    /// <summary>
    ///     评级的图片
    /// </summary>
    public class RankingImageCollection
    {
        /// <summary>
        ///     准确度100%时评价的图片
        /// </summary>
        public RankingImage SS { get; internal set; }

        /// <summary>
        ///     准确度100%并开启Hidden或Flashlight或FadeIn时的评价的图片
        /// </summary>
        public RankingImage SSH { get; internal set; }

        /// <summary>
        ///     达到S时的评价的图片。此判定的标准详见<see cref="GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage S { get; internal set; }

        /// <summary>
        ///     在S的基础上开启Hidden或Flashlight或FadeIn时的评价的图片
        /// </summary>
        public RankingImage SH { get; internal set; }

        /// <summary>
        ///     达到A时的评价的图片。此判定的标准详见<see cref="GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage A { get; internal set; }

        /// <summary>
        ///     达到B时的评价的图片。此判定的标准详见<see cref="GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage B { get; internal set; }

        /// <summary>
        ///     达到C时的评价的图片。此判定的标准详见<see cref="GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage C { get; internal set; }

        /// <summary>
        ///     达到D时的评价的图片。此判定的标准详见<see cref="GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage D { get; internal set; }
    }
}