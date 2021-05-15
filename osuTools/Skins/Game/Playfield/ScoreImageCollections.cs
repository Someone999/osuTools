using System.Collections.Generic;

namespace osuTools.Skins.Game.Playfield
{
    /// <summary>
    ///     分数相关图片的集合
    /// </summary>
    public class ScoreImageCollections
    {
        /// <summary>
        ///     各个数字的图片
        /// </summary>
        public List<GeneralSkinImage> ScoreNumbers { get; internal set; } = new List<GeneralSkinImage>();

        /// <summary>
        ///     逗号
        /// </summary>
        public GeneralSkinImage Coma { get; internal set; }

        /// <summary>
        ///     小数点
        /// </summary>
        public GeneralSkinImage Dot { get; internal set; }

        /// <summary>
        ///     百分号
        /// </summary>
        public GeneralSkinImage Percent { get; internal set; }

        /// <summary>
        ///     连击指示器
        /// </summary>
        public GeneralSkinImage x { get; internal set; }
    }
}