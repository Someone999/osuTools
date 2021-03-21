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

        public string FileName { get; } = "default";
        public string FullPath { get; } = "default";
        public string SkinImageTypeName { get; } = "RankSkinImage";

        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义图片的Mod的图片。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }

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

    /// <summary>
    ///     评级的图片
    /// </summary>
    public class RankingImageCollection
    {
        /// <summary>
        ///     准确度100%时评价的图片
        /// </summary>
        public RankingImage SS { get; internal set; }

        /// <summary>
        ///     准确度100%并开启Hidden或Flashlight或FadeIn时的评价的图片
        /// </summary>
        public RankingImage SSH { get; internal set; }

        /// <summary>
        ///     达到S时的评价的图片。此判定的标准详见<see cref="Game.Modes.GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage S { get; internal set; }

        /// <summary>
        ///     在S的基础上开启Hidden或Flashlight或FadeIn时的评价的图片
        /// </summary>
        public RankingImage SH { get; internal set; }

        /// <summary>
        ///     达到A时的评价的图片。此判定的标准详见<see cref="Game.Modes.GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage A { get; internal set; }

        /// <summary>
        ///     达到B时的评价的图片。此判定的标准详见<see cref="Game.Modes.GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage B { get; internal set; }

        /// <summary>
        ///     达到C时的评价的图片。此判定的标准详见<see cref="Game.Modes.GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage C { get; internal set; }

        /// <summary>
        ///     达到D时的评价的图片。此判定的标准详见<see cref="Game.Modes.GameMode" />中的GetRanking()方法
        /// </summary>
        public RankingImage D { get; internal set; }
    }
}