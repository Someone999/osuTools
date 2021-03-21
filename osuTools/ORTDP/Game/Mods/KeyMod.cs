using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class KeyMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod
        {
            get => true;
            protected set => IsRankedMod = value;
        }

        public override string Name
        {
            get => "KeyMod";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "KeyMod";
            protected set => ShortName = value;
        }

        public override ModType Type
        {
            get => ModType.Conversion;
            protected set => Type = value;
        }

        public override string Description
        {
            get => "将osu!谱转成指定键数的Mania谱";
            protected set => base.Description = value;
        }

        public virtual OsuGameMod LegacyMod => OsuGameMod.KeyMod;
    }

    public class Key1Mod : KeyMod
    {
        public override string Name
        {
            get => "Key1";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key1";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key1;
    }

    public class Key2Mod : KeyMod
    {
        public override string Name
        {
            get => "Key2";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key2";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key2;
    }

    public class Key3Mod : KeyMod
    {
        public override string Name
        {
            get => "Key3";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key3";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key3;
    }

    public class Key4Mod : KeyMod
    {
        public override string Name
        {
            get => "Key4";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key4";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key4;
    }

    public class Key5Mod : KeyMod
    {
        public override string Name
        {
            get => "Key5";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key5";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key5;
    }

    public class Key6Mod : KeyMod
    {
        public override string Name
        {
            get => "Key6";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key6";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key6;
    }

    public class Key7Mod : KeyMod
    {
        public override string Name
        {
            get => "Key7";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key7";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key7;
    }

    public class Key8Mod : KeyMod
    {
        public override string Name
        {
            get => "Key8";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key8";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key8;
    }

    public class Key9Mod : KeyMod
    {
        public override string Name
        {
            get => "Key9";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Key9";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.Key9;
    }

    public class KeyCoopMod : KeyMod
    {
        public override string Name
        {
            get => "KeyCoop";
            protected set => Name = value;
        }

        public override string ShortName
        {
            get => "Co-op";
            protected set => ShortName = value;
        }

        public override OsuGameMod LegacyMod => OsuGameMod.KeyCoop;

        public override double ScoreMultiplier
        {
            get => 1d;
            protected set => ScoreMultiplier = value;
        }

        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Osu)
                ScoreMultiplier = 0.9d;
            return beatmap;
        }
    }
}