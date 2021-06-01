using System;
using System.Drawing;
using System.IO;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Mods
{
    /// <summary>
    ///     Mod的图片元素
    /// </summary>
    public class ModImage : ISkinImage
    {
        /// <summary>
        ///     使用文件名和全路径创建一个ModImage对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        public ModImage(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
        }

        /// <summary>
        ///     皮肤元素类型的名字，应为Mod
        /// </summary>
        public string SkinImageTypeName { get; } = "Mod";
        ///<inheritdoc/>
        public string FileName { get; }
        ///<inheritdoc/>
        public string FullPath { get; }
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的图片。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath ?? string.Empty, tmpname)))
                return new ModImage(tmpname, Path.Combine(tmppath ?? string.Empty, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}