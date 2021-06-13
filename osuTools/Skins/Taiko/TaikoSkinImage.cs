using System;
using System.Drawing;
using System.IO;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Taiko
{
    /// <summary>
    ///     Taiko皮肤的图片元素
    /// </summary>
    public class TaikoSkinImage : ISkinImage
    {
        /// <summary>
        ///     使用文件名和全路径初始化一个TaikoSkinImage
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFileName"></param>
        public TaikoSkinImage(string fileName, string fullFileName)
        {
            FileName = fileName;
            FullPath = fullFileName;
        }
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
            throw new FileNotFoundException("找不到文件。原因可能是该皮肤使用了非标准的扩展名。");
        }
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath??throw new InvalidOperationException(), tmpname)))
                return new TaikoSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new FileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}