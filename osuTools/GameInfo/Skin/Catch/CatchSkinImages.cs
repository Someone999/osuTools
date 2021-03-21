using System;
using System.Collections.Generic;
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

    /// <summary>
    ///     水果的图片
    /// </summary>
    public class FruitImages
    {
        /// <summary>
        ///     苹果
        /// </summary>
        public CatchSkinImage Apple { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     葡萄
        /// </summary>
        public CatchSkinImage Grapes { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     橘子
        /// </summary>
        public CatchSkinImage Orange { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     梨子
        /// </summary>
        public CatchSkinImage Pear { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     香蕉
        /// </summary>
        public CatchSkinImage Bananas { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     果粒
        /// </summary>
        public CatchSkinImage Drop { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     苹果的渲染外圈
        /// </summary>
        public CatchSkinImage AppleOverlay { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     葡萄的渲染外圈
        /// </summary>
        public CatchSkinImage GrapesOverlay { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     橘子的渲染外圈
        /// </summary>
        public CatchSkinImage OrangeOverlay { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     梨子的渲染外圈
        /// </summary>
        public CatchSkinImage PearOverlay { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     香蕉的渲染外圈
        /// </summary>
        public CatchSkinImage BananasOverlay { get; internal set; } = new CatchSkinImage("default", "default");

        /// <summary>
        ///     果粒的渲染外圈
        /// </summary>
        public CatchSkinImage DropOverlay { get; internal set; } = new CatchSkinImage("default", "default");
    }

    /// <summary>
    ///     水果容器元素的图片
    /// </summary>
    public class FruitCatcherImages
    {
        /// <summary>
        ///     漏掉水果的时候
        /// </summary>
        public List<CatchSkinImage> Fail { get; internal set; } = new List<CatchSkinImage>();

        /// <summary>
        ///     空闲的时候
        /// </summary>
        public List<CatchSkinImage> Idle { get; internal set; } = new List<CatchSkinImage>();

        /// <summary>
        ///     KiaiTime的时候
        /// </summary>

        public List<CatchSkinImage> Kiai { get; internal set; } = new List<CatchSkinImage>();
    }

    /// <summary>
    ///     接水果的皮肤图片
    /// </summary>
    public class CatchSkinImageCollection
    {
        /// <summary>
        ///     水果的皮肤图片
        /// </summary>
        public FruitImages Fruit { get; internal set; } = new FruitImages();

        /// <summary>
        ///     水果容器元素的图片
        /// </summary>
        public FruitCatcherImages FruitCatcher { get; internal set; } = new FruitCatcherImages();
    }
}