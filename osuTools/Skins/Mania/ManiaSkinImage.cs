using System;
using System.Drawing;
using System.IO;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Mania
{
    /// <summary>
    /// Mania模式的皮肤图片
    /// </summary>
    public class ManiaSkinImage : ISkinImage
    {
        /// <summary>
        /// 使用
        /// </summary>
        public ManiaSkinImage()
        {
        }
        /// <summary>
        /// 使用父皮肤，文件名初始化一个ManiaSkinImage
        /// </summary>
        /// <param name="parentSkin"></param>
        /// <param name="fileName"></param>
        public ManiaSkinImage(Skin parentSkin, string fileName)
        {
            var ini = parentSkin.ConfigFileDirectory;
            FileName = fileName;
            var tmppath = Path.GetDirectoryName(ini);
            FullPath = Path.Combine(tmppath??throw new InvalidOperationException(), fileName);
        }
        /// <summary>
        /// 使用父皮肤skin.ini的路径和文件名初始化一个ManiaSkinImage
        /// </summary>
        /// <param name="parentSkinDir"></param>
        /// <param name="fileName"></param>
        public ManiaSkinImage(string parentSkinDir, string fileName)
        {
            var ini = Path.GetDirectoryName(parentSkinDir);
            FileName = fileName;
            FullPath = ini;
        }
        ///<inheritdoc/>
        public string FileName { get; }
        ///<inheritdoc/>
        public string FullPath { get; }
        ///<inheritdoc/>

        public Image LoadImage()
        {
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath ?? throw new InvalidOperationException(), tmpname)))
                return new ManiaSkinImage(tmppath, tmpname);
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}