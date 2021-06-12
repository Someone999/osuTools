using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.Exceptions;
using osuTools.Skins;
using Sync.Tools;

namespace osuTools.GameInfo
{

    /// <summary>
    ///     表示最基本的游戏信息
    /// </summary>
    [Obsolete("这个类型已经过时，请使用新的类型",true)]
    public class ObsoletedOsuInfo
    {
        [DllImport("kernel32")]
        private static extern bool IsWow64Process(IntPtr hProcess, ref bool wow64Process);
        private string _cfg;
        private KeyBinding.KeyBinding _d;
        private string[] _lines;
        private int _off = -2;
        private bool _running;
        private Skin _sk;
        private string _ver, _song, _osudir, _username, _skin;

        /// <summary>
        ///     初始化一个OsuInfo对象
        /// </summary>
        public ObsoletedOsuInfo()
        {
            Init();
        }

        /// <summary>
        ///     获取当前的皮肤，尚未完工。
        /// </summary>
        public Skin CurrentSkin
        {
            get
            {
                _sk = new Skin(CurrentSkinDir);
                return _sk;
            }
        }

        /// <summary>
        ///     快捷键的获取，实验功能。
        /// </summary>
        public KeyBinding.KeyBinding ShortcutKeys => _d ?? (_d = new KeyBinding.KeyBinding(_lines));

        /// <summary>
        ///     osu!的窗口句柄
        /// </summary>
        public IntPtr WindowHanle => CurrentOsuProcess.MainWindowHandle;

        /// <summary>
        ///     osu!的进程句柄
        /// </summary>
        public IntPtr ProcessHandle => CurrentOsuProcess.Handle;

        /// <summary>
        ///     osu!当前的窗口标题
        /// </summary>
        public string Title => CurrentOsuProcess.MainWindowTitle;

        /// <summary>
        ///     osu!的PID
        /// </summary>
        public int ProcessId => CurrentOsuProcess.Id;

        /// <summary>
        ///     当前存放osu!进程信息的System.Diagnostics.Process对象
        /// </summary>
        public Process CurrentOsuProcess { get; private set; }

        /// <summary>
        ///     osu!所在文件夹的全路径。
        /// </summary>
        public string OsuDirectory
        {
            get
            {
                if (CurrentOsuProcess != null) return CurrentOsuProcess.MainModule?.FileName.Replace("osu!.exe", "") ?? "";
                return _osudir;
            }
        }

        /// <summary>
        ///     获取存储谱面的文件夹的名称
        /// </summary>
        public string BeatmapDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_song))
                    GetSongDir();
                return _song;
            }
            set => SetValue("BeatmapDirectory", value);
        }

        /// <summary>
        ///     获取osu!当前登录用户的用户名
        /// </summary>
        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                    GetUserName();
                return _username;
            }
        }

        /// <summary>
        ///     获取当前osu!的版本
        /// </summary>
        public string OsuVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_ver))
                    GetVersion();
                return _ver;
            }
        }

        /// <summary>
        ///     获取当前的全局偏移量
        /// </summary>
        public int OverallOffset
        {
            get
            {
                if (_off == -2) GetOffset();
                return _off;
            }
        }

        /// <summary>
        ///     获取当前皮肤的全路径
        /// </summary>
        public string CurrentSkinDir
        {
            get
            {
                if (string.IsNullOrEmpty(_skin)) GetCurrentSkin();
                return Path.Combine(SkinDir, _skin);
            }
            set
            {
                SetValue("Skin", value);
                _skin = value;
            }
        }

        /// <summary>
        ///     获取存放皮肤的文件夹的全路径
        /// </summary>
        public string SkinDir => _osudir + "skins";

        private void Init()
        {
            try
            {
                var processes = Process.GetProcessesByName("osu!");
                foreach (var process in processes)
                {
                    bool isWow64 = true;
                    if(Environment.Is64BitOperatingSystem)
                        IsWow64Process(process.Handle, ref isWow64);
                    if (process.ProcessName == "osu!" && process.MainWindowTitle == "osu!" && isWow64)
                    {
                        CurrentOsuProcess = process;
                        _running = true;
                    }
                }

                if (CurrentOsuProcess is null)
                {
                    ReadFromFile();
                    _lines = File.ReadAllLines(_cfg);
                }
                
                StringBuilder osudir;
                if (OsuDirectory != null)
                {
                    osudir = new StringBuilder(OsuDirectory);
                }
                else
                {
                    ReadFromFile();
                    osudir = new StringBuilder(this._osudir);
                }

                var user = new StringBuilder("osu!.");
                user.Append(Environment.UserName + ".cfg");
                osudir.Replace("osu!.exe", user.ToString());
                _cfg = osudir + user.ToString();
                _lines = File.ReadAllLines(_cfg);
                _osudir = osudir.ToString();
                SaveToFile();
            }
            catch (NullReferenceException e)
            {
                ReadFromFile();
                _lines = File.ReadAllLines(_cfg);
                if (BeatmapDirectory == null)
                    throw new FailToParseException($"未能从文件读取，异常:{e.Message}");
            }
            catch (IndexOutOfRangeException e)
            {
                ReadFromFile();
                _lines = File.ReadAllLines(_cfg);
                if (BeatmapDirectory == null)
                    throw new FailToParseException($"未能从文件读取，异常:{e.Message}");
            }
        }

        private void SetValue(string name, string destVal)
        {
            for (var i = 0; i < _lines.Length; i++)
            {
                var data = _lines[i];
                if (data.Contains(name))
                {
                    var tmp = data.Split('=');
                    data = data.Replace(tmp[1].Trim(), destVal);
                    _lines[i] = data;
                    //MessageBox.Show(data);
                }
            }
        }

        /// <summary>
        ///     强制关闭osu!
        /// </summary>
        public void KillGame()
        {
            CurrentOsuProcess.Kill();
        }

        private void GetSongDir()
        {
            var osudir = CurrentOsuProcess != null ? OsuDirectory : _osudir;
            var tmp = string.Empty;
            foreach (var data in _lines)
                if (data.Trim().StartsWith("BeatmapDirectory"))
                {
                    tmp = data.Split('=')[1].Trim();
                    break;
                }

            if (string.IsNullOrWhiteSpace(tmp))
                _song = string.Empty;
            else
                _song = osudir + tmp;
        }

        private void GetUserName()
        {
            foreach (var data in _lines)
                if (data.Trim().StartsWith("Username"))
                {
                    _username = data.Split('=')[1].Trim();
                    return;
                }

            _username = string.Empty;
        }

        private void GetVersion()
        {
            foreach (var data in _lines)
            {
                var cur = data.Trim();
                if (cur.StartsWith("LastVersion"))
                {
                    if (cur.Split('=')[0].Trim() == "LastVersion")
                    {
                        var tmparr = data.Split('=');
                        _ver = tmparr[1].Trim();
                        return;
                    }

                    if (cur.Split('=')[0].Trim() == "LastVersionPermissionsFailed")
                    {
                    }
                }
            }

            _ver = string.Empty;
        }

        private void GetOffset()
        {
            {
                foreach (var data in _lines)
                    if (data.Trim().StartsWith("Offset"))
                    {
                        _off = int.Parse(data.Split('=')[1].Trim());
                        break;
                    }

                _off = 0;
            }
        }

        private void GetCurrentSkin()
        {
            foreach (var data in _lines)
                if (data.StartsWith("Skin = "))
                {
                    _skin = data.Split('=')[1].Trim();
                    break;
                }
        }

        /// <summary>
        ///     将配置文件的路径存储到文件
        /// </summary>
        public void SaveToFile()
        {
            var fileName = "OsuInfo.txt";
            try
            {
                

                if (CurrentOsuProcess != null)
                {
                    var fileNotExist = !File.Exists(_cfg);
                    var str = File.ReadAllText(fileName);
                    var jToken = ((JObject) JsonConvert.DeserializeObject(str))["OsuConfigFileDir"];
                    var cfgFileIsInvalid = jToken is null;
                    if (fileNotExist || cfgFileIsInvalid)
                    {
                        var stringWriter = new StringWriter();
                        JsonWriter writer = new JsonTextWriter(stringWriter);
                        writer.WriteStartObject();
                        writer.WritePropertyName("OsuConfigFileDir");
                        writer.WriteValue(_cfg);
                        writer.WriteEnd();
                        File.WriteAllText(fileName, stringWriter.ToString());
                        IO.CurrentIO.Write("Information has been saved.");
                    }
                }
            }
            catch
            {
                var stringWriter = new StringWriter();
                JsonWriter writer = new JsonTextWriter(stringWriter);
                writer.WriteStartObject();
                writer.WritePropertyName("OsuConfigFileDir");
                writer.WriteValue(_cfg);
                writer.WriteEnd();
                File.WriteAllText(fileName, stringWriter.ToString());
                IO.CurrentIO.Write("Information has been saved.");
            }
        }

        /// <summary>
        ///     从文件读取信息
        /// </summary>
        public void ReadFromFile()
        {
            try
            {
                var fileName = "OsuInfo.txt";
                var str = File.ReadAllText(fileName);
                _cfg = ((JObject)JsonConvert.DeserializeObject(str))["OsuConfigFileDir"].ToString();
            }
            catch (Exception)
            {
                _cfg = string.Empty;
            }
           
        }

        /// <summary>
        ///     将对象中存储的信息写入文件
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteConfigToFile(string fileName)
        {
            File.WriteAllLines(fileName, _lines);
        }

        /// <summary>
        ///     如果osu!在运行，则在析构时将配置文件的路径保存至文件
        /// </summary>
        ~ObsoletedOsuInfo()
        {
            if (_running) SaveToFile();
        }
    }
}