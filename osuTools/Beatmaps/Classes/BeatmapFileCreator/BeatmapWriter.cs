﻿using System.IO;
using System.Text;
using osuTools.Attributes;

namespace osuTools.Beatmaps.BeatmapFileCreator
{
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

        public Beatmap BaseBeatmap { get; set; }

        public virtual void Write(Stream stream)
        {
            var infoBytes = Encoding.UTF8.GetBytes(GetFormat());
            stream.Write(infoBytes, 0, infoBytes.Length);
        }
    }
}