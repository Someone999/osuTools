using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using osuTools.Game.Mods;
using osuTools.Skins.Color;

namespace osuTools
{
    /// <summary>
    ///     扩展方法
    /// </summary>
    public static class ExtraMethod
    {
        /// <summary>
        ///     将Mod数组转换成ModList
        /// </summary>
        /// <param name="modarr"></param>
        /// <returns></returns>
        public static ModList ToModList(this Mod[] modarr)
        {
            return ModList.FromModArray(modarr);
        }
        /// <summary>
        /// 将字符串转换成<seealso cref="Nullable{DateTime}"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string s)
        {
            return DateTime.TryParse(s, out var d) ? (DateTime?) d : null;
        }
        /// <summary>
        /// 将字符串转换成DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            return DateTime.Parse(s);
        }

        /// <summary>
        /// 将整数化成bool，0为false，非0为true
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToBool(this int i)
        {
            return Convert.ToBoolean(i);
        }
        /// <summary>
        /// 将字符串转换为bool，不分大小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            return Convert.ToBoolean(s == "1" || string.Equals(s, "True", StringComparison.OrdinalIgnoreCase)
                ? "True"
                : "False");
        }
        /// <summary>
        /// 将字符串转换成<see cref="Nullable{Boolean}"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool? ToNullableBool(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (bool?) Convert.ToBoolean(i == "1" ? "True" : "False");
        }
        /// <summary>
        /// 将字符串转换成int
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int ToInt32(this string i)
        {
            return int.Parse(i);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{Int32}"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int? ToNullableInt32(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (int?) int.Parse(i);
        }

        /// <summary>
        ///将字符串转换成uint
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string i)
        {
            return uint.Parse(i);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{UInt32}"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static uint? ToNullableUInt32(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (uint?) uint.Parse(i);
        }
        /// <summary>
        ///将字符串转换成double
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static double ToDouble(this string i)
        {
            return double.Parse(i);
        }
        /// <summary>
        ///将字符串转换成<seealso cref="Nullable{Double}"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static double? ToNullableDouble(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (double?) double.Parse(i);
        }
        /// <summary>
        /// 将使用任意分隔符隔开的3个数字转换成<seealso cref="RgbColor"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static RgbColor ToRgbColor(this string i)
        {
            return RgbColor.Parse(i);
        }
        /// <summary>
        /// 将使用任意分隔符隔开的4个数字转换成<seealso cref="RgbColor"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static RgbaColor ToRgbaColor(this string i)
        {
            return RgbaColor.Parse(i);
        }
        /// <summary>
        /// 判断字符是不是数字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        internal static Keys CheckIndexAndGetValue(this Dictionary<string, Keys> var, string index)
        {
            try
            {
                var tmp =
                    index == "LeftShift" ? "LShiftKey" :
                    index == "RightShift" ? "RShiftKey" :
                    index == "LeftControl" ? "LControlKey" :
                    index == "RightControl" ? "RControlKey" :
                    index == "LeftAlt" ? "LMenu" :
                    index == "RightAlt" ? "RMenu" : index;
                return var[tmp];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"找不到键位:{index}");
                return (Keys) (-1);
            }
        }

        internal static OsuGameMod CheckIndexAndGetValue(this Dictionary<string, OsuGameMod> var, string index)
        {
            try
            {
                if (index == "Auto") index = "AutoPlay";
                if (index == "Autopilot") index = "AutoPilot";
                if (index.StartsWith("key")) return var[index.Replace("key", "")];
                return var[index];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"不支持的Mod:{index}");
                return OsuGameMod.Unknown;
            }
        }

        internal static Keys CheckIndexAndGetValue(this Dictionary<OsuGameMod, Keys> var, OsuGameMod index)
        {
            try
            {
                var tmp = index;
                if (index.ToString().Contains("Key")) tmp = OsuGameMod.Relax;
                if (index == OsuGameMod.NightCore) tmp = OsuGameMod.DoubleTime;
                if (index == OsuGameMod.Perfect) tmp = OsuGameMod.SuddenDeath;
                if (index == OsuGameMod.FadeIn) tmp = OsuGameMod.Hidden;
                if (index == OsuGameMod.Random) tmp = OsuGameMod.AutoPilot;
                return var[tmp];
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"不支持的Mod:{index}");
                return (Keys) (-1);
            }
        }
        /// <summary>
        /// 将字符串转换成字节数组，可以指定编码器，默认为UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(str);
        }
        /// <summary>
        /// 将字节数组转换成字符串，可以指定编码器，默认为UTF8
        /// </summary>
        /// <param name="b"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] b, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(b);
        }
    }
}