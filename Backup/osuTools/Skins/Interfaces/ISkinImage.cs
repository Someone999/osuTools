using System.Drawing;

namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     一个皮肤的图像元素
    /// </summary>
    public interface ISkinImage : ISkinObject
    {

        /// <summary>
        ///     将图像读入内存
        /// </summary>
        /// <returns></returns>
        Image LoadImage();

        /// <summary>
        ///     获取在高分辨率下显示的图像
        /// </summary>
        /// <returns></returns>
        ISkinImage GetHighResolutionImage();
    }
}