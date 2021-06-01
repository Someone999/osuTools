using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace osuTools.GameInfo.KeyLayout
{
    /// <summary>
    ///     Taiko模式的按键
    /// </summary>
    public class TaikoKeyLayout
    {
        internal List<string> InternalName = new List<string>(new[]
            {"keyTaikoInnerLeft", "keyTaikoInnerRight", "keyTaikoOuterLeft", "keyTaikoOuterRight"});

        private readonly Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        private readonly string[] lines;

        /// <summary>
        ///     使用格式正确的字符数组构造一个KeyLayout
        /// </summary>
        /// <param name="data">要使用的字符数组</param>
        public TaikoKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     从配置文件读取
        /// </summary>
        /// <param name="ConfigFile">配置文件的目录</param>
        public TaikoKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     键位
        /// </summary>
        public Dictionary<string, Keys> KeyLayout { get; private set; }

        private void InitKeysDict()
        {
            var values = Enum.GetValues(typeof(Keys));
            var names = Enum.GetNames(typeof(Keys));
            try
            {
                for (var i = 0; i < values.Length; i++) keyandint.Add(names[i], (Keys) values.GetValue(i));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitKeyLayout()
        {
            KeyLayout = new Dictionary<string, Keys>();
            KeyLayout.Add("RedLeft", Keys.X);
            KeyLayout.Add("RedRight", Keys.C);
            KeyLayout.Add("BlueLeft", Keys.Z);
            KeyLayout.Add("BlueRight", Keys.V);
        }

        private void Parse()
        {
            foreach (var data in lines)
            {
                if (data.StartsWith("keyTaikoInnerLeft"))
                    KeyLayout["RedLeft"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoInnerRight"))
                    KeyLayout["RedRight"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoOuterLeft"))
                    KeyLayout["BlueLeft"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoOuterRight"))
                    KeyLayout["BlueRight"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
            }
        }

        /// <summary>
        ///     Taiko的键位信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var tmp = "";
            for (var i = 0; i < KeyLayout.Count; i++)
                if (i + 1 != KeyLayout.Count)
                    tmp += KeyLayout.Values + " ";
                else
                    tmp += KeyLayout.Values.ToString();
            return tmp;
        }
    }
}