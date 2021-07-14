using System;

namespace osuTools.Game.Modes
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
            
            if (string.Compare(mode, "osu", StringComparison.OrdinalIgnoreCase) == 0) 
                return GameMode.FromLegacyMode(OsuGameMode.Osu);
            if (string.Compare(mode, "taiko", StringComparison.OrdinalIgnoreCase) == 0) 
                return GameMode.FromLegacyMode(OsuGameMode.Taiko);
            if (string.Compare(mode, "catch", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(mode, "Ctb", StringComparison.OrdinalIgnoreCase) == 0)
                GameMode.FromLegacyMode(OsuGameMode.Catch);
            if (string.Compare(mode, "mania", StringComparison.OrdinalIgnoreCase) == 0) 
                GameMode.FromLegacyMode(OsuGameMode.Mania);
            return null;
        }

        /// <summary>
        ///     将游戏模式从整数转换为枚举
        /// </summary>
        public static GameMode Parse(int mode)
        {
            switch (mode)
            {
                case 0: return GameMode.FromLegacyMode(OsuGameMode.Osu);
                case 1: return GameMode.FromLegacyMode(OsuGameMode.Taiko);
                case 2: return GameMode.FromLegacyMode(OsuGameMode.Catch);
                case 3: return GameMode.FromLegacyMode(OsuGameMode.Mania);
                default: return null;
            }
        }
    }
}