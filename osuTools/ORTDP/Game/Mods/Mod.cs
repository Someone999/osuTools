using System;
using System.Linq;
using System.Reflection;
using osuTools.Beatmaps;
using osuTools.ExtraMethods;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    /// <summary>
    ///     Mod的信息
    /// </summary>
    public abstract class Mod
    {
        /// <summary>
        ///     所有模式公用的可用Mod
        /// </summary>
        public static Mod[] GenericAvailableMods { get; } =
        {
            new EasyMod(), new NoFailMod(), new HalfTimeMod(),
            new HardRockMod(), new SuddenDeathMod(), new PerfectMod(), new DoubleTimeMod(), new NightCoreMod(),
            new HiddenMod(), new FlashlightMod(),
            new AutoPlayMod(), new CinemaMod(), new ScoreV2Mod()
        };

        /// <summary>
        ///     Osu模式的可用Mod
        /// </summary>
        public static Mod[] OsuMods { get; } = new Mod[]
        {
            new RelaxMod(), new AutoPilotMod(), new SpunOutMod()
        }.Concat(GenericAvailableMods).ToArray();

        /// <summary>
        ///     Taiko模式的可用Mod
        /// </summary>
        public static Mod[] TaikoMods { get; } = new Mod[] {new RelaxMod()}.Concat(GenericAvailableMods).ToArray();

        /// <summary>
        ///     Catch(CTB)模式的可用Mod
        /// </summary>
        public static Mod[] CatchMods { get; } = new Mod[] {new RelaxMod()}.Concat(GenericAvailableMods).ToArray();

        /// <summary>
        ///     Mania模式的可用Mod
        /// </summary>
        public static Mod[] ManiaMods { get; } = new Mod[]
        {
            new FadeInMod(),
            new KeyMod(), new RandomMod(), new MirrorMod()
        }.Concat(GenericAvailableMods).ToArray();

        /// <summary>
        ///     Mod的名字
        /// </summary>
        public virtual string Name { get; protected set; } = "";

        /// <summary>
        ///     Mod的短名称
        /// </summary>
        public virtual string ShortName { get; protected set; } = "";

        /// <summary>
        ///     Mod对分数的影响
        /// </summary>
        public virtual double ScoreMultiplier { get; protected set; } = 1d;

        /// <summary>
        ///     Mod的类型
        /// </summary>
        public virtual ModType Type { get; protected set; } = ModType.Fun;

        /// <summary>
        ///     Mod的描述
        /// </summary>
        public virtual string Description { get; protected set; } = "";

        /// <summary>
        ///     开启该Mod后，得分是否会上传，默认为true
        /// </summary>
        public virtual bool IsRankedMod { get; protected set; } = true;

        /// <summary>
        ///     将Mod应用到谱面
        /// </summary>
        /// <param name="beatmap"></param>
        /// <returns></returns>
        public virtual Beatmap Apply(Beatmap beatmap)
        {
            return beatmap;
        }

        /// <summary>
        ///     在特定的模式上设置Mod
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public virtual bool CheckAndSetForMode(GameMode mode)
        {
            if (mode.AvaliableMods.ToModList().HasMod(this))
                return true;
            return false;
        }

        /// <summary>
        ///     从Mod枚举获取Mod对象
        /// </summary>
        /// <param name="legacyMod"></param>
        /// <returns></returns>
        public static Mod FromLegacyMod(OsuGameMod legacyMod)
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
                if (type.GetInterfaces().Any(i => i == typeof(ILegacyMod)))
                {
                    var mod = (ILegacyMod) type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    if (legacyMod == mod.LegacyMod)
                        return (Mod) mod;
                }

            return null;
        }

        public static bool operator ==(Mod a, Mod b)
        {
            if ((object) a == null && (object) b != null)
                return false;
            if ((object) a != null && (object) b == null)
                return false;
            if ((object) a == null && (object) b == null)
                return true;
            if (a is ILegacyMod && b is ILegacyMod)
                return ((ILegacyMod) a).LegacyMod == ((ILegacyMod) b).LegacyMod;
            return a.Name == b.Name;
        }

        public static bool operator !=(Mod a, Mod b)
        {
            if ((object) a == null && (object) b != null)
                return true;
            if ((object) a != null && (object) b == null)
                return true;
            if ((object) a == null && (object) b == null)
                return false;
            if (a is ILegacyMod && b is ILegacyMod)
                return ((ILegacyMod) a).LegacyMod != ((ILegacyMod) b).LegacyMod;
            return a.Name != b.Name;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        ///     获取Mod的Hash。如果一个Mod是<see cref="ILegacyMod" />，将返回Mod的枚举值。否则返回Mod的名字的Hash。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (this is ILegacyMod)
                return (int) (this as ILegacyMod).LegacyMod;
            return Name.GetHashCode();
        }
    }
}