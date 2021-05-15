namespace osuTools.StoryBoard.Enums
{
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