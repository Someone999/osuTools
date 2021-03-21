using System;
using System.Collections.Generic;
using System.Windows.Forms;
using osuTools.ExtraMethods;
using osuTools.KeyLayouts;

namespace osuTools.KeyBindings
{
    /// <summary>
    ///     存储所有的快捷键及按键信息。
    /// </summary>
    public class KeyBinding
    {
        private readonly Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        private readonly string[] lines;

        /// <summary>
        ///     将字符串数组解析成键位信息
        /// </summary>
        /// <param name="data"></param>
        public KeyBinding(string[] data)
        {
            Init();
            lines = data;
            ManiaKeyLayouts = new ManiaKeyLayout(lines);
            OsuKeyLayouts = new OsuKeyLayout(lines);
            CatchKeyLayouts = new CatchKeyLayout(lines);
            TaikoKeyLayouts = new TaikoKeyLayout(lines);
            ModKeyLayouts = new ModsKeyLayout(lines);
            Parse();
        }

        /// <summary>
        ///     Mods的按键绑定
        /// </summary>
        public ModsKeyLayout ModKeyLayouts { get; }

        /// <summary>
        ///     Mania模式的键位
        /// </summary>
        public ManiaKeyLayout ManiaKeyLayouts { get; }

        /// <summary>
        ///     CTB模式的键位
        /// </summary>
        public CatchKeyLayout CatchKeyLayouts { get; }

        /// <summary>
        ///     Osu模式的键位
        /// </summary>
        public OsuKeyLayout OsuKeyLayouts { get; }

        /// <summary>
        ///     Taiko模式的键位
        /// </summary>
        public TaikoKeyLayout TaikoKeyLayouts { get; }

        /// <summary>
        ///     绑定的快捷键
        /// </summary>
        public Dictionary<string, Keys> Bindings { get; private set; }

        private void Init()
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

        private void Parse()
        {
            Bindings = new Dictionary<string, Keys>();
            foreach (var data in lines)
            {
                var tmp = data.Split('=');
                if (tmp.Length > 1)
                {
                    if (tmp[1].Trim().Split(' ').Length > 1)
                    {
                    }
                    else
                    {
                        var Invalid = !keyandint.ContainsKey(tmp[1].Trim());
                        var IsLayOutKey = OsuKeyLayouts.InternalName.Contains(tmp[0].Trim()) ||
                                          CatchKeyLayouts.InternalName.Contains(tmp[0].Trim()) ||
                                          TaikoKeyLayouts.InternalName.Contains(tmp[0].Trim()) ||
                                          ModKeyLayouts.InternalName.Contains(tmp[0].Trim());
                        if (Invalid || IsLayOutKey)
                            continue;
                        Bindings.Add(tmp[0], keyandint.CheckIndexAndGetValue(tmp[1].Trim()));
                    }
                }
            }
        }
    }
}