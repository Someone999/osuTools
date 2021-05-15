using System.IO;
using osuTools.Attributes;
using osuTools.Exceptions;

namespace osuTools.Beatmaps.BeatmapFileCreator
{
    /// <summary>
    /// 将谱面写入文件
    /// </summary>
    [WorkingInProgress(DevelopmentStage.Developing, "2020/10/25")]
    public interface IBeatmapFileCreator
    {
        /// <summary>
        ///     创建时要使用的Beatmap
        /// </summary>
        Beatmap BaseBeatmap { get; set; }

        /// <summary>
        ///     将BaseBeatmap写入文件
        /// </summary>
        /// <param name="stream"></param>
        void Write(Stream stream);
    }
}