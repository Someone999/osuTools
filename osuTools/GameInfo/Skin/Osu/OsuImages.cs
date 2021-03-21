using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;
using osuTools.Skins.SkinObjects.Osu.Slider;
using osuTools.Skins.SkinObjects.Osu.Spinner;

namespace osuTools.Skins.SkinObjects.Osu
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
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }

        /// <summary>
        ///     创建一个空的OsuSkinImage对象
        /// </summary>
        public OsuSkinImage()
        {
        }

        public string FileName { get; } = "default";
        public string FullPath { get; } = "default";
        public string SkinImageTypeName { get; } = "OsuSkinImage";

        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new OsuSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }

        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的图片。");
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
    }

    /// <summary>
    ///     通用的判定突变
    /// </summary>
    public class GeneralHitBurstImages
    {
        /// <summary>
        ///     Miss的判定图标
        /// </summary>
        public List<ISkinImage> Hit0 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     50的判定图标
        /// </summary>
        public List<ISkinImage> Hit50 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     100的判定图标
        /// </summary>
        public List<ISkinImage> Hit100 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     300的判定图标
        /// </summary>
        public List<ISkinImage> Hit300 { get; internal set; } = new List<ISkinImage>();
    }

    /// <summary>
    ///     Osu模式的判定图标
    /// </summary>
    public class OsuHitBurstImageCollection : GeneralHitBurstImages
    {
        /// <summary>
        ///     一组Note中有不是300的判定时显示的图片
        /// </summary>
        public List<ISkinImage> Hit100k { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     一组Note中都是300的判定时显示的图片
        /// </summary>
        public List<ISkinImage> Hit300k { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     滑条中的10分的图片
        /// </summary>
        public List<ISkinImage> SliderPoint10 { get; internal set; } = new List<ISkinImage>();

        /// <summary>
        ///     滑条中的30分的图片
        /// </summary>
        public List<ISkinImage> SliderPoint30 { get; internal set; } = new List<ISkinImage>();
    }

    /// <summary>
    ///     Osu模式的图片的集合
    /// </summary>
    public class OsuSkinImageCollection
    {
        /// <summary>
        ///     圈圈的外圈的图片
        /// </summary>
        public OsuSkinImage ApproachCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     圈圈的图片
        /// </summary>
        public OsuSkinImage HitCircle { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     小豆豆的图片
        /// </summary>
        public OsuSkinImage FollowPoint { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     编辑器中选中的圈圈的外圈
        /// </summary>
        public OsuSkinImage HitCircleSelect { get; internal set; } = new OsuSkinImage();

        /// <summary>
        ///     滑条的皮肤图片
        /// </summary>
        public SliderSkinImageCollection SliderSkinImages { get; internal set; } = new SliderSkinImageCollection();

        /// <summary>
        ///     转盘的皮肤图片
        /// </summary>
        public SpinnerSkinImageCollection SpinnerSkinImages { get; internal set; } = new SpinnerSkinImageCollection();

        /// <summary>
        ///     转盘的外圈图片
        /// </summary>
        public List<OsuSkinImage> HitCircleOverlay { get; internal set; } = new List<OsuSkinImage>();

        /// <summary>
        ///     Osu达到指定连击后显示的图片
        /// </summary>
        public OsuHitBurstImageCollection HitBurstImages { get; internal set; } = new OsuHitBurstImageCollection();
    }
}