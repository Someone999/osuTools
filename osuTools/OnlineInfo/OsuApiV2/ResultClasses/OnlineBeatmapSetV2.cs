using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using osuTools.MusicPlayer;
using osuTools.OnlineInfo.OsuApiV1.OnlineQueries;

namespace osuTools.OnlineInfo.OsuApiV2.ResultClasses
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
            JObject setinfo;
            if (json.GetValue("beatmaps") != null)
            {
                setinfo = json;
                var beatmaps = (JArray) json["beatmaps"];
                foreach (var js in beatmaps)
                    Beatmaps.Add(new OnlineBeatmapV2((JObject) js,this));
            }
            else
            {
                setinfo = (JObject) json["beatmapset"];
                Beatmaps.Add(new OnlineBeatmapV2(json,this));
            }

            Artist = setinfo["artist"].ToString();
            ArtistUnicode = setinfo["artist_unicode"].ToString();
            Creator = setinfo["creator"].ToString();
            FavoriteCount = setinfo["favourite_count"].ToObject<int>();
            SetId = setinfo["id"].ToObject<int>();
            PlayCount = setinfo["play_count"].ToObject<int>();
            PreviewUrl = "https:" + setinfo["preview_url"];
            Source = setinfo["source"].ToString();
            var arr = setinfo["status"].ToString().ToCharArray();
            arr[0] = char.ToUpper(arr[0]);
            Status = (BeatmapStatus) Enum.Parse(typeof(BeatmapStatus), new string(arr));
            Title = setinfo["title"].ToString();
            TitleUnicode = setinfo["title_unicode"].ToString();
            CreatorUserId = setinfo["user_id"].ToObject<int>();
            HasVideo = setinfo["video"].ToObject<bool>();
            Bpm = setinfo["bpm"].ToObject<double>();
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
            if(nomin?.HasValues ?? false)
            {
                Nominations.CurrentNominations = nomin["current"]?.ToObject<int>() ?? -1;
                Nominations.RequiredNominations = nomin["required"]?.ToObject<int>() ?? -1;
            }

            #endregion

            #region 谱面能否被宣传(???)


            var hyped = setinfo["hype"];
            if (hyped?.HasValues ?? false)
            {
                HypeStatus.CanBeHyped = setinfo["can_be_hyped"]?.ToObject<bool>() ?? false;
                HypeStatus.CurrentHyped = hyped["current"]?.ToObject<int>() ?? -1;
                HypeStatus.RequiredHype = hyped["required"]?.ToObject<int>() ?? -1;
            }

            #endregion

            #region 谱面是否可下载

            var ava = setinfo["availability"];
            if (ava?.HasValues ?? false)
            {
                BeatmapAvailability.DownloadDisabled = ava["download_disabled"]?.ToObject<bool>() ?? false;
                BeatmapAvailability.MoreInformation = ava["more_information"]?.ToString();
            }

            #endregion

            #region 封面等图片

            var covers = (JObject) setinfo["covers"];
            if (covers?.HasValues ?? false)
            {
                Covers.Card = covers["card"]?.ToString();
                Covers.Card2X = covers["card@2x"]?.ToString();
                Covers.Cover = covers["cover"]?.ToString();
                Covers.Cover2X = covers["cover@2x"]?.ToString();
                Covers.List = covers["list"]?.ToString();
                Covers.List2X = covers["list@2x"]?.ToString();
                Covers.SlimCover = covers["slimcover"]?.ToString();
                Covers.SlimCover2X = covers["slimcover@2x"]?.ToString();
            }

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
        public List<OnlineBeatmapV2> Beatmaps { get; } = new List<OnlineBeatmapV2>();

        /// <summary>
        ///     谱面集对应曲目的艺术家的英文名
        /// </summary>
        public string Artist { get; } = "";

        /// <summary>
        ///     谱面集对应曲目的艺术家
        /// </summary>
        public string ArtistUnicode { get; } = "";

        /// <summary>
        ///     谱面集的各种封面图片的Url
        /// </summary>
        public ImageUrl Covers { get; } = new ImageUrl();

        /// <summary>
        ///     谱面集的创作者
        /// </summary>
        public string Creator { get; } = "";

        /// <summary>
        ///     谱面集的创作者的UserID
        /// </summary>
        public int CreatorUserId { get; } = -1;

        /// <summary>
        ///     谱面集被收藏的次数
        /// </summary>
        public int FavoriteCount { get; } = -1;

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int SetId { get; } = -1;

        /// <summary>
        ///     谱面集被游玩的次数
        /// </summary>
        public int PlayCount { get;  } = -1;

        /// <summary>
        ///     预览音频的Url
        /// </summary>
        public string PreviewUrl { get; } = "";

        /// <summary>
        ///     谱面集的来源
        /// </summary>
        public string Source { get;  } = "";

        /// <summary>
        ///     谱面集的状态
        /// </summary>
        public BeatmapStatus Status { get;  } = BeatmapStatus.None;

        /// <summary>
        ///     谱面集的英文标题
        /// </summary>
        public string Title { get;} = "";

        /// <summary>
        ///     谱面集的标题
        /// </summary>
        public string TitleUnicode { get; } = "";

        /// <summary>
        ///     谱面集有无视频
        /// </summary>
        public bool HasVideo { get;}

        /// <summary>
        ///     谱面集是否可下载及不可下载的原因
        /// </summary>
        public Availibility BeatmapAvailability { get;} = new Availibility();

        /// <summary>
        ///     每分钟节奏数
        /// </summary>
        public double Bpm { get;} = -1;

        /// <summary>
        ///     谱面集被讨论的情况
        /// </summary>
        public Hype HypeStatus { get; } = new Hype();

        /// <summary>
        ///     是否计入分数
        /// </summary>
        public bool IsScoreable { get; }

        /// <summary>
        ///     上次更新的时间
        /// </summary>
        public DateTime? LastUpdate { get; }

        /// <summary>
        ///     谱面集的留言板的Url
        /// </summary>
        public string LegacyThreadUrl { get; } = "";

        /// <summary>
        ///     被提名的情况
        /// </summary>
        public Nomination Nominations { get; } = new Nomination();

        /// <summary>
        ///     谱面集是否Ranked
        /// </summary>
        public bool Ranked { get; }

        /// <summary>
        ///     谱面集Ranked的时间
        /// </summary>
        public DateTime? RankedDate { get; }

        /// <summary>
        ///     谱面集有无StoryBoard
        /// </summary>
        public bool HasStoryBoard { get; }

        /// <summary>
        ///     提交谱面集的时间
        /// </summary>
        public DateTime? SubmittedDate { get; }

        /// <summary>
        ///     谱面集标签
        /// </summary>
        public string Tags { get; } = "";

        /// <summary>
        ///     谱面集的评价
        /// </summary>
        public List<double> Rating { get; } = new List<double>();
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is OnlineBeatmapSetV2 beatmapSet)
                return beatmapSet.SetId == SetId;
            return false;
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return SetId << 3;
        }
        ///<inheritdoc/>
        public override string ToString()
        {
            return $"{Artist} - {Title}";
        }

        /// <summary>
        /// 从互联网加载预览音频
        /// </summary>
        /// <returns></returns>
        public BassMusicPlayer LoadPreviewAudio() =>
            !string.IsNullOrEmpty(PreviewUrl) ? new BassMusicPlayer(PreviewUrl) : null;
    }
}