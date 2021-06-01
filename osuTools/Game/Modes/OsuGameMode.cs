using System;

namespace osuTools.Game.Modes
{
    /// <summary>
    ///     Osu的游戏模式
    /// </summary>
    [Serializable]
    public enum OsuGameMode
    {
        /// <summary>
        ///     戳圈圈
        /// </summary>
        Osu,

        /// <summary>
        ///     太鼓
        /// </summary>
        Taiko,

        /// <summary>
        ///     接水果
        /// </summary>
        Catch,

        /// <summary>
        ///     砸键盘
        /// </summary>
        Mania,

        /// <summary>
        ///     未定义
        /// </summary>
        Unkonwn = -1
    }
}