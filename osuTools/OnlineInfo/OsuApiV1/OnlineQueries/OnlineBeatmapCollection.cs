using System.Collections.Generic;

namespace osuTools.Online.ApiV1
{
    /// <summary>
    ///     在线获取的谱面的集合
    /// </summary>
    public class OnlineBeatmapCollection : IOnlineInfo<OnlineBeatmap>
    {
        /// <summary>
        ///     指示本次查询是否失败
        /// </summary>
        public bool Failed { get; internal set; } = false;

        /// <summary>
        ///     使用整数索引从列表获取OnlineBeatmap
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public OnlineBeatmap this[int x]
        {
            get => Beatmaps[x];
            set => Beatmaps[x] = value;
        }

        /// <summary>
        ///     存储的<see cref="OnlineBeatmap" />
        /// </summary>
        public List<OnlineBeatmap> Beatmaps { get; } = new List<OnlineBeatmap>();

        /// <summary>
        ///     存储的谱面的数量
        /// </summary>
        public int Count => Beatmaps.Count;

        /// <summary>
        ///     通过关键词搜索谱面
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public OnlineBeatmapCollection Find(string keyword)
        {
            var bc = new OnlineBeatmapCollection();
            foreach (var beat in Beatmaps)
                if (beat.ToBeatmap().ToString().Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                    beat.ToBeatmap().Source.Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                    beat.ToBeatmap().Tags.Trim().ToUpper().Contains(keyword.ToUpper().Trim()))
                    bc.Beatmaps.Add(beat);
            return bc;
        }

        /// <summary>
        ///     判断列表中是否包含指定谱面
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Contains(OnlineBeatmap b)
        {
            return Beatmaps.Contains(b);
        }

        /// <summary>
        ///     返回循环访问List的枚举数
        /// </summary>
        /// <returns></returns>
        public IEnumerator<OnlineBeatmap> GetEnumerator()
        {
            return Beatmaps.GetEnumerator();
        }
    }
}