using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using osuTools.ExtraMethods;
using osuTools.Online.ApiV1;

namespace osuTools.Online.ApiV2.Classes
{
    /// <summary>
    ///     通过OsuApiV2查询到的谱面
    /// </summary>
    public class OnlineBeatmapV2
    {
        /// <summary>
        ///     使用Json填充一个OnlineBeatmapV2对象
        /// </summary>
        /// <param name="json"></param>
        public OnlineBeatmapV2(JObject json)
        {
            JToken jtoken;
            var suc = json.TryGetValue("beatmaps", out jtoken);
            if (suc)
                throw new ArgumentException("不应用BeatmapSet的返回json实例化Beatmap。");
            Stars = json["difficulty_rating"].ToObject<double>();
            BeatmapID = json["id"].ToObject<int>();
            Mode = json["mode_int"].ToObject<OsuGameMode>();
            Version = json["version"].ToString();
            OD = json["accuracy"].ToObject<double>();
            AR = json["ar"].ToObject<double>();
            HP = json["drain"].ToObject<double>();
            BeatmapSetID = json["beatmapset_id"].ToObject<int>();
            BPM = json["bpm"].ToObject<double>();
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
        }

        /// <summary>
        ///     创建一个空的OnlineBeatmapV2对象
        /// </summary>
        public OnlineBeatmapV2()
        {
        }

        /// <summary>
        ///     难度星级
        /// </summary>
        public double Stars { get; internal set; } = -1;

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapID { get; internal set; } = -2;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int BeatmapSetID { get; internal set; } = -2;

        /// <summary>
        ///     谱面的模式
        /// </summary>
        public OsuGameMode Mode { get; internal set; } = OsuGameMode.Unkonwn;

        /// <summary>
        ///     谱面的难度标签
        /// </summary>
        public string Version { get; internal set; } = "";

        /// <summary>
        ///     总体难度 Overall Difficulty
        /// </summary>
        public double OD { get; internal set; } = -1;

        /// <summary>
        ///     缩圈速度 Approach Rate
        /// </summary>
        public double AR { get; internal set; } = -1;

        /// <summary>
        ///     掉血和回血速度 HP Drain
        /// </summary>
        public double HP { get; internal set; } = -1;

        /// <summary>
        ///     圈圈大小 Circle Size
        /// </summary>
        public double CS { get; internal set; } = -1;

        /// <summary>
        ///     每分钟节奏数
        /// </summary>
        public double BPM { get; internal set; } = -1;

        /// <summary>
        ///     是否为转谱
        /// </summary>
        public bool Convert { get; internal set; }

        /// <summary>
        ///     圈圈的数量
        /// </summary>
        public short CircleCount { get; internal set; } = -1;

        /// <summary>
        ///     滑条的数量
        /// </summary>
        public short SliderCount { get; internal set; } = -1;

        /// <summary>
        ///     转盘的数量
        /// </summary>
        public short SpinnerCount { get; internal set; } = -1;

        /// <summary>
        ///     被删除的时间
        /// </summary>
        public DateTime? DeleteAt { get; internal set; }

        /// <summary>
        ///     最后一个<see cref="osuTools.Beatmaps.HitObject.IHitObject" />的时间
        /// </summary>
        public TimeSpan HitLength { get; internal set; }

        /// <summary>
        ///     是否计入分数
        /// </summary>
        public bool IsScoreable { get; internal set; }

        /// <summary>
        ///     上次更新的时间
        /// </summary>
        public DateTime? LastUpdate { get; internal set; }

        /// <summary>
        ///     通过次数
        /// </summary>
        public int PassCount { get; internal set; } = -1;

        /// <summary>
        ///     游玩次数
        /// </summary>
        public int PlayCount { get; internal set; } = -1;

        /// <summary>
        ///     谱面是否Ranked
        /// </summary>
        public bool Ranked { get; internal set; }

        /// <summary>
        ///     谱面状态
        /// </summary>
        public BeatmapStatus Status { get; internal set; } = BeatmapStatus.None;

        /// <summary>
        ///     谱面总长度
        /// </summary>
        public TimeSpan TotalLength { get; internal set; }

        /// <summary>
        ///     下载谱面的Url
        /// </summary>
        public string BeatmapDownloadPageUrl { get; internal set; } = "";

        /// <summary>
        ///     在曲目各个百分比失败的次数
        /// </summary>
        public List<int> FailTimesAtSongPercent { get; internal set; } = new List<int>();

        /// <summary>
        ///     在曲目各个百分比退出的次数
        /// </summary>
        public List<int> ExitTimesAtSongPercent { get; internal set; } = new List<int>();
    }
}