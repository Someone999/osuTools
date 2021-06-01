using System;
using System.Security.Cryptography;
using System.Text;
using osuTools.Beatmaps.BreakTime;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        [NonSerialized] private StringBuilder _b = new StringBuilder();

        private BreakTimeCollection _breakTimes;

        private HitObjectCollection _hitObjects;

        private int _m;

        [NonSerialized] private MD5CryptoServiceProvider _md5Calc = new MD5CryptoServiceProvider();

        private bool _modeHasSet;

    }
}