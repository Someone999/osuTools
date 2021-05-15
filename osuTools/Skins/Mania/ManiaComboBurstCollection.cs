using System.Collections.Generic;

namespace osuTools.Skins.Mania
{
    /// <summary>
    ///     Mania模式的连击提示图
    /// </summary>
    public class ManiaComboBurstCollection
    {
        /// <summary>
        /// 连击提示图
        /// </summary>
        public List<ManiaSkinImage> ComboBurstImages { get; internal set; } = new List<ManiaSkinImage>();
    }
}