using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Beatmaps.Classes.HitObject
{
    public static class HitObjectExtraMethod
    {
        public static int GetEndTime(this IHitObject hitObject)
        {
            if (hitObject is null) return 0;
            if (hitObject is IHasEndHitObject hasEnd)
                return hasEnd.EndTime;
            return hitObject.Offset;
        }
    }
}
