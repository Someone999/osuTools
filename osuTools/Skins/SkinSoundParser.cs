using osuTools.Attributes;

namespace osuTools.Skins
{
    public partial class Skin
    {
        /// <summary>
        ///     皮肤的音频文件的集合
        /// </summary>
        [WorkingInProgress(DevelopmentStage.AtStart, "2020/09/30")]
        public SkinSoundCollection SkinSounds { get; internal set; }

        private void GetSkinSound()
        {
        }
    }
}