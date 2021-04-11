using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.Game.Mods;
using osuTools.Online.ApiV1.Querier;

namespace osuTools.Online.ApiV1
{
    /// <summary>
    ///     最近24小时打出的成绩
    /// </summary>
    [Serializable]
    public partial class RecentOnlineResult : SortByScore, IComparable<RecentOnlineResult>, IFormattable
    {
        private double _acc;

        private int
            _beatmapId;

        private DateTime _d;

        private string
            _date;

        private int
            _maxcombo,
            _count50,
            _count100,
            _count300,
            _countmiss,
            _countkatu,
            _countgeki,
            _perfect,
            _userId;

        private int _mod;
        private int _score;

        /// <summary>
        ///     创造一个空白的RecentOnlineResult对象
        /// </summary>
        public RecentOnlineResult()
        {
            Perfect = false;
            _d = DateTime.MinValue;
            _beatmapId = 0;
            _score = 0;
            _mod = 0;
            _maxcombo = 0;
            Mods = ModList.FromInteger(_mod);
            _count300 = 0;
            _count100 = 0;
            _count50 = 0;
            _countgeki = 0;
            _countkatu = 0;
            _countmiss = 0;
            _perfect = 0;
            _userId = 0;
            _date = "0-0-0 0:0:0";
            Rank = "?";
        }

        /// <summary>
        ///     使用json字符串和游戏模式初始化一个RecentOnlineResult
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mode"></param>
        public RecentOnlineResult(string json, OsuGameMode mode)
        {
            Mode = mode;
            var jobj = (JObject) JsonConvert.DeserializeObject(json);
            int.TryParse(jobj["countgeki"].ToString(), out _countgeki);
            int.TryParse(jobj["countkatu"].ToString(), out _countkatu);
            int.TryParse(jobj["count300"].ToString(), out _count300);
            int.TryParse(jobj["count100"].ToString(), out _count100);
            int.TryParse(jobj["count50"].ToString(), out _count50);
            int.TryParse(jobj["countmiss"].ToString(), out _countmiss);
            int.TryParse(jobj["maxcombo"].ToString(), out _maxcombo);
            int.TryParse(jobj["score"].ToString(), out _score);
            int.TryParse(jobj["user_id"].ToString(), out _userId);
            int.TryParse(jobj["perfect"].ToString(), out _perfect);
            int.TryParse(jobj["enabled_mods"].ToString(), out _mod);
            Mods = ModList.FromInteger(_mod);
            int.TryParse(jobj["beatmap_id"].ToString(), out _beatmapId);
            _date = jobj["date"].ToString();
            Rank = jobj["rank"].ToString();
            DateTime.TryParse(_date, out _d);
            DateTime e;
            e = TimeZone.CurrentTimeZone.ToLocalTime(_d);
            _d = e;
            if (_perfect == 1)
                Perfect = true;
            else if (_perfect == 0) Perfect = false;
            Accuracy = AccCalc(Mode);
        }

        public string QuerierApiKey { get; set; }

        /// <summary>
        ///     本次游戏使用的Mods
        /// </summary>
        public ModList Mods { get; } = new ModList();

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; private set; }

        /// <summary>
        ///     准度
        /// </summary>
        public double Accuracy { get; private set; }

        /// <summary>
        ///     与另一个RecentOnlineResult的分数进行比较
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CompareTo(RecentOnlineResult r)
        {
            if (_score > r._score) return -1;
            if (_score == r._score) return 0;
            if (_score < r._score) return 1;
            return 0;
        }

        /// <summary>
        ///     使用指定的格式创建字符串
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var b = new StringBuilder(format);
            b.Replace("perfect", Perfect.ToString());
            b.Replace("Count300g", C300G.ToString());
            b.Replace("c300", C300.ToString());
            b.Replace("Count200", C200.ToString());
            b.Replace("Count100", C100.ToString());
            b.Replace("Count50", C50.ToString());
            b.Replace("CountMiss", CMiss.ToString());
            b.Replace("maxcombo", MaxCombo.ToString());
            b.Replace("userid", UserId.ToString());
            b.Replace("rank", Rank);
            b.Replace("playtime", _d.ToString("yyyy/MM/dd HH:mm:ss"));
            b.Replace("score", Score.ToString());
            b.Replace("beatmapid", BeatmapId.ToString());
            b.Replace("acc", Accuracy.ToString("p2"));
            return b.ToString();
        }

        /// <summary>
        ///     获取该成绩对应的谱面
        /// </summary>
        /// <returns></returns>
        public OnlineBeatmap GetOnlineBeatmap()
        {
            var q = new OnlineBeatmapQuery();
            q.OsuApiKey = QuerierApiKey;
            q.BeatmapId = _beatmapId;
            var beatmap = q.Beatmaps[0];
            return beatmap;
        }

        /// <summary>
        ///     获取游玩该谱面的玩家的信息
        /// </summary>
        /// <returns></returns>
        public OnlineUser GetUser()
        {
            var q = new OnlineUserQuery();
            q.UserId = _userId;
            q.OsuApiKey = QuerierApiKey;
            return q.UserInfo;
        }

        private double AccCalc(OsuGameMode mode)
        {
            double c3G = C300G, c3 = C300, c2 = C200, c1 = C100, c5 = C50, cm = CMiss;
            double a2 = 2.0 / 3, a1 = 1.0 / 3, a5 = 1.0 / 6;
            var mall = c3 + c3G + c2 + c1 + c5 + cm;
            var sall = c3 + c1 + c5 + cm;
            var call = c3 + c1 + c2 + c5 + cm;
            var tall = c3 + c3G + c1 + c2 + cm;
            switch (mode)
            {
                case OsuGameMode.Catch: return (c3 + c1 + c5) / call;
                case OsuGameMode.Osu: return (c3 + c1 * a1 + c5 * a5) / sall;
                case OsuGameMode.Taiko: return (c3 + c3G + (c1 + c2) * a1) / tall;
                case OsuGameMode.Mania: return (c3 + c3G + c2 * a2 + c1 * a1 + c5 * a5) / mall;
                default: return 0;
            }
        }

        /// <summary>
        ///     使用指定的格式创建字符串
        /// </summary>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return ToString(format, null);
        }
    }
}