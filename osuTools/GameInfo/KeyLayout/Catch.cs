using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using osuTools.ExtraMethods;

namespace osuTools.KeyLayouts
{
    /// <summary>
    ///     CTB模式的按键
    /// </summary>
    public class CatchKeyLayout
    {
        internal List<string> InternalName =
            new List<string>(new[] {"keyFruitsLeft", "keyFruitsRight", "keyFruitsDash"});

        private readonly Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        private readonly string[] lines;

        /// <summary>
        ///     将字符串解析成CTB键位
        /// </summary>
        /// <param name="data"></param>
        public CatchKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     从配置文件中读取CTB键位
        /// </summary>
        /// <param name="ConfigFile"></param>
        public CatchKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     CTB模式的键位
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
            catch
            {
            }
        }

        private void InitKeyLayout()
        {
            KeyLayout = new Dictionary<string, Keys>();
            KeyLayout.Add("Left", Keys.Left);
            KeyLayout.Add("Right", Keys.Right);
            KeyLayout.Add("Dash", Keys.LShiftKey);
        }

        private void Parse()
        {
            foreach (var data in lines)
            {
                if (data.StartsWith("keyFruitsLeft"))
                    KeyLayout["Left"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyFruitsRight"))
                    KeyLayout["Right"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyFruitsDash"))
                    KeyLayout["Dash"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
            }
        }
    }
}