using System.Collections.Generic;
using osuTools.Attributes;
using osuTools.Skins.Game;

namespace osuTools.Skins
{
    /// <summary>
    ///     皮肤音频的集合，处于开发阶段
    /// </summary>
    [WorkingInProgress(DevelopmentStage.AtStart, "2020/09/30")]
    public class SkinSoundCollection
    {
        /// <summary>
        ///     到达指定连击数时播放的声音
        /// </summary>
        public List<GeneralSkinSound> ComboBurstSounds { get; internal set; } = new List<GeneralSkinSound>();
    }
}