using System;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     osu!中的部分基础信息。
    /// </summary>
    public class OsuManifest
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
            ///     世界杯解说人员
            /// </summary>
            WorldCupstaff = 32
        }

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