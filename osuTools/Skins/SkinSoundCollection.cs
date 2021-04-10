using System.Collections.Generic;
using osuTools.Attributes;
using osuTools.Exceptions;
using osuTools.Skins.SkinObjects.Generic;

namespace osuTools.Skins.Sounds
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
        public List<GenericSkinSound> ComboBurstSounds { get; internal set; } = new List<GenericSkinSound>();
    }
}