using System;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Generic.Rank
{
    public class RankingImage : ISkinImage
    {
        public RankingImage(string fileName, string fullFileName)
        {
            FileName = fileName + ".png";
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }
        ///<inheritdoc/>
        public string FileName { get; } = "default";
        ///<inheritdoc/>
        public string FullPath { get; } = "default";
        ///<inheritdoc/>
        public string SkinImageTypeName { get; } = "RankSkinImage";
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义图片的Mod的图片。");
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
                return new GenericSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到这个Rank图片的@2x版本。");
        }

        public ISkinImage GetIcon()
        {
            var tmpname = FileName.Replace(".png", "-small.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new GenericSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到这个Rank图片的小图标。");
        }
    }
}