using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using osuTools.Beatmaps;
using osuTools.Game.Modes;
using osuTools.OnlineInfo.OsuApiV1.OnlineQueries;

namespace osuTools.OnlineInfo.OsuApiV2.ResultClasses
{
    /// <summary>
    ///     通过OsuApiV2查询到的谱面
    /// </summary>
    public class OnlineBeatmapV2:IBeatmap
    {
        /// <summary>
        ///     使用Json填充一个OnlineBeatmapV2对象
        /// </summary>
        /// <param name="json"></param>
        /// <param name="beatmapSet"></param>
        public OnlineBeatmapV2(JObject json,OnlineBeatmapSetV2 beatmapSet = null)
        {
            var suc = json.TryGetValue("beatmaps", out var jtoken);
            if (suc)
                throw new ArgumentException("不应用BeatmapSet的返回json实例化Beatmap。");
            Stars = json["difficulty_rating"].ToObject<double>();
            BeatmapId = json["id"].ToObject<int>();
            Mode = json["mode_int"].ToObject<OsuGameMode>();
            Version = json["version"].ToString();
            OverallDifficulty = json["accuracy"].ToObject<double>();
            ApproachRate = json["ar"].ToObject<double>();
            CircleSize = json["cs"].ToObject<double>();
            HpDrain = json["drain"].ToObject<double>();
            BeatmapSetId = json["beatmapset_id"].ToObject<int>();
            Bpm = json["bpm"].ToObject<double>();
            Convert = json["convert"].ToObject<bool>();
            CircleCount = json["count_circles"].ToObject<short>();
            SliderCount = json["count_sliders"].ToObject<short>();
            SpinnerCount = json["count_spinners"].ToObject<short>();
            DeleteAt = json["deleted_at"].ToString().ToNullableDateTime();
            HitLength = TimeSpan.FromSeconds(json["hit_length"].ToObject<int>());
            IsScoreable = json["is_scoreable"].ToObject<bool>();
            LastUpdate = json["last_updated"].ToString().ToNullableDateTime();
            PassCount = json["passcount"].ToObject<int>();
            PlayCount = json["playcount"].ToObject<int>();
            Ranked = json["ranked"].ToObject<bool>();
            var arr = json["status"].ToString().ToCharArray();
            arr[0] -= (char) Math.Abs('A' - 'a');
            Status = (BeatmapStatus) Enum.Parse(typeof(BeatmapStatus), new string(arr));
            TotalLength = TimeSpan.FromSeconds(json["total_length"].ToObject<int>());
            BeatmapDownloadPageUrl = json["url"].ToString();
            var failstat = json["failtimes"];
            FailTimesAtSongPercent = failstat["fail"].ToObject<List<int>>();
            ExitTimesAtSongPercent = failstat["exit"].ToObject<List<int>>();
            BeatmapSet = beatmapSet;
            Title = beatmapSet?.Title;
            TitleUnicode = beatmapSet?.TitleUnicode;
            Artist = beatmapSet?.Artist;
            ArtistUnicode = beatmapSet?.ArtistUnicode;
            Creator = beatmapSet?.Creator;

        }

        /// <summary>
        ///     创建一个空的OnlineBeatmapV2对象
        /// </summary>
        public OnlineBeatmapV2()
        {
        }
        /// <summary>
        /// 包含这个谱面的谱面集，可能为null
        /// </summary>
        public OnlineBeatmapSetV2 BeatmapSet { get; }
        /// <summary>
        ///     难度星级
        /// </summary>
        public double Stars { get; } = -1;

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId { get; } = -2;
        /// <summary>
        /// 谱面的标题
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// 谱面标题的Unicode格式
        /// </summary>
        public string TitleUnicode { get; }
        /// <summary>
        /// 曲目的艺术家
        /// </summary>
        public string Artist { get; }
        /// <summary>
        /// 曲目艺术家名字的Unicode格式
        /// </summary>
        public string ArtistUnicode { get; }
        /// <summary>
        /// 谱面的作者
        /// </summary>
        public string Creator { get; }

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int BeatmapSetId { get; } = -2;

        /// <summary>
        ///     谱面的模式
        /// </summary>
        public OsuGameMode Mode { get; } = OsuGameMode.Unkonwn;

        /// <summary>
        ///     谱面的难度标签
        /// </summary>
        public string Version { get; } = "";

        /// <summary>
        ///     总体难度 Overall Difficulty
        /// </summary>
        public double OverallDifficulty { get; } = -1;

        /// <summary>
        ///     缩圈速度 Approach Rate
        /// </summary>
        public double ApproachRate { get; } = -1;

        /// <summary>
        ///     掉血和回血速度 HpDrain
        /// </summary>
        public double HpDrain { get; } = -1;

        /// <summary>
        ///     圈圈大小 Circle Size
        /// </summary>
        public double CircleSize { get; } = -1;

        /// <summary>
        ///     每分钟节奏数
        /// </summary>
        public double Bpm { get; } = -1;

        /// <summary>
        ///     是否为转谱
        /// </summary>
        public bool Convert { get; }

        /// <summary>
        ///     圈圈的数量
        /// </summary>
        public short CircleCount { get; } = -1;

        /// <summary>
        ///     滑条的数量
        /// </summary>
        public short SliderCount { get; } = -1;

        /// <summary>
        ///     转盘的数量
        /// </summary>
        public short SpinnerCount { get; } = -1;

        /// <summary>
        ///     被删除的时间
        /// </summary>
        public DateTime? DeleteAt { get; }

        /// <summary>
        ///     最后一个<see cref="osuTools.Beatmaps.HitObject.IHitObject" />的时间
        /// </summary>
        public TimeSpan HitLength { get; }

        /// <summary>
        ///     是否计入分数
        /// </summary>
        public bool IsScoreable { get; }

        /// <summary>
        ///     上次更新的时间
        /// </summary>
        public DateTime? LastUpdate { get; }

        /// <summary>
        ///     通过次数
        /// </summary>
        public int PassCount { get; } = -1;

        /// <summary>
        ///     游玩次数
        /// </summary>
        public int PlayCount { get; } = -1;

        /// <summary>
        ///     谱面是否Ranked
        /// </summary>
        public bool Ranked { get; }

        /// <summary>
        ///     谱面状态
        /// </summary>
        public BeatmapStatus Status { get; } = BeatmapStatus.None;

        /// <summary>
        ///     谱面总长度
        /// </summary>
        public TimeSpan TotalLength { get; }

        /// <summary>
        ///     下载谱面的Url
        /// </summary>
        public string BeatmapDownloadPageUrl { get; } = "";

        /// <summary>
        ///     在曲目各个百分比失败的次数
        /// </summary>
        public List<int> FailTimesAtSongPercent { get; } = new List<int>();

        /// <summary>
        ///     在曲目各个百分比退出的次数
        /// </summary>
        public List<int> ExitTimesAtSongPercent { get; } = new List<int>();
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return BeatmapId << 3;
        }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is OnlineBeatmapV2 beatmap)
                return beatmap.BeatmapId == BeatmapId;
            return false;
        }
        ///<inheritdoc/>
        public override string ToString()
        {
            var beatmapSet = BeatmapSet;
            return beatmapSet is null?"":$"{beatmapSet.Artist} - {beatmapSet.Title} [{Version}]";
        }
    }
}