using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using osuTools.ExtraMethods;
using osuTools.Online.ApiV1;

namespace osuTools.Online.ApiV2.Classes
{
    /// <summary>
    ///     通过OsuApiV2查询到的谱面集
    /// </summary>
    public class OnlineBeatmapSetV2
    {
        /// <summary>
        ///     使用Json填充一个OnlineBeatmapSetV2对象
        /// </summary>
        /// <param name="json"></param>
        public OnlineBeatmapSetV2(JObject json)
        {
            var setinfo = new JObject();
            var beatmaps = new JArray();
            if (json.GetValue("beatmaps") != null)
            {
                setinfo = json;
                beatmaps = (JArray) json["beatmaps"];
                foreach (var js in beatmaps)
                    Beatmaps.Add(new OnlineBeatmapV2((JObject) js));
            }
            else
            {
                setinfo = (JObject) json["beatmapset"];
                Beatmaps.Add(new OnlineBeatmapV2(json));
            }

            Artist = setinfo["artist"].ToString();
            ArtistUnicode = setinfo["artist_unicode"].ToString();
            Creator = setinfo["creator"].ToString();
            FavoriteCount = setinfo["favourite_count"].ToObject<int>();
            SetID = setinfo["id"].ToObject<int>();
            PlayCount = setinfo["play_count"].ToObject<int>();
            PreviewUrl = setinfo["preview_url"].ToString();
            Source = setinfo["source"].ToString();
            var arr = setinfo["status"].ToString().ToCharArray();
            arr[0] -= (char) Math.Abs('A' - 'a');
            Status = (BeatmapStatus) Enum.Parse(typeof(BeatmapStatus), new string(arr));
            Title = setinfo["title"].ToString();
            TitleUnicode = setinfo["title_unicode"].ToString();
            CreatorUserID = setinfo["user_id"].ToObject<int>();
            HasVideo = setinfo["video"].ToObject<bool>();
            BPM = setinfo["bpm"].ToObject<double>();
            IsScoreable = setinfo["is_scoreable"].ToObject<bool>();
            LastUpdate = setinfo["last_updated"].ToString().ToNullableDateTime();
            LegacyThreadUrl = setinfo["legacy_thread_url"].ToString();
            Ranked = setinfo["ranked"].ToObject<bool>();
            RankedDate = setinfo["ranked_date"].ToString().ToNullableDateTime();
            HasStoryBoard = setinfo["storyboard"].ToObject<bool>();
            SubmittedDate = setinfo["submitted_date"].ToString().ToNullableDateTime();
            Tags = setinfo["tags"].ToString();
            Rating = setinfo["ratings"].ToObject<List<double>>();

            #region 谱面的被推荐次数(???)

            var nomin = setinfo["nominations"];
            Nominations.CurrentNominations = nomin["current"].ToObject<int>();
            Nominations.RequiredNominations = nomin["required"].ToObject<int>();

            #endregion

            #region 谱面能否被宣传(???)

            var hyped = setinfo["hype"];
            HypeStatus.CanBeHyped = setinfo["can_be_hyped"].ToObject<bool>();
            HypeStatus.CurrentHyped = hyped["current"].ToObject<int>();
            HypeStatus.RequiredHype = hyped["required"].ToObject<int>();

            #endregion

            #region 谱面是否可下载

            var ava = setinfo["availability"];
            BeatmapAvailability.DownloadDisabled = ava["download_disabled"].ToObject<bool>();
            BeatmapAvailability.MoreInformation = ava["more_information"].ToString();

            #endregion

            #region 封面等图片

            var covers = (JObject) setinfo["covers"];
            Covers.Card = covers["card"].ToString();
            Covers.Card2x = covers["card@2x"].ToString();
            Covers.Cover = covers["cover"].ToString();
            Covers.Cover2x = covers["cover@2x"].ToString();
            Covers.List = covers["list"].ToString();
            Covers.List2x = covers["list@2x"].ToString();
            Covers.SlimCover = covers["slimcover"].ToString();
            Covers.SlimCover2x = covers["slimcover@2x"].ToString();

            #endregion
        }

        /// <summary>
        ///     创建一个空的OnlineBeatmapSetV2对象
        /// </summary>
        public OnlineBeatmapSetV2()
        {
        }

        /// <summary>
        ///     谱面集中包含的所有谱面
        /// </summary>
        public List<OnlineBeatmapV2> Beatmaps { get; internal set; } = new List<OnlineBeatmapV2>();

        /// <summary>
        ///     谱面集对应曲目的艺术家的英文名
        /// </summary>
        public string Artist { get; internal set; } = "";

        /// <summary>
        ///     谱面集对应曲目的艺术家
        /// </summary>
        public string ArtistUnicode { get; internal set; } = "";

        /// <summary>
        ///     谱面集的各种封面图片的Url
        /// </summary>
        public ImageUrl Covers { get; internal set; } = new ImageUrl();

        /// <summary>
        ///     谱面集的创作者
        /// </summary>
        public string Creator { get; internal set; } = "";

        /// <summary>
        ///     谱面集的创作者的UserID
        /// </summary>
        public int CreatorUserID { get; internal set; } = -1;

        /// <summary>
        ///     谱面集被收藏的次数
        /// </summary>
        public int FavoriteCount { get; internal set; } = -1;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int SetID { get; internal set; } = -1;

        /// <summary>
        ///     谱面集被游玩的次数
        /// </summary>
        public int PlayCount { get; internal set; } = -1;

        /// <summary>
        ///     预览音频的Url
        /// </summary>
        public string PreviewUrl { get; internal set; } = "";

        /// <summary>
        ///     谱面集的来源
        /// </summary>
        public string Source { get; internal set; } = "";

        /// <summary>
        ///     谱面集的状态
        /// </summary>
        public BeatmapStatus Status { get; internal set; } = BeatmapStatus.None;

        /// <summary>
        ///     谱面集的英文标题
        /// </summary>
        public string Title { get; internal set; } = "";

        /// <summary>
        ///     谱面集的标题
        /// </summary>
        public string TitleUnicode { get; internal set; } = "";

        /// <summary>
        ///     谱面集有无视频
        /// </summary>
        public bool HasVideo { get; internal set; }

        /// <summary>
        ///     谱面集是否可下载及不可下载的原因
        /// </summary>
        public Availibility BeatmapAvailability { get; internal set; } = new Availibility();

        /// <summary>
        ///     每分钟节奏数
        /// </summary>
        public double BPM { get; internal set; } = -1;

        /// <summary>
        ///     谱面集被讨论的情况
        /// </summary>
        public Hype HypeStatus { get; internal set; } = new Hype();

        /// <summary>
        ///     是否计入分数
        /// </summary>
        public bool IsScoreable { get; internal set; }

        /// <summary>
        ///     上次更新的时间
        /// </summary>
        public DateTime? LastUpdate { get; internal set; }

        /// <summary>
        ///     谱面集的留言板的Url
        /// </summary>
        public string LegacyThreadUrl { get; internal set; } = "";

        /// <summary>
        ///     被提名的情况
        /// </summary>
        public Nomination Nominations { get; internal set; } = new Nomination();

        /// <summary>
        ///     谱面集是否Ranked
        /// </summary>
        public bool Ranked { get; internal set; }

        /// <summary>
        ///     谱面集Ranked的时间
        /// </summary>
        public DateTime? RankedDate { get; internal set; }

        /// <summary>
        ///     谱面集有无StoryBoard
        /// </summary>
        public bool HasStoryBoard { get; internal set; }

        /// <summary>
        ///     提交谱面集的时间
        /// </summary>
        public DateTime? SubmittedDate { get; internal set; }

        /// <summary>
        ///     谱面集标签
        /// </summary>
        public string Tags { get; internal set; } = "";

        /// <summary>
        ///     谱面集的评价
        /// </summary>
        public List<double> Rating { get; internal set; } = new List<double>();
    }
}