using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace osuTools.GameInfo.KeyLayout
{
    /// <summary>
    ///     CTB模式的按键
    /// </summary>
    public class CatchKeyLayout
    {
        internal List<string> InternalName =
            new List<string>(new[] {"keyFruitsLeft", "keyFruitsRight", "keyFruitsDash"});

        private readonly Dictionary<string, Keys> _keyandint = new Dictionary<string, Keys>();
        private readonly string[] _lines;

        /// <summary>
        ///     将字符串解析成CTB键位
        /// </summary>
        /// <param name="data"></param>
        public CatchKeyLayout(string[] data)
        {
            _lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     从配置文件中读取CTB键位
        /// </summary>
        /// <param name="configFile"></param>
        public CatchKeyLayout(string configFile)
        {
            _lines = File.ReadAllLines(configFile);
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
                for (var i = 0; i < values.Length; i++) _keyandint.Add(names[i], (Keys) values.GetValue(i));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitKeyLayout()
        {
            KeyLayout = new Dictionary<string, Keys>
            {
                {"Left", Keys.Left}, {"Right", Keys.Right}, {"Dash", Keys.LShiftKey}
            };
        }

        private void Parse()
        {
            foreach (var data in _lines)
            {
                if (data.StartsWith("keyFruitsLeft"))
                    KeyLayout["Left"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyFruitsRight"))
                    KeyLayout["Right"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyFruitsDash"))
                    KeyLayout["Dash"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
            }
        }
    }
}