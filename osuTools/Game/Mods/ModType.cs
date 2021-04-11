namespace osuTools.Game.Mods
{
    /// <summary>
    ///     Mod的类型
    /// </summary>
    public enum ModType
    {
        /// <summary>
        ///     降低难度的Mod
        /// </summary>
        DifficultyReduction,

        /// <summary>
        ///     提升难度的Mod
        /// </summary>
        DifficultyIncrease,

        /// <summary>
        ///     转谱Mod
        /// </summary>
        Conversion,

        /// <summary>
        ///     自动Mod
        /// </summary>
        Automation,

        /// <summary>
        ///     趣味Mod
        /// </summary>
        Fun,

        /// <summary>
        ///     系统Mod
        /// </summary>
        System
    }
}