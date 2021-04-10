using System;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Catch
{
    /// <summary>
    ///     接水果皮肤图片的元素
    /// </summary>
    public class CatchSkinImage : ISkinImage
    {
        /// <summary>
        ///     使用文件名和文件全路径初始化一个CatchSkinImage
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFileName"></param>
        public CatchSkinImage(string fileName, string fullFileName)
        {
            FileName = fileName;
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }

        public string FileName { get; } = "default";
        public string FullPath { get; } = "default";
        public string SkinImageTypeName { get; } = "OsuSkinImage";

        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的图片。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }

        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new CatchSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}