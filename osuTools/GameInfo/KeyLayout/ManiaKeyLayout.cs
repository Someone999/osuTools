using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace osuTools.GameInfo.KeyLayout
{
    /// <summary>
    ///     Mania模式的按键，1-9k，只有设置过按键的会出现在设置里，其余的按照默认值处理。
    /// </summary>
    public class ManiaKeyLayout
    {
        private readonly Dictionary<string, Keys> _keyandint = new Dictionary<string, Keys>();
        private Dictionary<int, List<Keys>> _layout;
        private List<KeyValuePair<int, List<Keys>>> _layouts;

        private readonly string[] _lines;

        /// <summary>
        ///     从配置文件中读取Mania的键位信息
        /// </summary>
        /// <param name="configFile"></param>
        public ManiaKeyLayout(string configFile)
        {
            _lines = File.ReadAllLines(configFile);
            InitKeysDict();
            InitLayoutDict();
            InitPair();
            Parse();
        }

        /// <summary>
        ///     从数据行解析Manua的键位信息
        /// </summary>
        /// <param name="data"></param>
        public ManiaKeyLayout(string[] data)
        {
            _lines = data;
            InitKeysDict();
            InitLayoutDict();
            InitPair();
            Parse();
        }

        /// <summary>
        ///     1K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key1 => _layouts[0];

        /// <summary>
        ///     2K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key2 => _layouts[1];

        /// <summary>
        ///     3K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key3 => _layouts[2];

        /// <summary>
        ///     4K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key4 => _layouts[3];

        /// <summary>
        ///     5K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key5 => _layouts[4];

        /// <summary>
        ///     6K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key6 => _layouts[5];

        /// <summary>
        ///     7K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key7 => _layouts[6];

        /// <summary>
        ///     8K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key8 => _layouts[7];

        /// <summary>
        ///     9K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key9 => _layouts[8];

        private void InitKeysDict()
        {
            var values = Enum.GetValues(typeof(Keys));
            var names = Enum.GetNames(typeof(Keys));
            try
            {
                for (var i = 0; i < values.Length; i++)
                {
                    var tmp =
                        names[i] == "LeftShift" ? "LShiftKey" :
                        names[i] == "RightShift" ? "RShiftKey" :
                        names[i] == "LeftControl" ? "LControlKey" :
                        names[i] == "RightControl" ? "RControlKey" :
                        names[i] == "LeftAlt" ? "LMenu" :
                        names[i] == "RightAlt" ? "RMenu" : names[i];

                    _keyandint.Add(tmp, (Keys) values.GetValue(i));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitPair()
        {
            _layouts = GetFromDict(_layout);
        }

        private void InitLayoutDict()
        {
            _layout = new Dictionary<int, List<Keys>>
            {
                {1, new List<Keys>(new[] {Keys.Space})},
                {2, new List<Keys>(new[] {Keys.F, Keys.J})},
                {3, new List<Keys>(new[] {Keys.F, Keys.Space, Keys.J})},
                {4, new List<Keys>(new[] {Keys.D, Keys.F, Keys.J, Keys.K})},
                {5, new List<Keys>(new[] {Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K})},
                {6, new List<Keys>(new[] {Keys.S, Keys.D, Keys.F, Keys.J, Keys.K, Keys.L})},
                {7, new List<Keys>(new[] {Keys.S, Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K, Keys.L})},
                {
                    8, new List<Keys>(
                        new[] {Keys.LShiftKey, Keys.S, Keys.D, Keys.F, Keys.J, Keys.K, Keys.L, Keys.OemSemicolon})
                },
                {
                    9,
                    new List<Keys>(new[]
                    {
                        Keys.LShiftKey, Keys.S, Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K, Keys.L,
                        Keys.OemSemicolon
                    })
                }
            };
        }

        internal List<KeyValuePair<int, List<Keys>>> GetFromDict(Dictionary<int, List<Keys>> dict)
        {
            var lst = new List<KeyValuePair<int, List<Keys>>>();
            foreach (var kvp in dict) lst.Add(kvp);
            return lst;
        }

        private void Parse()
        {
            foreach (var data in _lines)
            {
                var tmp = new List<Keys>();
                var keystr = "ManiaLayouts";

                if (data.Trim().StartsWith(keystr))
                {
                    var keys = data.Trim().Split('=');
                    var regex = new Regex("\\d");
                    var keycount = int.Parse(regex.Match(keys[0]).Value);
                    if (keys.Length > 1)
                    {
                        var keylayout = keys[1].Split(' ');
                        foreach (var maniakey in keylayout)
                        {
                            var tp = maniakey;
                            if (string.IsNullOrWhiteSpace(maniakey))
                                continue;

                            tmp.Add(_keyandint.CheckIndexAndGetValue(tp.Trim()));
                        }

                        _layout[keycount - 1] = tmp;
                    }
                }

                InitPair();
            }
        }
    }
}