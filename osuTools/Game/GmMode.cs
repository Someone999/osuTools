using System;
using OsuRTDataProvider.Listen;
using osuTools.Game.Modes;

namespace osuTools.Game
{
    /// <summary>
    ///     记录游戏模式
    /// </summary>
    [Serializable]
    public class GmMode
    {
        /// <summary>
        ///     使用两个<see cref="OsuRTDataProvider.Listen.OsuPlayMode" />构造一个GMMode
        /// </summary>
        /// <param name="lastMode"></param>
        /// <param name="nowMode"></param>
        public GmMode(OsuPlayMode lastMode, OsuPlayMode nowMode)
        {
            LastMode = GameMode.FromLegacyMode((OsuGameMode) lastMode);
            CurrentMode = GameMode.FromLegacyMode((OsuGameMode) nowMode);
        }

        /// <summary>
        ///     上一次的游戏模式
        /// </summary>
        public GameMode LastMode { get; internal set; }

        /// <summary>
        ///     当前游戏模式
        /// </summary>
        public GameMode CurrentMode { get; internal set; }
    }
}