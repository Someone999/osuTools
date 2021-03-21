using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using osuTools.Skins.Interfaces;
using osuTools.Skins.SkinObjects.Osu;

namespace osuTools.Skins.SkinObjects.Taiko
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
            throw new FileNotFoundException("找不到文件。原因可能是该皮肤使用了非标准的扩展名。");
        }

        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new TaikoSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new FileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }

    /// <summary>
    ///     Taiko的皮肤图片元素的集合
    /// </summary>
    public class TaikoSkinImageCollection
    {
        /// <summary>
        ///     双打Note的图片
        /// </summary>
        public TaikoSkinImage TaikoBigCircle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     单打Note的图片
        /// </summary>
        public TaikoSkinImage TaikoHitCircle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的中间的小豆豆
        /// </summary>
        public TaikoSkinImage SliderScorePoint { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的条子
        /// </summary>
        public TaikoSkinImage TaikoRollMiddle { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     连打的尾部
        /// </summary>
        public TaikoSkinImage TaikoRollEnd { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     转盘的提示
        /// </summary>
        public TaikoSkinImage SpinnerWarning { get; internal set; } = new TaikoSkinImage("default", "default");

        /// <summary>
        ///     单打的外圈
        /// </summary>
        public List<TaikoSkinImage> TaikoHitCircleOverlay { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     双打的外圈
        /// </summary>
        public List<TaikoSkinImage> TaikoBigCircleOverlay { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     左侧的敲鼓等动作
        /// </summary>
        public PipidonSkinImageCollection PippidonImages { get; internal set; } = new PipidonSkinImageCollection();

        /// <summary>
        ///     判定的图标
        /// </summary>
        public TaikoHitBurstImageCollection HitBurstImages { get; internal set; } = new TaikoHitBurstImageCollection();
    }

    /// <summary>
    ///     左侧敲鼓等动作的集合
    /// </summary>
    public class PipidonSkinImageCollection
    {
        /// <summary>
        ///     每100连击播放一次
        /// </summary>
        public List<TaikoSkinImage> PippidonClear { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     每Miss一个播放一次
        /// </summary>
        public List<TaikoSkinImage> PippidonFail { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     在非KiaiTime，连击数不是100的倍数，未连续Miss时播放
        /// </summary>
        public List<TaikoSkinImage> PippidonIdle { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     在KiaiTime，连击数不是100的倍数，未连续Miss时播放
        /// </summary>
        public List<TaikoSkinImage> PipidonKiai { get; internal set; } = new List<TaikoSkinImage>();
    }

    /// <summary>
    ///     Taiko的判定图标
    /// </summary>
    public class TaikoHitBurstImageCollection : GeneralHitBurstImages
    {
        /// <summary>
        ///     一组Note不全为良时所出现的判定
        /// </summary>
        public List<TaikoSkinImage> Hit100k { get; internal set; } = new List<TaikoSkinImage>();

        /// <summary>
        ///     一组Note全为良时所出现的判定
        /// </summary>
        public List<TaikoSkinImage> Hit300k { get; internal set; } = new List<TaikoSkinImage>();
    }
}