namespace osuTools.StoryBoard.Enums
{
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
}