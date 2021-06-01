using System;
using System.Drawing;
using System.IO;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Game
{
    /// <summary>
    /// 一般的皮肤文件
    /// </summary>
    public class GeneralSkinImage : ISkinImage
    {
        /// <summary>
        /// 使用文件名和全路径初始化一个Generic
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFileName"></param>
        public GeneralSkinImage(string fileName, string fullFileName)
        {
            FileName = fileName;
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }
        ///<inheritdoc/>
        public string FileName { get; protected set; }
        ///<inheritdoc/>
        public string FullPath { get; protected set; }
        ///<inheritdoc/>
        public string SkinImageTypeName { get; protected set; } = "MenuSkinImage";
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的皮肤元素。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new GeneralSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}