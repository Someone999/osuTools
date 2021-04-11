namespace osuTools.Attributes
{
    /// <summary>
    ///     用于WorkingInProgressAttribute的枚举，用于表示开发进程
    /// </summary>
    public enum DevelopmentStage
    {
        /// <summary>
        ///     开发刚刚开始
        /// </summary>
        AtStart,

        /// <summary>
        ///     开发中
        /// </summary>
        Developing,

        /// <summary>
        ///     开发因非技术原因暂停
        /// </summary>
        Breaked,

        /// <summary>
        ///     开发因技术原因暂停
        /// </summary>
        Stuck,

        /// <summary>
        ///     查错期
        /// </summary>
        TroubleShooting,

        /// <summary>
        ///     调试期
        /// </summary>
        Debug
    }
}