using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.Online.ApiV1.Querier;

namespace osuTools.Online.ApiV1
{
    /// <summary>
    ///     一个谱面的最高100个记录之一。
    /// </summary>
    [Serializable]
    public partial class OnlineScore : PpSorted, IFormattable
    {
        private int
            _beatmapId = -1,
            _maxcombo,
            _count50,
            _count100,
            _count300,
            _countmiss,
            _countkatu,
            _countgeki,
            _perfect,
            _userId;

        private DateTime _d;

        private string
            _date;

        private int _mod;
        private double _pp;
        private int _score, _replayAvailable;

        private uint
            _scoreId;

        /// <summary>
        ///     创造一个空的OnlineScore对象
        /// </summary>
        public OnlineScore()
        {
            _replayAvailable = 0;
            Perfect = false;
            _d = DateTime.MinValue;
            ReplayAvailable = false;
            _scoreId = 0;
            _score = 0;
            _pp = 0.0;
            _maxcombo = 0;
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
            Mods = ModList.FromInteger(_mod);
        }

        /// <summary>
        ///     使用Json填充OnlineScore对象并且指定游戏模式和BeatmapID
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mode"></param>
        /// <param name="beatmapId"></param>
        public OnlineScore(string json, OsuGameMode mode, int beatmapId)
        {
            //try
            {
                BeatmapId = beatmapId;
                Mode = mode;
                var jobj = JsonConvert.DeserializeObject(json);
                var cjobj = new JObject();
                if (jobj.GetType() == typeof(JObject)) cjobj = (JObject) jobj;
                if (jobj.GetType() == typeof(JArray)) cjobj = (JObject) ((JArray) jobj)[0];

                int.TryParse(cjobj["countgeki"].ToString(), out _countgeki);
                int.TryParse(cjobj["countkatu"].ToString(), out _countkatu);
                int.TryParse(cjobj["count300"].ToString(), out _count300);
                int.TryParse(cjobj["count100"].ToString(), out _count100);
                int.TryParse(cjobj["count50"].ToString(), out _count50);
                int.TryParse(cjobj["countmiss"].ToString(), out _countmiss);
                int.TryParse(cjobj["maxcombo"].ToString(), out _maxcombo);
                int.TryParse(cjobj["score"].ToString(), out _score);
                int.TryParse(cjobj["user_id"].ToString(), out _userId);
                int.TryParse(cjobj["perfect"].ToString(), out _perfect);
                int.TryParse(cjobj["replay_available"].ToString(), out _replayAvailable);
                uint.TryParse(cjobj["score_id"].ToString(), out _scoreId);
                int.TryParse(cjobj["enabled_mods"].ToString(), out _mod);
                double.TryParse(cjobj["pp"].ToString(), out _pp);
                _date = cjobj["date"].ToString();
                Rank = cjobj["rank"].ToString();
                Mods = ModList.FromInteger(_mod);
                Accuracy = AccCalc(Mode);
                DateTime.TryParse(_date, out _d);
                if (_perfect == 1)
                    Perfect = true;
                else if (_perfect == 0) Perfect = false;
                ReplayAvailable = _replayAvailable == 1;
            }
            /* catch
                 {
                     replay_available = 0;
                     per = false;
                     d = DateTime.MinValue;
                     rep = false;
                     score_id = 0;
                     score = 0;
                     pp = 0.0;
                     maxcombo = 0;
                     count300 = 0;
                     count100 = 0;
                     count50 = 0;
                     countgeki = 0;
                     countkatu = 0;
                     countmiss = 0;
                     perfect = 0;
                     user_id = 0;
                     date = "0-0-0 0:0:0";
                     rank = "?";
                 }*/
        }

        /// <summary>
        ///     使用Json填充一个OnlineScore，并指定BeamapID
        /// </summary>
        /// <param name="json"></param>
        /// <param name="beatmapid"></param>
        public OnlineScore(string json, int beatmapid)
        {
            //try
            {
                var jobj = JsonConvert.DeserializeObject(json);
                var cjobj = new JObject();
                if (jobj.GetType() == typeof(JObject)) cjobj = (JObject) jobj;
                if (jobj.GetType() == typeof(JArray)) cjobj = (JObject) ((JArray) jobj)[0];
                if (beatmapid > 0) _beatmapId = beatmapid;
                int.TryParse(cjobj["countgeki"].ToString(), out _countgeki);
                int.TryParse(cjobj["countkatu"].ToString(), out _countkatu);
                int.TryParse(cjobj["count300"].ToString(), out _count300);
                int.TryParse(cjobj["count100"].ToString(), out _count100);
                int.TryParse(cjobj["count50"].ToString(), out _count50);
                int.TryParse(cjobj["countmiss"].ToString(), out _countmiss);
                int.TryParse(cjobj["maxcombo"].ToString(), out _maxcombo);
                int.TryParse(cjobj["score"].ToString(), out _score);
                int.TryParse(cjobj["user_id"].ToString(), out _userId);
                int.TryParse(cjobj["perfect"].ToString(), out _perfect);
                int.TryParse(cjobj["replay_available"].ToString(), out _replayAvailable);
                uint.TryParse(cjobj["score_id"].ToString(), out _scoreId);
                int.TryParse(cjobj["enabled_mods"].ToString(), out _mod);
                double.TryParse(cjobj["pp"].ToString(), out _pp);
                _date = cjobj["date"].ToString();
                Rank = cjobj["rank"].ToString();
                Mods = ModList.FromInteger(_mod);
                DateTime.TryParse(_date, out _d);
                if (_perfect == 1)
                    Perfect = true;
                else if (_perfect == 0) Perfect = false;
                ReplayAvailable = _replayAvailable == 1;
            }
        }

        public string QuerierApiKey { get; set; }

        /// <summary>
        ///     本次游玩中使用的Mods
        /// </summary>
        public ModList Mods { get; } = new ModList();

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId { get; private set; }

        /// <summary>
        ///     准度
        /// </summary>
        public double Accuracy { get; private set; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; private set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var b = new StringBuilder(format);
            b.Replace("perfect", Perfect.ToString());
            b.Replace("pp", Pp.ToString());
            b.Replace("Count300g", C300G.ToString());
            b.Replace("c300", C300.ToString());
            b.Replace("Count200", C200.ToString());
            b.Replace("Count100", C100.ToString());
            b.Replace("Count50", C50.ToString());
            b.Replace("CountMiss", CMiss.ToString());
            b.Replace("userid", UserId.ToString());
            b.Replace("rank", Rank);
            b.Replace("playtime", _d.ToString("yyyy/MM/dd HH:mm:ss"));
            b.Replace("score", Score.ToString());
            b.Replace("beatmapid", BeatmapId.ToString());
            b.Replace("maxcombo", MaxCombo.ToString());
            b.Replace("acc", Accuracy.ToString("p2"));
            b.Replace("mode", Mode.ToString());
            return b.ToString();
        }

        /// <summary>
        ///     以"用户ID PP 分数"为格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{UserId} {Pp} {Score}";
        }

        private double AccCalc(OsuGameMode mode)
        {
            return GameMode.FromLegacyMode(mode).AccuracyCalc(new ScoreInfo
                {CountGeki = C300G, Count300 = C300, CountKatu = C200, Count100 = C100, Count50 = C50, CountMiss = CMiss});
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
            var user = q.UserInfo;
            return user;
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }
    }
}