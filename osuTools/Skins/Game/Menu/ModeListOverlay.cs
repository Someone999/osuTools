namespace osuTools.Skins.Game.Menu
{
    /// <summary>
    ///     模式列表中，各个模式的背景图片
    /// </summary>
    public class ModeListOverlay
    {
        /// <summary>
        ///     模式列表中，Std模式的背景图片
        /// </summary>
        public GeneralSkinImage Osu { get; internal set; }
        /// <summary>
        ///     模式列表中，Taiko模式的背景图片
        /// </summary>
        public GeneralSkinImage Taiko { get; internal set; }
        /// <summary>
        ///     模式列表中，Catch模式的背景图片
        /// </summary>
        public GeneralSkinImage Catch { get; internal set; }
        /// <summary>
        ///     模式列表中，Mania模式的背景图片
        /// </summary>
        public GeneralSkinImage Mania { get; internal set; }
    }
}