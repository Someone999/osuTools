using System;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using osuTools.Game.Mods;

namespace osuTools.Online.ApiV1.Querier
{
    /// <summary>
    ///     在线查询一个或多个谱面
    /// </summary>
    public class OnlineBeatmapQuery
    {
        private int limit = 100;
        private bool queried;
        private OnlineBeatmapCollection rec = new OnlineBeatmapCollection();

        /// <summary>
        ///     查询到的谱面
        /// </summary>
        public OnlineBeatmapCollection Beatmaps
        {
            get
            {
                if (!queried)
                {
                    GetResult();
                    queried = true;
                    return rec;
                }

                return rec;
            }
            private set => rec = value;
        }

        /// <summary>
        ///     OsuApi的密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     作者的用户名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        ///     作者的用户ID
        /// </summary>
        public int CreatorUserID { get; set; }

        /// <summary>
        ///     查询的最大数量，默认为100
        /// </summary>
        public int Limit
        {
            get => limit;
            set
            {
                if (OnlineQueryTools.InRange(0, 500, value)) limit = value;
                else limit = 100;
            }
        }

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapID { get; set; }

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int BeatmapSetID { get; set; }

        /// <summary>
        ///     Ranked或Loved的时间
        /// </summary>
        public DateTime RankedOrLovedSince { get; set; } = new DateTime();

        /// <summary>
        ///     包含转谱
        /// </summary>
        public bool IncludeConvertedBeatmap { get; set; } = false;

        /// <summary>
        ///     谱面模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Unkonwn;

        /// <summary>
        ///     要附带查询的Mod
        /// </summary>
        public ModList Mods { get; set; }

        /// <summary>
        ///     生成查询Uri
        /// </summary>
        /// <returns></returns>
        public Uri UriGenerator()
        {
            if (string.IsNullOrEmpty(OsuApiKey) || string.IsNullOrWhiteSpace(OsuApiKey)) throw new ArgumentException();
            var baseuri = $"https://osu.ppy.sh/api/get_beatmaps?k={OsuApiKey}";
            string id = $"&b={BeatmapID}",
                setid = $"&s={BeatmapSetID}",
                incconver = $"&a={(IncludeConvertedBeatmap ? 1 : 0)}",
                hash = $"&h={Hash}",
                mode = $"&m={(int) Mode}",
                lim = $"&limit={Limit}",
                uname = $"&u={CreatorUserName}&type=string",
                userid = $"&u={CreatorUserID}&type=id",
                since = $"&since={RankedOrLovedSince:YYYY-MM-DD}",
                mods = $"&mods={Mods.ToIntMod()}";
            var builder = new StringBuilder(baseuri);
            builder.Append(string.IsNullOrEmpty(CreatorUserName) ? CreatorUserID == 0 ? "" : userid : uname);
            builder.Append(Limit != 0 ? lim : "");
            builder.Append(!string.IsNullOrEmpty(Hash) ? hash : "");
            builder.Append(Mode != OsuGameMode.Unkonwn ? mode : "");
            builder.Append(Mods.Count == 0 ? mods : "");
            builder.Append(RankedOrLovedSince != new DateTime() ? since : "");
            builder.Append(BeatmapID != 0 ? id : setid);
            builder.Append(IncludeConvertedBeatmap ? incconver : "");
            return new Uri(builder.ToString());
        }

        private void GetResult()
        {
            var c = new OnlineBeatmapCollection();
            var q = OnlineQueryTools.GetResponse(UriGenerator());
            if (q.Results.Count == 0)
            {
                c.Failed = true;
                return;
            }

            if (q.Results != null)
                foreach (JObject result in q.Results)
                    c.Beatmaps.Add(new OnlineBeatmap(result));
            Beatmaps = c;
        }
    }

    /// <summary>
    ///     在线查询用户信息
    /// </summary>
    public class OnlineUserQuery
    {
        private bool queried;
        private OnlineUser rec = new OnlineUser();

        /// <summary>
        ///     查询到的用户信息
        /// </summary>
        public OnlineUser UserInfo
        {
            get
            {
                if (!queried)
                {
                    GetResult();
                    queried = true;
                    return rec;
                }

                return rec;
            }
            private set => rec = value;
        }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     要查询的模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Osu;

        /// <summary>
        ///     osuApi密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     距离上次时间的最大天数
        /// </summary>
        public int MaxDaysLastEventBefore { get; set; } = 1;

        /// <summary>
        ///     生成查询的Uri
        /// </summary>
        /// <returns></returns>
        public Uri UriGenerator()
        {
            var baseuri = $"https://osu.ppy.sh/api/get_user?k={OsuApiKey}";
            if (UserID == 0 && string.IsNullOrEmpty(UserName))
                throw new ArgumentException();
            string usern = $"&u={UserName}&type=string",
                userid = $"&u={UserID}&type=id",
                mode = $"&m={(int) Mode}",
                eventdays = $"&event_days={MaxDaysLastEventBefore}";
            var b = new StringBuilder(baseuri);
            b.Append(UserID == 0 ? usern : userid);
            b.Append(mode);
            b.Append(eventdays);
            return new Uri(b.ToString());
        }

        private void GetResult()
        {
            UserInfo = new OnlineUser(OnlineQueryTools.GetResponse(UriGenerator()).Results);
        }
    }

    /// <summary>
    ///     在线查询用户最近的游玩记录
    /// </summary>
    public class OnlineRecentRecordQuery
    {
        private int lim = 10;
        private bool queried;
        private OnlineRecentResultCollection res = new OnlineRecentResultCollection();

        /// <summary>
        ///     OsuApi的密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Osu;

        /// <summary>
        ///     查询的最大数量
        /// </summary>
        public int Limit
        {
            get => lim;
            set
            {
                if (OnlineQueryTools.InRange(0, 50, value))
                    lim = value;
            }
        }

        /// <summary>
        ///     查询结果
        /// </summary>
        public OnlineRecentResultCollection Results
        {
            get
            {
                if (!queried)
                {
                    GetResult();
                    queried = true;
                }

                return res;
            }
            private set => res = value;
        }

        private void GetResult()
        {
            if (UserID == 0)
                if (string.IsNullOrEmpty(UserName))
                    throw new ArgumentException("必须指定用户名或用户ID。");
            var basestr = $"https://osu.ppy.sh/api/get_user_recent?k={OsuApiKey}";
            var b = new StringBuilder(basestr);
            b.Append(UserID != 0
                ? $"&u={UserID}&type=id&m={(int) Mode}&limit={Limit}"
                : $"&u={UserName}&type=string&m={(int) Mode}&limit={Limit}");
            var q = OnlineQueryTools.GetResponse(new Uri(b.ToString()));
            foreach (var json in q.Results) res.RecentResults.Add(new RecentOnlineResult(json.ToString(), Mode));
        }
    }

    /// <summary>
    ///     在线查询一个谱面的游玩记录
    /// </summary>
    public class OnlineScoresQuery
    {
        private int lim = 50;
        private bool queried;
        private OnlineScoreCollection res = new OnlineScoreCollection();

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapID { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Osu;

        /// <summary>
        ///     OsuApi密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     要指定的Mod
        /// </summary>
        public ModList Mods { get; set; } = new ModList();

        /// <summary>
        ///     查询结果
        /// </summary>
        public OnlineScoreCollection Result
        {
            get
            {
                if (!queried)
                {
                    GetResult();
                    queried = true;
                }

                return res;
            }
            private set => res = value;
        }

        /// <summary>
        ///     查询的最大数量，默认为50
        /// </summary>
        public int Limit
        {
            get => lim;
            set
            {
                if (OnlineQueryTools.InRange(0, 100, value))
                    lim = value;
            }
        }

        private void GetResult()
        {
            if (BeatmapID == 0)
                throw new ArgumentException("必须指定谱面ID。");
            var basestr = $"https://osu.ppy.sh/api/get_scores?k={OsuApiKey}";
            var b = new StringBuilder(basestr);
            b.Append(UserID != 0 ? $"&u={UserID}" :
                string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName) ? "" : $"&u={UserName}");
            b.Append($"&b={BeatmapID}");
            b.Append(Mods.Count == 0 ? "" : $"&mods={Mods.ToIntMod()}");
            b.Append($"&m={(int) Mode}");
            MessageBox.Show(b.ToString());
            var q = OnlineQueryTools.GetResponse(new Uri(b.ToString()));
            foreach (var json in q.Results) res.Scores.Add(new OnlineScore(json.ToString(), Mode, BeatmapID));
        }
    }

    /// <summary>
    ///     在线查询玩家的最佳记录
    /// </summary>
    public class OnlineUserBestQuery
    {
        private int lim = 10;
        private bool queried;
        private OnlineBestRecordCollection res = new OnlineBestRecordCollection();

        /// <summary>
        ///     osuApi的密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Osu;

        /// <summary>
        ///     查询的最大数目，默认为10
        /// </summary>
        public int Limit
        {
            get => lim;
            set
            {
                if (OnlineQueryTools.InRange(0, 50, value))
                    lim = value;
            }
        }

        /// <summary>
        ///     查询结果
        /// </summary>
        public OnlineBestRecordCollection Results
        {
            get
            {
                if (!queried)
                {
                    GetResult();
                    queried = true;
                }

                return res;
            }
            private set => res = value;
        }

        private void GetResult()
        {
            if (UserID == 0)
                if (string.IsNullOrEmpty(UserName))
                    throw new ArgumentException("必须指定用户名或用户ID。");
            var basestr = $"https://osu.ppy.sh/api/get_user_best?k={OsuApiKey}";
            var b = new StringBuilder(basestr);
            b.Append(UserID != 0 ? $"&u={UserID}&type=id&m={(int) Mode}" : $"&u={UserName}&type=string&m={(int) Mode}");
            var q = OnlineQueryTools.GetResponse(new Uri(b.ToString()));
            if (q.Results.Count == 0) res.Failed = true;
            foreach (var json in q.Results) res.Records.Add(new OnlineBestRecord(json.ToString(), Mode));
        }
    }
}