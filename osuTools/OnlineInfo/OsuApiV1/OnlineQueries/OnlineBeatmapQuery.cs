using System;
using System.Text;
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
}