using osuTools.Beatmaps;
using osuTools.Game.Mods;

namespace osuTools.Game
{
    /// <summary>
    ///     代表一个可用于GameMode计算的分数结果
    /// </summary>
    public interface IScoreInfo
    {
        /// <summary>
        ///     300g的数量
        /// </summary>
        int CountGeki { get; }

        /// <summary>
        ///     300的数量
        /// </summary>
        int Count300 { get; }

        /// <summary>
        ///     200的数量
        /// </summary>
        int CountKatu { get; }
        /// <summary>
        /// 开启的Mod
        /// </summary>
        ModList Mods { get; }

        /// <summary>
        ///     100的数量
        /// </summary>
        int Count100 { get; }

        /// <summary>
        ///     50的数量
        /// </summary>
        int Count50 { get; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        int CountMiss { get; }
        /// <summary>
        /// 玩家达到的最大连击
        /// </summary>
        int PlayerMaxCombo { get; }
        /// <summary>
        /// 最大连击
        /// </summary>
        int MaxCombo { get; }
        /// <summary>
        /// 当前连击
        /// </summary>

        int CurrentCombo { get;  }
        /// <summary>
        /// 谱面
        /// </summary>
        Beatmap Beatmap { get; }
    }
}
