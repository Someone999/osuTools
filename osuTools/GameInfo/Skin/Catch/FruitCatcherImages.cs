using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Catch
{
    /// <summary>
    ///     水果容器元素的图片
    /// </summary>
    public class FruitCatcherImages
    {
        /// <summary>
        ///     漏掉水果的时候
        /// </summary>
        public List<CatchSkinImage> Fail { get; internal set; } = new List<CatchSkinImage>();

        /// <summary>
        ///     空闲的时候
        /// </summary>
        public List<CatchSkinImage> Idle { get; internal set; } = new List<CatchSkinImage>();

        /// <summary>
        ///     KiaiTime的时候
        /// </summary>

        public List<CatchSkinImage> Kiai { get; internal set; } = new List<CatchSkinImage>();
    }
}