﻿using System.Security.Cryptography;

namespace osuTools.Beatmaps
{
    internal static class ExtraMethod
    {
        public static MD5String GetMD5String(this MD5 md5)
        {
            return new MD5String(md5);
        }
    }
}