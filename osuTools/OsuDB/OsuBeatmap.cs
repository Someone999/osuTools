using System;
using System.Collections.Generic;
using System.Linq;
using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     谱面，存储的信息多于Beatmap类
    /// </summary>
    public class OsuBeatmap : IOsuDbData,IBeatmap
    {
        internal List<OsuBeatmapTimePoint> Timepoints = new List<OsuBeatmapTimePoint>();

        /// <summary>
        ///     谱面的难度星级
        /// </summary>
        public double Stars { get; internal set; }

        /// <summary>
        ///     存放谱面的文件夹的名称
        /// </summary>
        public string FolderName { get; internal set; }

        /// <summary>
        ///     谱面的游戏模式
        /// </summary>
        public OsuGameMode Mode { get; internal set; } = OsuGameMode.Unkonwn;

        /// <summary>
        ///     谱面的音乐文件名称
        /// </summary>
        public string AudioFileName { get; internal set; }

        /// <summary>
        ///     艺术家
        /// </summary>
        public string Artist { get; internal set; }

        /// <summary>
        ///     艺术家
        /// </summary>
        public string ArtistUnicode { get; internal set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string TitleUnicode { get; internal set; }

        /// <summary>
        ///     来源
        /// </summary>
        public string Source { get; internal set; }

        /// <summary>
        ///     标签
        /// </summary>
        public string Tags { get; internal set; }

        /// <summary>
        ///     谱面的创造者
        /// </summary>
        public string Creator { get; internal set; }

        /// <summary>
        ///     谱面的难度标签
        /// </summary>
        public string Difficulty { get; internal set; }

        /// <summary>
        /// 谱面的难度标签
        /// </summary>
        public string Version => Difficulty;

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string Md5 { get; internal set; }

        /// <summary>
        ///     谱面文件的文件名
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        ///     谱面的状态
        /// </summary>
        public OsuBeatmapStatus BeatmapStatus { get; internal set; } = OsuBeatmapStatus.Unknown;

        /// <summary>
        ///     谱面中圈圈的数量
        /// </summary>
        public short HitCircle { get; internal set; }

        /// <summary>
        ///     谱面中滑条的数量
        /// </summary>
        public short Slider { get; internal set; }

        /// <summary>
        ///     谱面中转盘的数量
        /// </summary>
        public short Spinner { get; internal set; }

        /// <summary>
        ///     谱面上次修改的时间
        /// </summary>
        public DateTime LastModificationTime { get; internal set; }

        /// <summary>
        ///     谱面的长度
        /// </summary>
        public TimeSpan DrainTime { get; internal set; }

        /// <summary>
        ///     音频的长度
        /// </summary>
        public TimeSpan TotalTime { get; internal set; }

        /// <summary>
        ///     音频的预览点
        /// </summary>
        public TimeSpan PreviewPoint { get; internal set; }

        /// <summary>
        ///     缩圈速度
        /// </summary>
        public double ApproachRate { get; internal set; }

        /// <summary>
        ///     综合难度
        /// </summary>
        public double OverallDifficulty { get; internal set; }

        /// <summary>
        ///     圈圈大小
        /// </summary>
        public double CircleSize { get; internal set; }

        /// <summary>
        ///     掉血、回血速度
        /// </summary>
        public double HpDrain { get; internal set; }

        /// <summary>
        ///     谱面的时间点
        /// </summary>
        public IReadOnlyList<OsuBeatmapTimePoint> TimePoints => Timepoints.AsReadOnly();

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId { get; internal set; }

        /// <summary>
        ///     谱面集的ID
        /// </summary>
        public int BeatmapSetId { get; internal set; }

        /// <summary>
        ///     包含部分Mod与难度星级的字典
        /// </summary>
        public DifficultyRate ModStarPair { get; internal set; } = new DifficultyRate();

        /// <summary>
        /// </summary>
        public int ThreadId { get; internal set; }

        /// <summary>
        ///     将OsuBeatmap转换成字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Artist} - {Title} [{Difficulty}]";
        }

        /// <summary>
        ///     将OsuBeatmap转化成<seealso cref="Beatmap" />
        /// </summary>
        /// <returns></returns>
        public Beatmap ToBeatmap()
        {
            return new Beatmap(this);
        }

        /// <summary>
        ///     使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuBeatmap a, OsuBeatmap b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Md5 == b.Md5;
        }

        /// <summary>
        ///     使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator !=(OsuBeatmap a, OsuBeatmap b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null) 
                return true;
            return a.Md5 != b.Md5;
        }

        private double _bpm;
        /// <summary>
        /// 谱面中出现次数最多的Bpm
        /// </summary>
        public double Bpm
        {
            get
            {
                if (_bpm == 0)
                {
                    Dictionary<double, double> bpmTime = new Dictionary<double, double>();
                    var tmPts = TimePoints;
                    OsuBeatmapTimePoint cur = tmPts[0];
                    for (int i = 1; i < tmPts.Count; i++)
                    {
                        
                        //cur = tmPts[i];
                        var nxt = tmPts[i];
                        if (nxt.Inherit)
                        {
                            double offset = nxt.Offset - cur.Offset;
                            if (!bpmTime.ContainsKey(Math.Round(cur.Bpm, 2)))
                                bpmTime.Add(Math.Round(cur.Bpm, 2), offset);
                            else
                                bpmTime[Math.Round(cur.Bpm, 2)] += offset;
                            cur = nxt;
                        }
                        if (i >= tmPts.Count - 1 && bpmTime.Count == 0)
                            bpmTime.Add(Math.Round(cur.Bpm, 2), (DrainTime - TimeSpan.FromMilliseconds(cur.Offset)).TotalMilliseconds);
                    }

                    var most = from bpm in bpmTime where bpm.Key > 0 orderby bpm.Value descending select bpm;
                    _bpm = most.First().Key;
                }

                return _bpm;
            }
            set => _bpm = value;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is OsuBeatmap b)
                return b.Md5 == Md5;
            return false;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode()
        {
            return Md5.GetHashCode();
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与谱面Md5匹配的的成绩
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<OsuScoreInfo> GetScores()
        {
            OsuScoreDb scoreDb = new OsuScoreDb();
            List<OsuScoreInfo> info = new List<OsuScoreInfo>();
            foreach (var score in scoreDb.Scores)
            {
                if(score.BeatmapMd5 == Md5)
                    info.Add(score);
            }
            return info.AsReadOnly();
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与符合条件的成绩
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>

        public IReadOnlyList<OsuScoreInfo> GetScores(Comparison<OsuScoreInfo> comparison)
        {
            var lst = new List<OsuScoreInfo>(GetScores());
            lst.Sort(comparison);
            return lst.AsReadOnly();
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与符合条件的成绩
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public IReadOnlyList<OsuScoreInfo> GetScores(IComparer<OsuScoreInfo> comparer)
        {
            var lst = new List<OsuScoreInfo>(GetScores());
            lst.Sort(comparer);
            return lst.AsReadOnly();
        }
    }
}