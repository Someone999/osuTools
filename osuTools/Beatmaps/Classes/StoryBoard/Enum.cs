namespace osuTools.StoryBoard
{
    /// <summary>
    ///     StoryBoard的图层
    /// </summary>
    public enum StoryBoardLayer
    {
        /// <summary>
        ///     背景
        /// </summary>
        Background,

        /// <summary>
        ///     失败时
        /// </summary>
        Fail,

        /// <summary>
        ///     打过时
        /// </summary>
        Pass,

        /// <summary>
        ///     前景
        /// </summary>
        Foreground,

        /// <summary>
        ///     未指定时的初始值
        /// </summary>
        None = -1
    }

    /// <summary>
    ///     StoryBoard的可用命令
    /// </summary>
    public enum StoryBoardEvent
    {
        /// <summary>
        ///     渐隐
        /// </summary>
        Fade,

        /// <summary>
        ///     移动
        /// </summary>
        Move,

        /// <summary>
        ///     在X轴上移动
        /// </summary>
        MoveX,

        /// <summary>
        ///     在Y轴上移动
        /// </summary>
        MoveY,

        /// <summary>
        ///     缩放
        /// </summary>
        Scale,

        /// <summary>
        ///     矢量缩放
        /// </summary>
        VectorScale,

        /// <summary>
        ///     旋转
        /// </summary>
        Rotate,

        /// <summary>
        ///     颜色
        /// </summary>
        Color,

        /// <summary>
        ///     循环
        /// </summary>
        Loop,

        /// <summary>
        ///     触发器
        /// </summary>
        Trigger,

        /// <summary>
        ///     翻转图片与更改色彩混合
        /// </summary>
        Parameter,

        /// <summary>
        ///     默认值
        /// </summary>
        None
    }

    /// <summary>
    ///     StoryBoard资源的移动类型
    /// </summary>
    public enum StoryBoardEasing
    {
        /// <summary>
        ///     未定义
        /// </summary>
        Unknown = -1,
        Linear,
        EasingOut,
        EasingIn,
        QuadIn,
        QuadOut,
        QuadInOut,
        CubicIn,
        CubicOut,
        CubicInOut,
        QuartIn,
        QuartOut,
        QuartInOut,
        QuintIn,
        QuintOut,
        QuintInOut,
        SineIn,
        SineOut,
        SineInOut,
        ExpoIn,
        ExpoOut,
        ExpoInOut,
        CircIn,
        CircOut,
        CircInOut,
        ElasticIn,
        ElasticOut,
        ElasticHalfOut,
        ElasticQuarterOut,
        ElasticInOut,
        BackIn,
        BackOut,
        BackInOut,
        BounceIn,
        BounceOut,
        BounceInOut
    }

    /// <summary>
    ///     StoryBoard的资源类型
    /// </summary>
    public enum StoryBoardResourceType
    {
        /// <summary>
        ///     音频
        /// </summary>
        Audio,

        /// <summary>
        ///     精灵
        /// </summary>
        Sprite,

        /// <summary>
        ///     动画
        /// </summary>
        Animation
    }

    /// <summary>
    ///     StoryBoar资源所处的位置
    /// </summary>
    public enum StoryBoardOrigin
    {
        /// <summary>
        ///     左上
        /// </summary>
        TopLeft,

        /// <summary>
        ///     中心
        /// </summary>
        Centre,

        /// <summary>
        ///     左
        /// </summary>
        CentreLeft,

        /// <summary>
        ///     右上
        /// </summary>
        TopRight,

        /// <summary>
        ///     底部中心
        /// </summary>
        BottomCentre,

        /// <summary>
        ///     顶部中心
        /// </summary>
        TopCentre,

        /// <summary>
        ///     自定义
        /// </summary>
        Custom,

        /// <summary>
        ///     右
        /// </summary>
        CentreRight,

        /// <summary>
        ///     左下
        /// </summary>
        BottomLeft,

        /// <summary>
        ///     右下
        /// </summary>
        BottomRight,

        /// <summary>
        ///     未定义
        /// </summary>
        Unknown = -1
    }

    /// <summary>
    ///     StoryBoard动画的循环次数
    /// </summary>
    public enum StoryBoardAnimationLoopType
    {
        /// <summary>
        ///     循环一次
        /// </summary>
        Once,

        /// <summary>
        ///     无限循环
        /// </summary>
        Forever,

        /// <summary>
        ///     未定义
        /// </summary>
        Unknow = -1
    }
    /// <summary>
    /// 行为
    /// </summary>
    public enum ParameterOperation
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 水平翻转
        /// </summary>
        HorizentalFlip = 1 << 0,
        /// <summary>
        /// 垂直翻转
        /// </summary>
        VerticalFlip = 1 << 1,
        /// <summary>
        /// 沉浸式混色
        /// </summary>
        AddictiveColorBlend = 1 << 2
    }
}