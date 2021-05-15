using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace osuTools.GameInfo.KeyLayout
{
    /// <summary>
    ///     std模式的按键
    /// </summary>
    public class OsuKeyLayout
    {
        internal List<string> InternalName = new List<string>(new[] {"keyOsuLeft", "keyOsuRight", "keyOsuSmoke"});
        private readonly Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        private readonly string[] lines;

        /// <summary>
        ///     使用包含按键布局的字符串初始化一个OsuKeyLayout
        /// </summary>
        /// <param name="data"></param>
        public OsuKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     使用配置文件初始化osu的按键布局
        /// </summary>
        /// <param name="ConfigFile"></param>
        public OsuKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }

        /// <summary>
        ///     osu模式的按键布局
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
            KeyLayout = new Dictionary<string, Keys> {{"Left", Keys.Z}, {"Right", Keys.X}, {"Smoke", Keys.C}};
        }

        private void Parse()
        {
            foreach (var data in lines)
            {
                if (data.StartsWith("keyOsuLeft"))
                    KeyLayout["Left"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyOsuRight"))
                    KeyLayout["Right"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                if (data.StartsWith("keyOsuSmoke"))
                    KeyLayout["Smoke"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
            }
        }
    }
}