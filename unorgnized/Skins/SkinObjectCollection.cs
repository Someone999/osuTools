using osuTools.Skin.Mods;
using osuTools.Skins.SkinObjects.Catch;
using osuTools.Skins.SkinObjects.Collections;
using osuTools.Skins.SkinObjects.Generic;
using osuTools.Skins.SkinObjects.Mania;
using osuTools.Skins.SkinObjects.Osu;
using osuTools.Skins.SkinObjects.Taiko;

namespace osuTools.Skins.SkinObjects.Collections
{
    /// <summary>
    ///     皮肤元素的集合
    /// </summary>
    public class SkinObjectCollection
    {
        /// <summary>
        ///     通用皮肤元素
        /// </summary>
        public GenericSkinObjectCollection GenericSkinObjects { get; internal set; } =
            new GenericSkinObjectCollection();

        /// <summary>
        ///     Osu模式的皮肤元素
        /// </summary>
        public OsuSkinImageCollection OsuSkinImages { get; internal set; } = new OsuSkinImageCollection();

        /// <summary>
        ///     Catch(CTB)模式的皮肤元素
        /// </summary>
        public CatchSkinImageCollection CatchSkinImages { get; internal set; } = new CatchSkinImageCollection();

        /// <summary>
        ///     Taiko模式的皮肤元素
        /// </summary>
        public TaikoSkinImageCollection TaikoSkinImages { get; internal set; } = new TaikoSkinImageCollection();

        /// <summary>
        ///     Mania模式的判定的图片
        /// </summary>
        public ManiaHitBurstImageCollection ManiaHitBurstImages { get; internal set; } =
            new ManiaHitBurstImageCollection();

        /// <summary>
        ///     Mania模式到达指定连击后显示的图片
        /// </summary>
        public ManiaComboBurstCollection ManiaComboBurstImages { get; internal set; } = new ManiaComboBurstCollection();

        /// <summary>
        ///     所有Mod的图片
        /// </summary>
        public ModImageCollection ModImages { get; internal set; } = new ModImageCollection();
    }
}

namespace osuTools.Skins
{
    public partial class Skin
    {
        /// <summary>
        ///     包含所有的皮肤元素
        /// </summary>
        public SkinObjectCollection SkinObjects { get; internal set; } = new SkinObjectCollection();
    }
}