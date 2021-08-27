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
        HitCircle = 0,

        /// <summary>
        ///     滑条
        /// </summary>
        Slider = 1,

        /// <summary>
        ///     开始一个新颜色
        /// </summary>
        NewCombo = 2,

        /// <summary>
        ///     转盘
        /// </summary>
        Spinner = 3,

        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag1 = 4,

        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag2,

        /// <summary>
        ///     指示要跳过的颜色的数量
        /// </summary>
        ColorSkipFlag3,

        /// <summary>
        ///     Taiko连打
        /// </summary>
        DrumRoll,

        /// <summary>
        ///     Mania长条
        /// </summary>
        ManiaHold = 7,

        /// <summary>
        ///     Mania单点
        /// </summary>
        ManiaHit,

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