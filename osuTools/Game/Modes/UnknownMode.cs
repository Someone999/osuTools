using osuTools.Game.Mods;

namespace osuTools.Game.Modes
{
    /// <summary>
    /// 出错时的预留Mod
    /// </summary>
    public class UnknownMode : GameMode
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Mod[] AvaliableMods => new Mod[0];
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ModeName => "Unknown";
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Description => "未知的模式。";
    }
}