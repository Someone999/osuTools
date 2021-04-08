using System.IO;
using System.Threading.Tasks;
using osuTools.Skins.Colors.Settings;
using osuTools.Skins.Settings.Catch;
using osuTools.Skins.Settings.ComboBurst;
using osuTools.Skins.Settings.Cursor;
using osuTools.Skins.Settings.Fonts;
using osuTools.Skins.Settings.Mania;
using osuTools.Skins.Settings.Slider;
using osuTools.Skins.Settings.Spinner;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private readonly string[] data;


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
                data = File.ReadAllLines(skinConfigFile);
                files = Directory.GetFiles(ConfigFileDirectory.Replace("skin.ini", ""), "*.*",
                    SearchOption.TopDirectoryOnly);
                Task.Run(getInfo);
                Task.Run(getModsImages);
                Task.Run(getOsuSkinImage);
                Task.Run(getCatchSkinImage);
                Task.Run(getTaikoSkinImage);
                Task.Run(getManiaSkinImages);
                Task.Run(getGenericSkinImage);
                Task.Run(getSkinSound);
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
            Task.Run(getModsImages);
            Task.Run(getOsuSkinImage);
            Task.Run(getCatchSkinImage);
            Task.Run(getTaikoSkinImage);
            Task.Run(getManiaSkinImages);
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