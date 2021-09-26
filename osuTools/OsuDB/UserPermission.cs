namespace osuTools.OsuDB
{
    /// <summary>
    ///     当前登录用户所拥有的权限。
    /// </summary>
    public enum UserPermission
    {
        /// <summary>
        ///     无
        /// </summary>
        None,

        /// <summary>
        ///     普通身份
        /// </summary>
        Normal,

        /// <summary>
        ///     主持人
        /// </summary>
        Moderator,

        /// <summary>
        ///     支持者
        /// </summary>
        Supporter = 4,

        /// <summary>
        ///     好友
        /// </summary>
        Friend = 8,

        /// <summary>
        ///     官方
        /// </summary>
        Peppy = 16,

        /// <summary>
        ///     世界杯工作人员
        /// </summary>
        WorldCupStaff = 32
    }
}