using System.Collections.Generic;

namespace osuTools.Game.Mods
{
    /// <summary>
    ///     Mod的比较器
    /// </summary>
    internal class ModEqulityComparer : IEqualityComparer<Mod>
    {
        public bool Equals(Mod a, Mod b)
        {
            return a == b;
        }

        public int GetHashCode(Mod m)
        {
            return m.GetHashCode();
        }
    }
}