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

        private readonly Dictionary<string, Keys> _keyandint = new Dictionary<string, Keys>();
        private readonly string[] _lines;

        /// <summary>
        ///     使用格式正确的字符数组构造一个KeyLayout
        /// </summary>
        /// <param name="data">要使用的字符数组</param>
        public TaikoKeyLayout(string[] data)
        {
            _lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     从配置文件读取
        /// </summary>
        /// <param name="configFile">配置文件的目录</param>
        public TaikoKeyLayout(string configFile)
        {
            _lines = File.ReadAllLines(configFile);
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
                {"RedLeft", Keys.X}, {"RedRight", Keys.C}, {"BlueLeft", Keys.Z}, {"BlueRight", Keys.V}
            };
        }

        private void Parse()
        {
            foreach (var data in _lines)
            {
                if (data.StartsWith("keyTaikoInnerLeft"))
                    KeyLayout["RedLeft"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoInnerRight"))
                    KeyLayout["RedRight"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoOuterLeft"))
                    KeyLayout["BlueLeft"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyTaikoOuterRight"))
                    KeyLayout["BlueRight"] = _keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
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