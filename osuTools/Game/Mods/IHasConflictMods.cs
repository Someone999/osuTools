namespace osuTools.Game.Mods
{
    /// <summary>
    ///     表明这个Mod有不能Mod与这个Mod一起开的Mod
    /// </summary>
    public interface IHasConflictMods
    {
        /// <summary>
        ///     不能与这个Mod一起开的Mod
        /// </summary>
        Mod[] ConflictMods { get; }
    }
}