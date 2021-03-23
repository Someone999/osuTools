using System.Collections.Generic;
using osuTools.Beatmaps;
using osuTools.osuToolsException;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     存储<see cref="OsuBeatmap" />的集合
    /// </summary>
    public class OsuBeatmapCollection
    {
        /// <summary>
        ///     谱面的ID的种类
        /// </summary>
        public enum BeatmapIDType
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            BeatmapId,

            /// <summary>
            ///     谱面集ID
            /// </summary>
            BeatmapSetId
        }

        private List<OsuBeatmap> beatmaps { get; } = new List<OsuBeatmap>();

        /// <summary>
        ///     存储的<seealso cref="OsuBeatmap" />
        /// </summary>
        public IReadOnlyList<OsuBeatmap> Beatmaps => beatmaps.AsReadOnly();

        /// <summary>
        ///     谱面的数量
        /// </summary>
        public int Count => beatmaps.Count;

        /// <summary>
        ///     使用整数索引从列表中获取OsuBeatmap
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public OsuBeatmap this[int x] => beatmaps[x];

        /// <summary>
        ///     检测指定谱面是否在列表中
        /// </summary>
        /// <param name="b">要检测的谱面</param>
        /// <returns>布尔值，指示谱面是否在列表中</returns>
        public bool Contains(OsuBeatmap b)
        {
            return beatmaps.Contains(b);
        }

        internal void Add(OsuBeatmap b)
        {
            beatmaps.Add(b);
        }

        /// <summary>
        ///     使用关键词搜索，可指定包含或不包含
        /// </summary>
        /// <param name="KeyWord">关键词</param>
        /// <param name="option">是否包含关键词</param>
        /// <returns>包含搜索结果的谱面集合</returns>
        public OsuBeatmapCollection Find(string KeyWord,
            BeatmapCollection.BeatmapFindOption option = BeatmapCollection.BeatmapFindOption.Contains)
        {
            var b = new OsuBeatmapCollection();
            var keyword = KeyWord.ToUpper();
            foreach (var beatmap in Beatmaps)
            {
                var allinfo = beatmap.ToString().ToUpper() + " " + beatmap.Source.ToUpper() + " " +
                              beatmap.Tags.ToUpper() + " " + beatmap.Creator.ToUpper();
                if (option == BeatmapCollection.BeatmapFindOption.Contains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        var newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Title.ToUpper() == newkeyw || beatmap.TitleUnicode.ToUpper() == newkeyw ||
                            beatmap.Artist.ToUpper() == newkeyw || beatmap.ArtistUnicode.ToUpper() == newkeyw ||
                            beatmap.Creator.ToUpper() == newkeyw || beatmap.Tags.ToUpper() == newkeyw ||
                            beatmap.Source.ToUpper() == newkeyw ||
                            beatmap.Difficulty.ToUpper() == newkeyw)
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                    }
                    else if (allinfo.Contains(keyword))
                    {
                        b.Add(beatmap);
                    }
                }

                if (option == BeatmapCollection.BeatmapFindOption.NotContains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        var newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Title.ToUpper() != newkeyw && beatmap.TitleUnicode.ToUpper() != newkeyw &&
                            beatmap.Artist.ToUpper() != newkeyw && beatmap.ArtistUnicode.ToUpper() != newkeyw &&
                            beatmap.Creator.ToUpper() != newkeyw && beatmap.Tags.ToUpper() != newkeyw &&
                            beatmap.Source.ToUpper() != newkeyw &&
                            beatmap.Difficulty.ToUpper() != newkeyw)
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                    }
                    else if (!allinfo.Contains(keyword))
                    {
                        b.Add(beatmap);
                    }
                }
            }

            if (b.Count == 0) throw new BeatmapNotFoundException("找不到指定的谱面");
            return b;
        }

        /// <summary>
        ///     根据谱面的ID查找谱面
        /// </summary>
        /// <param name="ID">BeatmapID或BeatmapSetID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<OsuBeatmap> Find(int ID, BeatmapIDType type = BeatmapIDType.BeatmapId)
        {
            var lst = new List<OsuBeatmap>();
            if (ID != -1)
                if (type == BeatmapIDType.BeatmapId)
                    foreach (var beatmap in Beatmaps)
                        if (beatmap.BeatmapID == ID)
                            lst.Add(beatmap);
            if (type == BeatmapIDType.BeatmapSetId)
                foreach (var beatmap in Beatmaps)
                    if (beatmap.BeatmapSetID == ID)
                        lst.Add(beatmap);
            return lst;
        }

        /// <summary>
        ///     使用MD5在谱面列表里搜索
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public OsuBeatmap FindByMD5(string md5)
        {
            foreach (var beatmap in Beatmaps)
                if (beatmap.MD5 == md5)
                    return beatmap;
            throw new BeatmapNotFoundException($"找不到MD5为{md5}的谱面。");
        }

        /// <summary>
        ///     使用游戏模式来搜索谱面，可指定包括或不包括
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public OsuBeatmapCollection Find(OsuGameMode Mode,
            BeatmapCollection.BeatmapFindOption option = BeatmapCollection.BeatmapFindOption.Contains)
        {
            var bc = new OsuBeatmapCollection();
            foreach (var b in beatmaps)
            {
                if (option == BeatmapCollection.BeatmapFindOption.Contains)
                    if (b.Mode == Mode)
                        if (!bc.Contains(b))
                            bc.Add(b);
                if (option == BeatmapCollection.BeatmapFindOption.NotContains)
                    if (b.Mode != Mode)
                        if (!bc.Contains(b))
                            bc.Add(b);
            }

            return bc;
        }

        /// <summary>
        ///     获取列表的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<OsuBeatmap> GetEnumerator()
        {
            return beatmaps.GetEnumerator();
        }
    }
}

namespace osuTools.OsuDB
{
}