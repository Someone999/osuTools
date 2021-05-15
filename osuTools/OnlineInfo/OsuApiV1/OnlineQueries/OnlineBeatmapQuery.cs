using System;
using System.Text;
using Newtonsoft.Json.Linq;
using osuTools.Game.Modes;
using osuTools.Game.Mods;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     在线查询一个或多个谱面
    /// </summary>
    public class OnlineBeatmapQuery
    {
        private int _limit = 100;
        private bool _queried;
        private OnlineBeatmapCollection _rec = new OnlineBeatmapCollection();

        /// <summary>
        ///     查询到的谱面
        /// </summary>
        public OnlineBeatmapCollection Beatmaps
        {
            get
            {
                if (!_queried)
                {
                    GetResult();
                    _queried = true;
                    return _rec;
                }

                return _rec;
            }
            private set => _rec = value;
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
        public int CreatorUserId { get; set; }

        /// <summary>
        ///     查询的最大数量，默认为100
        /// </summary>
        public int Limit
        {
            get => _limit;
            set => _limit = OnlineQueryTools.InRange(0, 500, value) ? value : 100;
        }

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId { get; set; }

        /// <summary>
        ///     谱面集ID
        /// </summary>
        public int BeatmapSetId { get; set; }

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
            string id = $"&b={BeatmapId}",
                setid = $"&s={BeatmapSetId}",
                incconver = $"&a={(IncludeConvertedBeatmap ? 1 : 0)}",
                hash = $"&h={Hash}",
                mode = $"&m={(int) Mode}",
                lim = $"&limit={Limit}",
                uname = $"&u={CreatorUserName}&type=string",
                userid = $"&u={CreatorUserId}&type=id",
                since = $"&since={RankedOrLovedSince:YYYY-MM-DD}",
                mods = $"&mods={Mods.ToIntMod()}";
            var builder = new StringBuilder(baseuri);
            builder.Append(string.IsNullOrEmpty(CreatorUserName) ? CreatorUserId == 0 ? "" : userid : uname);
            builder.Append(Limit != 0 ? lim : "");
            builder.Append(!string.IsNullOrEmpty(Hash) ? hash : "");
            builder.Append(Mode != OsuGameMode.Unkonwn ? mode : "");
            builder.Append(Mods.Count == 0 ? mods : "");
            builder.Append(RankedOrLovedSince != new DateTime() ? since : "");
            builder.Append(BeatmapId != 0 ? id : setid);
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