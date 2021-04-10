using System;

namespace osuTools
{
    /// <summary>
    ///     osu的游戏状态
    /// </summary>
    [Serializable]
    public enum OsuGameStatus
    {
        /// <summary>
        ///     未找到进程
        /// </summary>
        ProcessNotFound = 1,

        /// <summary>
        ///     未定义
        /// </summary>
        Unkonwn = 2,

        /// <summary>
        ///     选歌
        /// </summary>
        SelectSong = 4,

        /// <summary>
        ///     游戏中
        /// </summary>
        Playing = 8,

        /// <summary>
        ///     谱面编辑
        /// </summary>
        Editing = 16,

        /// <summary>
        ///     结算
        /// </summary>
        Rank = 32,

        /// <summary>
        ///     位于游戏房间中
        /// </summary>
        MatchSetup = 64,

        /// <summary>
        ///     位于多人游戏大厅
        /// </summary>
        Lobby = 128,

        /// <summary>
        ///     位于主界面
        /// </summary>
        Idle = 256
    }
}