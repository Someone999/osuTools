using System;
using System.Drawing;
using System.IO;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Osu
{
    /// <summary>
    ///     Osu模式皮肤元素的图片
    /// </summary>
    public class OsuSkinImage : ISkinImage
    {
        /// <summary>
        ///     使用文件名和全路径创建一个OsuSkinImage
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFileName"></param>
        public OsuSkinImage(string fileName, string fullFileName)
        {
            FileName = fileName;
            FullPath = fullFileName;
        }

        /// <summary>
        ///     创建一个空的OsuSkinImage对象
        /// </summary>
        public OsuSkinImage()
        {
        }
        ///<inheritdoc/>
        public string FileName { get; } = "default";
        ///<inheritdoc/>
        public string FullPath { get; } = "default";
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath??throw new InvalidOperationException(), tmpname)))
                return new OsuSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的图片。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
    }
}