using osuTools.Attributes;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Generic.PlayField.SectionRank
{
    /// <summary>
    ///     BreakTime时的播放的区间通过与否的音频与图象的单个元素
    /// </summary>
    [WorkingInProgress(DevelopmentStage.AtStart, "2020/09/30")]
    public class SectionRank : ISoundedSkinImage
    {
        /// <summary>
        ///     图象
        /// </summary>
        public ISkinImage Image { get; internal set; }

        /// <summary>
        ///     音频
        /// </summary>
        public ISkinSound Sound { get; internal set; }
    }
}