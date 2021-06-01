using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osuTools.Attributes;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Game.Modes;
using osuTools.GameInfo;
using osuTools.MD5Tools;
using osuTools.MusicPlayer;
using osuTools.OnlineInfo.OsuApiV1.OnlineQueries;
using osuTools.OsuDB;
using osuTools.Skins.Mania;

namespace osuTools.Beatmaps
{
    /// <summary>
    ///     谱面类，其中包含谱面的信息。
    /// </summary>
    [Serializable]
    public partial class Beatmap : IBeatmap,IHasPlayableAudio
    {
        internal bool Notv = false;

        /// <summary>
        ///     初始化一个空的Beatmap对象
        /// </summary>
        public Beatmap()
        {
            FullAudioFileName = "";
            FileName = "";
            FullPath = "";
            DownloadLink = "";
            BackgroundFileName = "";
            Md5 = new MD5String("");
        }

        /// <summary>
        /// 谱面文件的版本
        /// </summary>
        public int BeatmapVersion { get; }

        /// <summary>
        ///     谱面的游戏模式
        /// </summary>
        [AvailableVariable("Beatmap.Mode", "LANG_VAR_BEATMAPMODE")]
        public OsuGameMode Mode { get; set; }

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
                    var cur = tmPts[0];
                    for (int i = 1; i < tmPts.Count; i++)
                    {

                        //cur = tmPts[i];
                        var nxt = tmPts[i];
                        if (nxt.Uninherited)
                        {
                            double offset = nxt.Offset - cur.Offset;
                            if (!bpmTime.ContainsKey(Math.Round(cur.Bpm, 2)))
                                bpmTime.Add(Math.Round(cur.Bpm, 2), offset);
                            else
                                bpmTime[Math.Round(cur.Bpm, 2)] += offset;
                            cur = nxt;
                        }

                        if (i >= tmPts.Count - 1 && bpmTime.Count == 0)
                            bpmTime.Add(Math.Round(cur.Bpm, 2),
                                TimeSpan.FromMilliseconds(cur.Offset).TotalMilliseconds);
                    }

                    var most = from bpm in bpmTime where bpm.Key > 0 orderby bpm.Value descending select bpm;
                    _bpm = most.First().Key;
                }

                return _bpm;
            }
            set => _bpm = value;
        }

        /// <summary>
        ///     谱面有无倒计时
        /// </summary>
        [AvailableVariable("Beatmap.HasCountdown", "LANG_VAR_HASCOUNTDOWN")]
        public bool HasCountdown { get; internal set; } = false;

        /// <summary>
        ///     谱面音频的前导时间
        /// </summary>
        [AvailableVariable("Beatmap.AudioLeadIn", "LANG_VAR_AUDIOLEADIN")]
        public double AudioLeadIn { get; set; } = 0;

        /// <summary>
        ///     谱面的预览时间点
        /// </summary>
        [AvailableVariable("Beatmap.PreviewTime", "LANG_VAR_PREVIEWTIME")]
        public double PreviewTime { get; set; } = -1;

        /// <summary>
        ///     谱面的音效集
        /// </summary>
        public SampleSets SampleSet { get; set; } = SampleSets.Default;

        /// <summary>
        ///     堆叠系数
        /// </summary>
        public double StackLeniency { get; set; } = 0;

        /// <summary>
        ///     暂未知
        /// </summary>
        public bool LetterboxInBreaks { get; set; } = false;

        /// <summary>
        ///     特殊样式
        /// </summary>
        public SpecialStyles SpecialStyle { get; set; } = SpecialStyles.None;

        /// <summary>
        ///     暂未知
        /// </summary>
        public bool StoryFireInFront { get; set; } = false;

        /// <summary>
        ///     可能诱发癫痫的警告
        /// </summary>
        public bool EpilepsyWarning { get; set; } = false;

        /// <summary>
        ///     倒计时的时间偏移
        /// </summary>
        public double CountdownOffset { get; set; } = 0;

        /// <summary>
        ///     暂未知
        /// </summary>
        public bool WidescreenStoryboard { get; set; } = false;

        /// <summary>
        ///     编辑器中时间线上的书签，以毫秒为标记
        /// </summary>
        public List<int> Bookmarks { get; set; } = new List<int>();

        /// <summary>
        ///     间隔空间
        /// </summary>
        public double DistanceSpacing { get; set; } = 0;

        /// <summary>
        ///     节拍
        /// </summary>
        public double BeatDivisor { get; set; } = 4;

        /// <summary>
        ///     网格大小
        /// </summary>
        public double GridSize { get; set; } = 32;

        /// <summary>
        ///     时间线的缩放比
        /// </summary>
        public double TimelineZoom { get; set; } = 1;

        /// <summary>
        ///     滑条的速度倍数
        /// </summary>
        public double SliderMultiplier { get; set; } = 0;

        /// <summary>
        ///     每拍的滑条点的个数
        /// </summary>
        public double SliderTickRate { get; set; } = 0;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        [AvailableVariable("Beatmap.BeatmapSetId", "LANG_VAR_BEATMAPSETID")]
        public int BeatmapSetId { get; internal set; }

        /// <summary>
        ///     谱面是否包含视频
        /// </summary>
        [AvailableVariable("Beatmap.HasVideo", "LANG_VAR_HASVIDEO")]
        public bool HasVideo { get; }

        /// <summary>
        ///     使用osu!api在线查询谱面信息
        /// </summary>
        /// <returns></returns>
        public OnlineBeatmap GetOnlineBeatmap(string apiKey)
        {
            var q = new OnlineBeatmapQuery {OsuApiKey = apiKey, BeatmapId = BeatmapId};
            return q.Beatmaps[0];
        }

        /// <summary>
        ///     将该谱面转换成OsuBeatmap
        /// </summary>
        /// <returns></returns>
        public OsuBeatmap ToOsuBeatmap()
        {
            var info = new OsuInfo();
            var baseDb = new OsuBeatmapDB();
            return baseDb.Beatmaps.FindByMd5(Md5.ToString());
        }

        /// <summary>
        ///     使用MD5判断两个谱面是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Beatmap a, Beatmap b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            try
            {
                return a.Md5 == b.Md5;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        ///     使用MD5判断两个谱面是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Beatmap a, Beatmap b)
        {
            if (a is null && b is null) return false;
            if (a is null || b is null) return true;

            try
            {
                return a.Md5 != b.Md5;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Artist} - {Title} [{Version}]";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is Beatmap b)
                return b.Md5 == Md5;
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode() => Md5.GetHashCode();

        /// <summary>
        /// 创造一个新的播放器并加载音乐文件
        /// </summary>
        /// <returns></returns>
        public IPlayer GetAudioPlayer()
        {
            if (!File.Exists(FullAudioFileName))
                return null;
            BassMusicPlayer musicPlayer = new BassMusicPlayer();
            musicPlayer.OnMediaFailed += (state, info, error) =>
                throw new PlayerException($"在{state}文件{info.Path}时出现错误\n{error}");
            musicPlayer.Load(FullAudioFileName);
            return musicPlayer;


        }
    }
}