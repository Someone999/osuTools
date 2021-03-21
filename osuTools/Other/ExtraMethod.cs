using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using osuTools.Game.Mods;
using osuTools.Skins.Colors;

namespace osuTools.ExtraMethods
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

        public static DateTime? ToNullableDateTime(this string s)
        {
            DateTime d;
            return DateTime.TryParse(s, out d) ? (DateTime?) d : null;
        }

        public static DateTime ToDateTime(this string s)
        {
            return DateTime.Parse(s);
        }

        public static bool ToBool(this int i)
        {
            return Convert.ToBoolean(i);
        }

        public static bool ToBool(this string i)
        {
            return Convert.ToBoolean(i == "1" || string.Equals(i, "True", StringComparison.OrdinalIgnoreCase)
                ? "True"
                : "False");
        }

        public static bool? ToNullableBool(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (bool?) Convert.ToBoolean(i == "1" ? "True" : "False");
        }

        public static int ToInt32(this string i)
        {
            return int.Parse(i);
        }

        public static int? ToNullableInt32(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (int?) int.Parse(i);
        }

        public static uint ToUInt32(this string i)
        {
            return uint.Parse(i);
        }

        public static uint? ToNullableUInt32(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (uint?) uint.Parse(i);
        }

        public static double ToDouble(this string i)
        {
            return double.Parse(i);
        }

        public static double? ToNullableDouble(this string i)
        {
            return string.IsNullOrEmpty(i) ? null : (double?) double.Parse(i);
        }

        public static RGBColor ToRGBColor(this string i)
        {
            return RGBColor.Parse(i);
        }

        public static RGBAColor ToRGBAColor(this string i)
        {
            return RGBAColor.Parse(i);
        }

        public static bool IsDigit(this char c)
        {
            return c >= '0' && c <= '9';
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

        public static byte[] ToBytes(this string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(str);
        }

        public static string GetString(this byte[] b, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(b);
        }
    }

    public static class ObjectCopy
    {
        public static object DeepCopy(object obj)
        {
            MemoryStream mstream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mstream,obj);
            mstream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(mstream);
        }
        public static T DeepCopy<T>(T obj)
        {
            MemoryStream mstream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mstream, obj);
            mstream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(mstream);
        }
    }
}