using System.Collections.Generic;

namespace osuTools.Replays
{
    internal static class Ex
    {
        public static bool Contains(this IReadOnlyList<OsuGameMod> mods, OsuGameMod mod)
        {
            foreach (var moda in mods)
                if (mod == moda)
                    return true;
            return false;
        }
    }
}