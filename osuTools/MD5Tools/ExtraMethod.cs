using System.Security.Cryptography;

namespace osuTools.MD5Tools
{
    internal static class ExtraMethod
    {
        public static MD5String GetMd5String(this MD5 md5)
        {
            return new MD5String(md5);
        }
    }
}