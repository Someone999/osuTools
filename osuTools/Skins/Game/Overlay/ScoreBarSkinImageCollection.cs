using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Generic
{
    public class ScoreBarSkinImageCollection
    {
        /// <summary>
        ///     血条的外框
        /// </summary>
        public GenericSkinImage ScoreBarBackgorund { get; internal set; }

        /// <summary>
        ///     血条的背景
        /// </summary>
        public List<GenericSkinImage> ScoreBarColour { get; internal set; } = new List<GenericSkinImage>();

        /// <summary>
        ///     血量在50%以上的时候血条上显示的图片
        /// </summary>
        public GenericSkinImage ScoreBarKi { get; internal set; }

        /// <summary>
        ///     血量在15%-50%的时候血条上显示的图片
        /// </summary>
        public GenericSkinImage ScoreBarKiDanger { get; internal set; }

        /// <summary>
        ///     血量在15%以下的时候血条上显示的图片
        /// </summary>
        public GenericSkinImage ScoreBarKiCritical { get; internal set; }

        /// <summary>
        ///     血条上的标签
        /// </summary>
        public GenericSkinImage ScoreBarMarker { get; internal set; }
    }
}