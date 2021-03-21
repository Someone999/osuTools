using System;
using OsuRTDataProvider.Listen;
using osuTools.Game.Modes;

namespace osuTools
{
    /// <summary>
    ///     记录游戏模式
    /// </summary>
    [Serializable]
    public class GMMode
    {
        /// <summary>
        ///     使用两个<see cref="OsuRTDataProvider.Listen.OsuPlayMode" />构造一个GMMode
        /// </summary>
        /// <param name="LastMode"></param>
        /// <param name="NowMode"></param>
        public GMMode(OsuPlayMode LastMode, OsuPlayMode NowMode)
        {
            //IO.CurrentIO.Write($"Get Mode {LastMode} -> {NowMode}");
            this.LastMode = GameMode.FromLegacyMode((OsuGameMode) LastMode);
            CurrentMode = GameMode.FromLegacyMode((OsuGameMode) NowMode);
            //IO.CurrentIO.Write("");
        }

        /// <summary>
        ///     上一次的游戏模式
        /// </summary>
        public GameMode LastMode { get; }

        /// <summary>
        ///     当前游戏模式
        /// </summary>
        public GameMode CurrentMode { get; }
    }

    namespace Tools
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
                if (string.Compare(mode, "mania", true) == 0) return new ManiaMode();
                if (string.Compare(mode, "osu", true) == 0) return new OsuMode();
                if (string.Compare(mode, "catch", true) == 0 || string.Compare(mode, "Ctb", true) == 0)
                    return new CatchMode();
                if (string.Compare(mode, "taiko", true) == 0) return new TaikoMode();
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
}