using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;
using osuTools.Skins.SkinObjects.Generic.Menu;
using osuTools.Skins.SkinObjects.Generic.PauseMenu;
using osuTools.Skins.SkinObjects.Generic.PlayField.Countdown;
using osuTools.Skins.SkinObjects.Generic.Rank;
using osuTools.Skins.SkinObjects.Generic.ResultPage;
using osuTools.Skins.SkinObjects.Generic.Score;

namespace osuTools.Skins.SkinObjects.Generic
{
    public class GenericSkinImage : ISkinImage
    {
        public GenericSkinImage(string fileName, string fullFileName)
        {
            FileName = fileName;
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }
        ///<inheritdoc/>
        public string FileName { get; protected set; } = "default";
        ///<inheritdoc/>
        public string FullPath { get; protected set; } = "default";
        ///<inheritdoc/>
        public string SkinImageTypeName { get; protected set; } = "MenuSkinImage";
        ///<inheritdoc/>
        public Image LoadImage()
        {
            if (FileName == "default" && FullPath == "default")
                throw new NotSupportedException("无法加载未自定义的皮肤元素。");
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
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }


    /// <summary>
    ///     通用皮肤元素的集合
    /// </summary>
    public class GenericSkinObjectCollection
    {
        /// <summary>
        ///     光标
        /// </summary>
        public GenericSkinImage Cursor { get; internal set; }

        /// <summary>
        ///     光标拖尾
        /// </summary>
        public GenericSkinImage CursorTrail { get; internal set; }

        /// <summary>
        ///     圈圈里的数字
        /// </summary>
        public List<GenericSkinImage> HitCircleNumberImages { get; internal set; } = new List<GenericSkinImage>();

        /// <summary>
        ///     分数的图片
        /// </summary>
        public ScoreImageCollections ScoreImages { get; internal set; } = new ScoreImageCollections();

        /// <summary>
        ///     菜单的返回按钮
        /// </summary>
        public List<GenericSkinImage> MenuBackImages { get; internal set; } = new List<GenericSkinImage>();

        /// <summary>
        ///     谱面信息标签的背景
        /// </summary>
        public GenericSkinImage MenuButtonBackground { get; internal set; }

        /// <summary>
        ///     粉饼界面的背景
        /// </summary>
        public GenericSkinImage MenuBackground { get; internal set; }

        /// <summary>
        ///     粉饼界面的雪花
        /// </summary>
        public GenericSkinImage MenuSnow { get; internal set; }

        /// <summary>
        ///     在模式列表中，各个模式的背景图片
        /// </summary>
        public ModeListOverlay ModeListImages { get; internal set; } = new ModeListOverlay();

        /// <summary>
        ///     游戏播放的跳过动画的所有图片
        /// </summary>
        public List<GenericSkinImage> SkipImages { get; internal set; } = new List<GenericSkinImage>();

        /// <summary>
        ///     评级的图片
        /// </summary>
        public RankingImageCollection RankingImages { get; internal set; } = new RankingImageCollection();

        /// <summary>
        ///     评级界面的图片
        /// </summary>
        public ResultPageImageCollection ResultPageImages { get; internal set; } = new ResultPageImageCollection();

        /// <summary>
        ///     暂停菜单的图片
        /// </summary>
        public PauseMenuImageCollection PauseMenuImages { get; internal set; } = new PauseMenuImageCollection();

        /// <summary>
        ///     倒计时的图片
        /// </summary>
        public CountdownImageCollection Countdown { get; internal set; } = new CountdownImageCollection();

        /// <summary>
        ///     星星的图片
        /// </summary>
        public GenericSkinImage Star { get; internal set; }

        /// <summary>
        ///     血条
        /// </summary>
        public ScoreBarSkinImageCollection ScoreBarSkinImages { get; internal set; } =
            new ScoreBarSkinImageCollection();
    }
}