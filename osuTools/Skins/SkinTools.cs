using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace osuTools.Skins
{
    /// <summary>
    /// </summary>
    public static class SkinTools
    {
        /// <summary>
        ///     字符串转枚举
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T StringToEnum<T>(string str)
        {
            return (T) Enum.Parse(typeof(T), str);
        }

        /// <summary>
        ///     通过文件名或不带序号的文件名搜索皮肤中包含的<see cref="Interfaces.ISkinObject" />
        /// </summary>
        /// <param name="s"></param>
        /// <param name="frontFileName"></param>
        /// <returns></returns>
        public static List<string> GetMultipleFileSkinObject(Skin s, string frontFileName)
        {
            var pattern = "*.*";
            var dir = s.ConfigFileDirectory.Replace("skin.ini", "");
            var lst = Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly);
            var files = new List<string>();
            foreach (var n in lst)
            {
                var fileName = Path.GetFileNameWithoutExtension(n);
                if (fileName.Remove(fileName.Length - 1) == frontFileName)
                {
                    if (fileName.Last().IsDigit())
                    {
                        files.Add(n);
                    }
                }
                else if (fileName == n)
                {
                    files.Add(n);
                }
            }

            if (files.Count == 0)
                files.Add("default");
            return files;
        }

        /// <summary>
        ///     通过文件名或不带序号的文件名搜索皮肤中包含的<see cref="Interfaces.ISkinObject" />
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="frontFileName"></param>
        /// <returns></returns>
        public static List<string> GetMultipleFileSkinObject(string[] fileList, string frontFileName)
        {
            var lst = fileList;
            var files = new List<string>();
            foreach (var n in lst)
            {
                var fileName = Path.GetFileNameWithoutExtension(n);
                if (fileName.Remove(fileName.Length - 1) == "-") fileName = fileName.Remove(fileName.Length - 1);
                if (fileName.Remove(fileName.Length - 1) == frontFileName)
                {
                    if (fileName.Last().IsDigit())
                    {
                        files.Add(n);
                    }
                }
                else if (fileName == frontFileName)
                {
                    files.Clear();
                    files.Add(n);
                }
            }

            if (files.Count == 0)
                //Debug.WriteLine($"Target file \"{frontFileName}\" can not be found with pattern {pattern}. Fallback method has invoked.");
                files.Add("default");

            return files;
        }
    }
}