namespace osuTools.Skins.SkinObjects.Catch
{
    /// <summary>
    ///     接水果的皮肤图片
    /// </summary>
    public class CatchSkinImageCollection
    {
        /// <summary>
        ///     水果的皮肤图片
        /// </summary>
        public FruitImages Fruit { get; internal set; } = new FruitImages();

        /// <summary>
        ///     水果容器元素的图片
        /// </summary>
        public FruitCatcherImages FruitCatcher { get; internal set; } = new FruitCatcherImages();
    }
}