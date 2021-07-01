using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using osuTools.Game.Mods;

namespace osuTools.GameInfo.KeyLayout
{
    /// <summary>
    ///     Mod对应的快捷键
    /// </summary>
    public class ModsKeyLayout
    {
        internal List<string> InternalName = new List<string>(new[]
        {
            "keyEasy", "keyNoFail", "keyHalfTime", "keyHardRock", "keySuddenDeath", "keyDoubleTime", "keyHidden",
            "keyFlashlight", "keyRelax", "keyAutopilot", "keySpunOut", "keyAuto"
        });

        private readonly Dictionary<string, Keys> _keyandint = new Dictionary<string, Keys>();
        private readonly string[] _lines;
        private readonly Dictionary<string, OsuGameMod> _modlist = new Dictionary<string, OsuGameMod>();
        private readonly Dictionary<OsuGameMod, Keys> _mods = new Dictionary<OsuGameMod, Keys>();

        internal List<string> Name = new List<string>(new[]
        {
            "Easy", "NoFail", "HalfTime", "HardRock", "SuddenDeath", "DoubleTime", "Hidden",
            "Flashlight", "Relax", "AutoPilot", "SpunOut", "AutoPlay"
        });

        /// <summary>
        ///     使用字符串数组构建Mods和Keys的键值对
        /// </summary>
        /// <param name="data"></param>
        public ModsKeyLayout(string[] data)
        {
            InitKeys();
            _lines = data;
            InitModList();
            GetKeys();
        }

        /// <summary>
        ///     从配置文件读取并构建Mods和Keys的键值对
        /// </summary>
        /// <param name="configFile"></param>
        public ModsKeyLayout(string configFile)
        {
            InitKeys();
            _lines = File.ReadAllLines(configFile);
            InitModList();
            GetKeys();
        }

        private void InitModList()
        {
            var values = Enum.GetValues(typeof(OsuGameMod));
            foreach (var modstr in Name)
            foreach (var mod in values)
                if (mod.ToString() == modstr)
                    _modlist.Add(modstr, (OsuGameMod) mod);
        }

        /// <summary>
        ///     获取指定Mod对应的键位
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public Keys GetKey(OsuGameMod mod)
        {
            if (mod.ToString().Contains("Key")) return _mods.CheckIndexAndGetValue(OsuGameMod.Relax);
            return _mods.CheckIndexAndGetValue(mod);
        }

        private string ModConvert(string mod)
        {
            return _modlist.CheckIndexAndGetValue(mod.Trim().Replace("key", "")).ToString();
        }

        private void InitKeys()
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

        private void GetKeys()
        {
            foreach (var data in _lines)
            foreach (var name in InternalName)
            {
                var tmp = data.Split('=');
                if (tmp[0].Trim().Replace("key", "") == ModConvert(name))
                {
                    var tmpmod = ModConvert(tmp[0].Trim());
                    var isValid = tmpmod != OsuGameMod.Unknown.ToString();
                    if (isValid)
                        _mods.Add(_modlist.CheckIndexAndGetValue(tmpmod.Trim()),
                            _keyandint.CheckIndexAndGetValue(tmp[1].Trim()));
                }
            }
        }
    }
}