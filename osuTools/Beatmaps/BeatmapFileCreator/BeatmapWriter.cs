using System.IO;
using System.Text;
using osuTools.Attributes;

namespace osuTools.Beatmaps.BeatmapFileCreator
{
    /// <summary>
    ///     将Beatmap对象按照正确的格式写入文件
    /// </summary>
    [WorkingInProgress(DevelopmentStage.Developing, "2020/10/25")]
    public partial class BeatmapFileCreator : IBeatmapFileCreator
    {
        /// <summary>
        ///     使用指定的Beatmap初始化BeatmapFileCreator
        /// </summary>
        /// <param name="baseBeatmap"></param>
        public BeatmapFileCreator(Beatmap baseBeatmap)
        {
            BaseBeatmap = baseBeatmap;
        }
        /// <summary>
        /// 基础谱面
        /// </summary>
        public Beatmap BaseBeatmap { get; set; }
        /// <summary>
        /// 将谱面信息写入流
        /// </summary>
        /// <param name="stream"></param>
        public virtual void Write(Stream stream)
        {
            var infoBytes = Encoding.UTF8.GetBytes(GetFormat());
            stream.Write(infoBytes, 0, infoBytes.Length);
        }
    }
}