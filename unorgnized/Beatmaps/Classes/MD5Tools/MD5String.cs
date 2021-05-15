using System;
using System.Security.Cryptography;
using System.Text;

namespace osuTools.Beatmaps
{
    /// <summary>
    ///     将MD5以字符串的形式表示。
    /// </summary>
    [Serializable]
    public class MD5String
    {
        private string md5str = "";

        /// <summary>
        ///     使用MD5字符串构建新的MD5String对象。CurrentMD5将会被设置为null。
        /// </summary>
        /// <param name="md5"></param>
        public MD5String(string md5)
        {
            CurrentMD5 = null;
            md5str = md5;
        }

        /// <summary>
        ///     使用System.Security.Cryptography.MD5来构建一个新的MD5String。
        /// </summary>
        /// <param name="md5"></param>
        public MD5String(MD5 md5)
        {
            CurrentMD5 = md5;
        }

        /// <summary>
        ///     构造一个不包含MD5的MD5String
        /// </summary>
        public MD5String()
        {
            md5str = "";
            CurrentMD5 = new MD5CryptoServiceProvider();
        }

        /// <summary>
        ///     存储在类中的System.Security.Cryptography.MD5对象
        /// </summary>
        [field: NonSerialized]
        public MD5 CurrentMD5 { get; }

        /// <summary>
        ///     将System.Security.Cryptography.MD5中的Hash转化为字符串。
        /// </summary>
        /// <param name="md5">要转化的MD5对象</param>
        /// <returns>转换后的MD5字符串</returns>
        public static string GetString(MD5 md5)
        {
            if (md5.Hash.Length == 0) throw new NullReferenceException();
            var stringBuilder = new StringBuilder();
            foreach (var b in md5.Hash) stringBuilder.Append(b.ToString("x2"));
            return stringBuilder.ToString();
        }
        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (!(CurrentMD5 is null)) return CurrentMD5.GetHashCode();

            if (!string.IsNullOrEmpty(md5str))
                return md5str.GetHashCode();
            return base.GetHashCode();
        }
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is MD5String m)
                return md5str == m.md5str || CurrentMD5 == m.CurrentMD5;
            return obj.Equals(this);
        }

        /// <summary>
        ///     将一个byte数组转换成MD5字符串。
        /// </summary>
        /// <param name="md5">要转换的字符数组</param>
        /// <returns>转换后的MD5字符串</returns>
        public static string GetString(byte[] md5)
        {
            var stringBuilder = new StringBuilder();
            foreach (var b in md5) stringBuilder.Append(b.ToString("x2"));
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     返回其中存储的转化为字符串的MD5
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            ConvertToString();
            return md5str;
        }

        private void ConvertToString()
        {
            if (string.IsNullOrEmpty(md5str))
            {
                try
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var b in CurrentMD5.Hash) stringBuilder.Append(b.ToString("x2"));
                    md5str = stringBuilder.ToString();
                }
                catch (NullReferenceException)
                {
                    md5str = string.Empty;
                }

                
            }
        }

        /// <summary>
        ///     使用两个MD5String判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator ==(MD5String obja, MD5String objb)
        {
            if (obja is null && objb is null) 
                return true;
            if (obja is null || objb is null)
                return false;
            if (String.Compare(obja.ToString(), objb.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
                return false;
            return true;
        }

        /// <summary>
        ///     使用两个MD5String判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator !=(MD5String obja, MD5String objb)
        {
            if (obja is null && objb is null)
                return false;
            if (obja is null || objb is null)
                return true;
            if (String.Compare(obja.ToString(), objb.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                return false;
            return true;
        }

        /// <summary>
        ///     使用MD5字符串判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator ==(MD5String obja, string objb)
        {
            if (obja is null && string.IsNullOrEmpty(objb)) 
                return true;
            if (obja is null || string.IsNullOrEmpty(objb))
                return false;
            if (string.Compare(obja.ToString(), objb, StringComparison.OrdinalIgnoreCase) != 0)
                return false;
            return true;
        }

        /// <summary>
        ///     使用MD5字符串判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator !=(MD5String obja, string objb)
        {
            if (obja is null && string.IsNullOrEmpty(objb))
                return false;
            if (obja is null || string.IsNullOrEmpty(objb))
                return true;
            if (string.Compare(obja.ToString(), objb, StringComparison.OrdinalIgnoreCase) == 0)
                return false;
            return true;
        }
    }
}