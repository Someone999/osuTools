namespace osuTools.Skins.Mania
{
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
}