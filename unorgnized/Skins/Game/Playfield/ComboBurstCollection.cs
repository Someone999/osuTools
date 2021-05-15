using System.Collections.Generic;

namespace osuTools.Skins.SkinObjects.Generic.PlayField
{
    /// <summary>
    ///     各模式的达到指定连击时会显示的图片
    /// </summary>
    public class ComboBurstCollection
    {
        public List<GenericSkinImage> ManiaComboBurstImages { get; internal set; }
        public List<GenericSkinImage> CatchComboBurstImages { get; internal set; }
        public List<GenericSkinImage> OsuComboBurstImages { get; internal set; }
    }
}