namespace osuTools.Beatmaps
{
    /// <summary>
    ///     预设音效
    /// </summary>
    public enum SampleSets
    {
        /// <summary>
        ///     默认
        /// </summary>
        Default,

        /// <summary>
        ///     普通
        /// </summary>
        Normal,

        /// <summary>
        ///     轻柔
        /// </summary>
        Soft,

        /// <summary>
        ///     击鼓
        /// </summary>
        Drum
    }

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
        DrumRoll = 3,

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

    /// <summary>
    ///     曲线类型
    /// </summary>
    public enum CurveTypes
    {
        /// <summary>
        ///     贝塞尔曲线
        /// </summary>
        Bezier,

        /// <summary>
        ///     CRS曲线
        /// </summary>
        CentripetalCatmullRom,

        /// <summary>
        ///     线性曲线
        /// </summary>
        Linear,

        /// <summary>
        ///     完美曲线
        /// </summary>
        PerfectCircle,

        /// <summary>
        ///     未定义
        /// </summary>
        Unknown = -1
    }

    /// <summary>
    ///     打击音效的类型
    /// </summary>
    public enum HitSounds
    {
        /// <summary>
        ///     普通
        /// </summary>
        Normal,

        /// <summary>
        ///     口哨
        /// </summary>
        Whistle,

        /// <summary>
        ///     结束音效
        /// </summary>
        Finish,

        /// <summary>
        ///     拍掌
        /// </summary>
        Clap
    }

    /// <summary>
    ///     Taiko的连打的类型
    /// </summary>
    public enum DrumRollTypes
    {
        /// <summary>
        ///     转盘
        /// </summary>
        Spinner = 3,

        /// <summary>
        ///     长条
        /// </summary>
        Slider = 1
    }
}