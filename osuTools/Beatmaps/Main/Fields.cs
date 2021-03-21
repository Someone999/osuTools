using System;
using System.Security.Cryptography;
using System.Text;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        [NonSerialized] private StringBuilder b = new StringBuilder();

        private BreakTimeCollection breakTimes;

        private HitObjectCollection hitObjects;

        private int id = -2048,
            setid = -2048;

        private int m;

        [NonSerialized] private MD5CryptoServiceProvider md5calc = new MD5CryptoServiceProvider();

        private bool ModeHasSet;

        private double
            od,
            cs,
            hp,
            ar,
            stars;

        private string t = "",
            ut = "",
            a = "",
            ua = "",
            c = "",
            dif = "",
            ver = "",
            au = "",
            sou = "",
            tag = "",
            mak = "",
            vi = "",
            fuvi = "";
    }
}