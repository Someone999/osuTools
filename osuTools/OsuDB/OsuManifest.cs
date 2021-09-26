using System;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     osu!中的部分基础信息。
    /// </summary>
    public class OsuManifest
    {
       

        /// <summary>
        ///     当前游戏的版本。
        /// </summary>
        public int Version { get; internal set; }

        /// <summary>
        ///     当前谱面目录下文件夹的数目
        /// </summary>
        public int FolderCount { get; internal set; }

        /// <summary>
        ///     账号是否处于未封禁的状态。
        /// </summary>
        public bool AccountUnlocked { get; internal set; }

        /// <summary>
        ///     账号解封的时间。
        /// </summary>
        public DateTime AccountUnlockTime { get; internal set; }

        /// <summary>
        ///     当前登录的用户的用户名。
        /// </summary>
        public string PlayerName { get; internal set; }

        /// <summary>
        ///     谱面的数目。
        /// </summary>
        public int NumberOfBeatmap { get; internal set; }

        /// <summary>
        ///     当前登录用户所拥有的权限
        /// </summary>
        public UserPermission Permission { get; internal set; }
    }
}