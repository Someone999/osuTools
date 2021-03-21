using osuTools.Game.Mods;

namespace osuTools.Game.Modes
{
    public class UnknownMode : GameMode
    {
        public override Mod[] AvaliableMods => new Mod[0];
        public override string ModeName => "Unknown";
        public override string Description => "未知的模式。";
    }
}