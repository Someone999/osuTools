using System.Collections.Generic;

namespace osuTools.Skins.Game.Overlay
{
    /// <summary>
    /// 与血条相关的皮肤元素
    /// </summary>
    public class ScoreBarSkinImageCollection
    {
        /// <summary>
        ///     血条的外框
        /// </summary>
        public GeneralSkinImage ScoreBarBackgorund { get; internal set; }

        /// <summary>
        ///     血条的背景
        /// </summary>
        public List<GeneralSkinImage> ScoreBarColour { get; internal set; } = new List<GeneralSkinImage>();

        /// <summary>
        ///     血量在50%以上的时候血条上显示的图片
        /// </summary>
        public GeneralSkinImage ScoreBarKi { get; internal set; }

        /// <summary>
        ///     血量在15%-50%的时候血条上显示的图片
        /// </summary>
        public GeneralSkinImage ScoreBarKiDanger { get; internal set; }

        /// <summary>
        ///     血量在15%以下的时候血条上显示的图片
        /// </summary>
        public GeneralSkinImage ScoreBarKiCritical { get; internal set; }

        /// <summary>
        ///     血条上的标签
        /// </summary>
        public GeneralSkinImage ScoreBarMarker { get; internal set; }
    }
}