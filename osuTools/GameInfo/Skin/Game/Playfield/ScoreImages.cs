using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Generic.Score
{
    /// <summary>
    ///     分数相关图片的集合
    /// </summary>
    public class ScoreImageCollections
    {
        /// <summary>
        ///     各个数字的图片
        /// </summary>
        public List<GenericSkinImage> ScoreNumbers { get; internal set; } = new List<GenericSkinImage>();

        /// <summary>
        ///     逗号
        /// </summary>
        public GenericSkinImage Coma { get; internal set; }

        /// <summary>
        ///     小数点
        /// </summary>
        public GenericSkinImage Dot { get; internal set; }

        /// <summary>
        ///     百分号
        /// </summary>
        public GenericSkinImage Percent { get; internal set; }

        /// <summary>
        ///     连击指示器
        /// </summary>
        public GenericSkinImage x { get; internal set; }
    }
}