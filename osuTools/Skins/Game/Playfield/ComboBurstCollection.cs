using System.Collections.Generic;

namespace osuTools.Skins.Game.Playfield
{
    /// <summary>
    ///     各模式的达到指定连击时会显示的图片
    /// </summary>
    public class ComboBurstCollection
    {
        /// <summary>
        ///     Mania模式的达到指定连击时会显示的图片
        /// </summary>
        public List<GeneralSkinImage> ManiaComboBurstImages { get; internal set; }
        /// <summary>
        ///     Catch模式的达到指定连击时会显示的图片
        /// </summary>
        public List<GeneralSkinImage> CatchComboBurstImages { get; internal set; }

        /// <summary>
        ///     Std模式的达到指定连击时会显示的图片
        /// </summary>
        public List<GeneralSkinImage> OsuComboBurstImages { get; internal set; }
    }
}