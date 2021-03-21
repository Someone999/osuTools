using System.Drawing;

namespace osuTools.Skins.Interfaces
{
    /// <summary>
    ///     表示一个皮肤元素
    /// </summary>
    public interface ISkinObjectBase
    {
    }

    /// <summary>
    ///     表示一个皮肤元素文件
    /// </summary>
    public interface ISkinObject
    {
        /// <summary>
        ///     文件名
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     全路径
        /// </summary>
        string FullPath { get; }
    }

    /// <summary>
    ///     一个皮肤的图像元素
    /// </summary>
    public interface ISkinImage : ISkinObject
    {
        /// <summary>
        ///     图片在游戏中对应的元素
        /// </summary>
        string SkinImageTypeName { get; }

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

    /// <summary>
    ///     表示一个皮肤的音频元素
    /// </summary>
    public interface ISkinSound : ISkinObject
    {
        /// <summary>
        ///     音频在游戏中对应的元素
        /// </summary>
        string SkinSoundTypeName { get; }
    }

    /// <summary>
    ///     一个有图片元素和音频元素的皮肤元素
    /// </summary>
    public interface ISoundedSkinImage : ISkinObjectBase
    {
        /// <summary>
        ///     图片
        /// </summary>
        ISkinImage Image { get; }

        /// <summary>
        ///     音频
        /// </summary>
        ISkinSound Sound { get; }
    }

    /// <summary>
    ///     一个Mod的图片
    /// </summary>
    public interface IModImage : ISkinObject
    {
        /// <summary>
        ///     对应的Mod
        /// </summary>
        OsuGameMod Mod { get; }
    }
}