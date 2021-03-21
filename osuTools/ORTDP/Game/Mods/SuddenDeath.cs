namespace osuTools.Game.Mods
{
    public class SuddenDeathMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod
        {
            get => true;
            protected set => IsRankedMod = value;
        }

        public override string Name
        {
            get => "SuddenDeath";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "SD";
            protected set => ShortName = value;
        }

        public override ModType Type
        {
            get => ModType.DifficultyIncrease;
            protected set => Type = value;
        }

        /// <summary>
        ///     与这个Mod相冲突的Mod
        /// </summary>
        public Mod[] ConflictMods => new Mod[] {new PerfectMod(), new NoFailMod()};

        public OsuGameMod LegacyMod => OsuGameMod.SuddenDeath;
    }
}