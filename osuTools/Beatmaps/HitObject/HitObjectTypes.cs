namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     打击物件的类型
    /// </summary>
    public enum HitObjectTypes
    {
        /// <summary>
        ///     圈圈
        /// </summary>
        HitCircle,

        /// <summary>
        ///     滑条
        /// </summary>
        Slider,

        /// <summary>
        ///     开始一个新颜色
        /// </summary>
        NewCombo,

        /// <summary>
        ///     转盘
        /// </summary>
        Spinner,
        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag1,

        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag2,

        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag3,

        /// <summary>
        ///     Mania长条
        /// </summary>
        ManiaHold,

        /// <summary>
        ///     Mania单点
        /// </summary>
        ManiaHit,

        /// <summary>
        ///     Taiko连打
        /// </summary>
        DrumRoll,

        /// <summary>
        ///     水果
        /// </summary>
        Fruit,

        /// <summary>
        ///     果汁流
        /// </summary>
        JuiceStream,

        /// <summary>
        ///     香蕉雨
        /// </summary>
        BananaShower,
        /// <summary>
        ///  JuiceStream中的果粒
        /// </summary>
        CatchSliderTick,
        /// <summary>
        ///     Taiko内侧单打
        /// </summary>
        TaikoRedHit,

        /// <summary>
        ///     Taiko内侧双打
        /// </summary>
        LargeTaikoRedHit,

        /// <summary>
        ///     Taiko外侧单打
        /// </summary>
        TaikoBlueHit,

        /// <summary>
        ///     Taiko外侧双打
        /// </summary>
        LargeTaikoBlueHit,

        /// <summary>
        ///     未指定
        /// </summary>
        Unknown = -1
    }
}