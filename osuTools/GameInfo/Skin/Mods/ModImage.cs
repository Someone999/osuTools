using System;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Mods
{
    /// <summary>
    ///     Mod的图片元素
    /// </summary>
    public class ModImage : ISkinImage
    {
        /// <summary>
        ///     使用文件名和全路径创建一个ModImage对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        public ModImage(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
        }

        /// <summary>
        ///     皮肤元素类型的名字，应为Mod
        /// </summary>
        public string SkinImageTypeName { get; } = "Mod";
        ///<inheritdoc/>
        public string FileName { get; } = "default";
        ///<inheritdoc/>
        public string FullPath { get; } = "default";
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的图片。");
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
                return new ModImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }

    /// <summary>
    ///     Mod图片的集合
    /// </summary>
    public class ModImageCollection
    {
        public ModImage Easy { get; internal set; }
        public ModImage HalfTime { get; internal set; }
        public ModImage NoFail { get; internal set; }
        public ModImage HardRock { get; internal set; }
        public ModImage SuddenDeath { get; internal set; }
        public ModImage Perfect { get; internal set; }
        public ModImage DoubleTime { get; internal set; }
        public ModImage NightCore { get; internal set; }
        public ModImage Hidden { get; internal set; }
        public ModImage FadeIn { get; internal set; }
        public ModImage Random { get; internal set; }
        public ModImage Mirror { get; internal set; }
        public ModImage Flashlight { get; internal set; }
        public ModImage Relax { get; internal set; }
        public ModImage AutoPilot { get; internal set; }
        public ModImage SpunOut { get; internal set; }
        public ModImage AutoPlay { get; internal set; }
        public ModImage Cinema { get; internal set; }
        public ModImage ScoreV2 { get; internal set; }
        public ModImage Key1 { get; internal set; }
        public ModImage Key2 { get; internal set; }
        public ModImage Key3 { get; internal set; }
        public ModImage Key4 { get; internal set; }
        public ModImage Key5 { get; internal set; }
        public ModImage Key6 { get; internal set; }
        public ModImage Key7 { get; internal set; }
        public ModImage Key8 { get; internal set; }
        public ModImage Key9 { get; internal set; }
        public ModImage KeyCoop { get; internal set; }
    }
}