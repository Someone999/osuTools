using System.Collections.Generic;
using osuTools.Game.Modes;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     包含指定模式的指定Mods与Star的键值对。
    /// </summary>
    public class DifficultyRate
    {
        internal Dictionary<OsuGameMode, Dictionary<int, double>> Difficuties =
            new Dictionary<OsuGameMode, Dictionary<int, double>>();

        /// <summary>
        ///     使用游戏模式获取难度字典
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Dictionary<int, double> this[OsuGameMode mode]
        {
            get
            {
                try
                {
                    var ret = Difficuties[mode];
                    return ret;
                }
                catch
                {
                    var d = new Dictionary<int, double> {{0, 0}};
                    return d;
                }
            }
        }

        internal void Add(OsuGameMode mode, int modCombine, double stars)
        {
            Difficuties[mode].Add(modCombine, stars);
        }

        /// <summary>
        ///     获取指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <returns></returns>
        public double GetStars(OsuGameMode mode, int modCombine)
        {
            try
            {
                return Difficuties[mode][modCombine];
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///     设置指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <param name="stars" />
        /// <returns></returns>
        public void SetStar(OsuGameMode mode, int modCombine, double stars)
        {
            try
            {
                Difficuties[mode][modCombine] = stars;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     将模式的难度字典更改为指定字典
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dict"></param>
        public void SetModeDict(OsuGameMode mode, Dictionary<int, double> dict)
        {
            Difficuties[mode] = dict;
        }
    }
}