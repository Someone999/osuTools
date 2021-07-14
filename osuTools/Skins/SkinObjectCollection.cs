using osuTools.Skins.Catch;
using osuTools.Skins.Game;
using osuTools.Skins.Mania;
using osuTools.Skins.Mods;
using osuTools.Skins.Osu;
using osuTools.Skins.Taiko;

namespace osuTools.Skins
{
    /// <summary>
    ///     皮肤元素的集合
    /// </summary>
    public class SkinObjectCollection
    {
        /// <summary>
        ///     通用皮肤元素
        /// </summary>
        public GeneralSkinObjectCollection GeneralSkinObjects { get; internal set; } =
            new GeneralSkinObjectCollection();

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

    public partial class Skin
    {
        /// <summary>
        ///     包含所有的皮肤元素
        /// </summary>
        public SkinObjectCollection SkinObjects { get; internal set; } = new SkinObjectCollection();
    }
}