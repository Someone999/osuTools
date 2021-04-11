using System;
using osuTools.Game.Modes;

namespace osuTools.Tools
{
    /// <summary>
    ///     游戏模式的工具
    /// </summary>
    public static class OsuGameModeTools
    {
        /// <summary>
        ///     将游戏模式从字符串转换为枚举
        /// </summary>
        public static GameMode Parse(string mode)
        {
            if (string.Compare(mode, "mania", StringComparison.OrdinalIgnoreCase) == 0) return new ManiaMode();
            if (string.Compare(mode, "osu", StringComparison.OrdinalIgnoreCase) == 0) return new OsuMode();
            if (string.Compare(mode, "catch", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(mode, "Ctb", StringComparison.OrdinalIgnoreCase) == 0)
                return new CatchMode();
            if (string.Compare(mode, "taiko", StringComparison.OrdinalIgnoreCase) == 0) return new TaikoMode();
            return null;
        }

        /// <summary>
        ///     将游戏模式从整数转换为枚举
        /// </summary>
        public static GameMode Parse(int mode)
        {
            switch (mode)
            {
                case 0: return new OsuMode();
                case 1: return new TaikoMode();
                case 2: return new CatchMode();
                case 3: return new ManiaMode();
                default: return null;
            }
        }
    }
}