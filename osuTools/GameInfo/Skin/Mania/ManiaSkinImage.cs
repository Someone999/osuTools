using System.Collections.Generic;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;
using osuTools.Skins.Settings.Mania.MultipleColumnsSettings;
using osuTools.Skins.SkinObjects.Osu;

namespace osuTools.Skins.SkinObjects.Mania
{
    public class ManiaSkinImage : ISkinImage
    {
        public ManiaSkinImage()
        {
        }

        public ManiaSkinImage(Skin parentSkin, string fileName, string skinImageTypeName)
        {
            var ini = parentSkin.ConfigFileDirectory;
            FileName = fileName;
            var tmppath = Path.GetDirectoryName(ini);
            FullPath = Path.Combine(tmppath, fileName);
            SkinImageTypeName = skinImageTypeName;
        }

        public ManiaSkinImage(string parentSkinDir, string fileName, string skinImageTypeName)
        {
            var ini = Path.GetDirectoryName(parentSkinDir);
            FileName = fileName;
            FullPath = ini;
            SkinImageTypeName = skinImageTypeName;
        }
        ///<inheritdoc/>
        public string FileName { get; }
        ///<inheritdoc/>
        public string FullPath { get; }
        ///<inheritdoc/>
        public string SkinImageTypeName { get; }
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
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new ManiaSkinImage(tmppath, tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }

    /// <summary>
    ///     Mania的皮肤图片的集合
    /// </summary>
    public class ManiaSkinImageCollection
    {
        /// <summary>
        ///     未按下键时对应键位的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> KeyImage { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     按下键时对应键位的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> KeyImageD { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     普通Note的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> NoteImage { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     长条头部的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> NoteImageH { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     长条主体的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> NoteImageL { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     长条尾部的图片
        /// </summary>
        public MultipleColumnsSetting<ManiaSkinImage> NoteImageT { get; internal set; } =
            new MultipleColumnsSetting<ManiaSkinImage>();

        /// <summary>
        ///     游戏区域左侧的图片
        /// </summary>
        public ManiaSkinImage StageLeft { get; internal set; }

        /// <summary>
        ///     游戏区域右侧的图片
        /// </summary>
        public ManiaSkinImage StageRight { get; internal set; }

        /// <summary>
        ///     游戏区域偏下区域显示的图片
        /// </summary>
        public ManiaSkinImage StageBottom { get; internal set; }

        /// <summary>
        ///     游戏区域底部显示的图片
        /// </summary>
        public ManiaSkinImage StageHint { get; internal set; }

        /// <summary>
        ///     打击Note时出现的亮光
        /// </summary>
        public ManiaSkinImage StageLight { get; internal set; }

        /// <summary>
        ///     击打Note的光效
        /// </summary>
        public ManiaSkinImage LightingN { get; internal set; }

        /// <summary>
        ///     按住长条时的光效
        /// </summary>
        public ManiaSkinImage LightingL { get; internal set; }

        /// <summary>
        ///     Note出现前3秒的提示箭头
        /// </summary>
        public ManiaSkinImage WarningArrow { get; internal set; }
    }

    /// <summary>
    ///     Mania模式的判定图标
    /// </summary>
    public class ManiaHitBurstImageCollection : GeneralHitBurstImages
    {
        public List<ISkinImage> Hit200 { get; internal set; } = new List<ISkinImage>();
        public List<ISkinImage> Hit300g { get; internal set; } = new List<ISkinImage>();
    }

    /// <summary>
    ///     Mania模式的连击提示图
    /// </summary>
    public class ManiaComboBurstCollection
    {
        public List<ManiaSkinImage> ComboBurstImages { get; internal set; } = new List<ManiaSkinImage>();
    }
}