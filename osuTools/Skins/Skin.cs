using System.IO;
using System.Threading.Tasks;
using osuTools.Skins.Catch;
using osuTools.Skins.Color;
using osuTools.Skins.Fonts;
using osuTools.Skins.Mania;
using osuTools.Skins.OtherSerrttings;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private readonly string[] _data;

        /// <summary>
        ///     用skin.ini的路径初始化一个Skin对象
        /// </summary>
        /// <param name="skinConfigFile"></param>
        public Skin(string skinConfigFile)
        {
            if (!skinConfigFile.EndsWith("\\"))
                skinConfigFile += "\\";
            if (!skinConfigFile.EndsWith("skin.ini"))
                skinConfigFile += "skin.ini";
            if (File.Exists(skinConfigFile))
            {
                ConfigFileDirectory = skinConfigFile;
                _data = File.ReadAllLines(skinConfigFile);
                _files = Directory.GetFiles(ConfigFileDirectory.Replace("skin.ini", ""), "*.*",
                    SearchOption.TopDirectoryOnly);
                Task.Run(GetInfo);
                Task.Run(GetModsImages);
                Task.Run(GetOsuSkinImage);
                Task.Run(GetCatchSkinImage);
                Task.Run(GetTaikoSkinImage);
                Task.Run(GetManiaSkinImages);
                Task.Run(GetGenericSkinImage);
                Task.Run(GetSkinSound);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        ///     初始化一个空的Skin对象
        /// </summary>
        public Skin()
        {
            ConfigFileDirectory = null;
            Task.Run(GetModsImages);
            Task.Run(GetOsuSkinImage);
            Task.Run(GetCatchSkinImage);
            Task.Run(GetTaikoSkinImage);
            Task.Run(GetManiaSkinImages);
        }

        /// <summary>
        ///     皮肤配置文件的路径
        /// </summary>
        public string ConfigFileDirectory { get; }

        /// <summary>
        ///     皮肤的名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     皮肤的作者
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        ///     皮肤的版本
        /// </summary>
        public string Version { get; private set; } = "latest";

        /// <summary>
        ///     动画的帧率
        /// </summary>
        public uint AnimationFrameRate { get; private set; }

        /// <summary>
        ///     光标的设置
        /// </summary>
        public CursorSetting CursorSettings { get; internal set; } = new CursorSetting();

        /// <summary>
        ///     ComboBurst的设置
        /// </summary>
        public ComboBurstSetting ComboBurstSettings { get; internal set; } = new ComboBurstSetting();

        /// <summary>
        ///     滑条的设置
        /// </summary>
        public SliderSetting SliderSettings { get; internal set; } = new SliderSetting();

        /// <summary>
        ///     圈圈渲染在数字上方
        /// </summary>
        public bool HitCircleOverlayAboveNumber { get; private set; } = true;

        /// <summary>
        ///     将HitSound分层
        /// </summary>
        public bool LayeredHitSounds { get; private set; } = true;

        /// <summary>
        ///     转盘的设置
        /// </summary>
        public SpinnerSetting SpinnerSettings { get; internal set; } = new SpinnerSetting();

        /// <summary>
        ///     颜色的设置
        /// </summary>
        public ColorSetting ColorSettings { get; private set; }

        /// <summary>
        ///     字体的设置
        /// </summary>
        public FontSetting FontSettings { get; private set; }

        /// <summary>
        ///     Catch(CTB)模式的设置
        /// </summary>
        public CatchSkinSetting CatchSettings { get; private set; }

        /// <summary>
        ///     Mania模式的设置
        /// </summary>
        public MultipleKeysSettings ManiaSettings { get; private set; }
    }
}