using System.Collections.Generic;
using osuTools.Skins.Game.Menu;
using osuTools.Skins.Game.Overlay;
using osuTools.Skins.Game.Playfield;
using osuTools.Skins.Game.Rank;
using osuTools.Skins.Game.ResultPage;

namespace osuTools.Skins.Game
{
    /// <summary>
    ///     通用皮肤元素的集合
    /// </summary>
    public class GenericSkinObjectCollection
    {
        /// <summary>
        ///     光标
        /// </summary>
        public GeneralSkinImage Cursor { get; internal set; }

        /// <summary>
        ///     光标拖尾
        /// </summary>
        public GeneralSkinImage CursorTrail { get; internal set; }

        /// <summary>
        ///     圈圈里的数字
        /// </summary>
        public List<GeneralSkinImage> HitCircleNumberImages { get; internal set; } = new List<GeneralSkinImage>();

        /// <summary>
        ///     分数的图片
        /// </summary>
        public ScoreImageCollections ScoreImages { get; internal set; } = new ScoreImageCollections();

        /// <summary>
        ///     菜单的返回按钮
        /// </summary>
        public List<GeneralSkinImage> MenuBackImages { get; internal set; } = new List<GeneralSkinImage>();

        /// <summary>
        ///     谱面信息标签的背景
        /// </summary>
        public GeneralSkinImage MenuButtonBackground { get; internal set; }

        /// <summary>
        ///     粉饼界面的背景
        /// </summary>
        public GeneralSkinImage MenuBackground { get; internal set; }

        /// <summary>
        ///     粉饼界面的雪花
        /// </summary>
        public GeneralSkinImage MenuSnow { get; internal set; }

        /// <summary>
        ///     在模式列表中，各个模式的背景图片
        /// </summary>
        public ModeListOverlay ModeListImages { get; internal set; } = new ModeListOverlay();

        /// <summary>
        ///     游戏播放的跳过动画的所有图片
        /// </summary>
        public List<GeneralSkinImage> SkipImages { get; internal set; } = new List<GeneralSkinImage>();

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
        public GeneralSkinImage Star { get; internal set; }

        /// <summary>
        ///     血条
        /// </summary>
        public ScoreBarSkinImageCollection ScoreBarSkinImages { get; internal set; } =
            new ScoreBarSkinImageCollection();
    }
}