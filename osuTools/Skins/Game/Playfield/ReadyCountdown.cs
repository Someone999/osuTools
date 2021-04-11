using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Generic.PlayField.Countdown
{
    /// <summary>
    ///     倒计时时会播放的音频与图片
    /// </summary>
    public class ReadyCountdown : ISoundedSkinImage
    {
        /// <summary>
        ///     图片
        /// </summary>
        public ISkinImage Image { get; internal set; }

        /// <summary>
        ///     音频
        /// </summary>
        public ISkinSound Sound { get; internal set; }
    }
}