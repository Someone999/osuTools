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
    public partial class OnlineScore : PPSorted, IFormattable
    {
        private int
            beatmap_id = -1,
            maxcombo,
            count50,
            count100,
            count300,
            countmiss,
            countkatu,
            countgeki,
            perfect,
            user_id;

        private DateTime d;

        private string
            date;

        private int mod;
        private double pp;
        private int score, replay_available;

        private uint
            score_id;

        /// <summary>
        ///     创造一个空的OnlineScore对象
        /// </summary>
        public OnlineScore()
        {
            replay_available = 0;
            Perfect = false;
            d = DateTime.MinValue;
            ReplayAvailable = false;
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
            Rank = "?";
            Mods = ModList.FromInteger(mod);
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
                BeatmapID = beatmapId;
                Mode = mode;
                var jobj = JsonConvert.DeserializeObject(json);
                var cjobj = new JObject();
                if (jobj.GetType() == typeof(JObject)) cjobj = (JObject) jobj;
                if (jobj.GetType() == typeof(JArray)) cjobj = (JObject) ((JArray) jobj)[0];

                int.TryParse(cjobj["countgeki"].ToString(), out countgeki);
                int.TryParse(cjobj["countkatu"].ToString(), out countkatu);
                int.TryParse(cjobj["count300"].ToString(), out count300);
                int.TryParse(cjobj["count100"].ToString(), out count100);
                int.TryParse(cjobj["count50"].ToString(), out count50);
                int.TryParse(cjobj["countmiss"].ToString(), out countmiss);
                int.TryParse(cjobj["maxcombo"].ToString(), out maxcombo);
                int.TryParse(cjobj["score"].ToString(), out score);
                int.TryParse(cjobj["user_id"].ToString(), out user_id);
                int.TryParse(cjobj["perfect"].ToString(), out perfect);
                int.TryParse(cjobj["replay_available"].ToString(), out replay_available);
                uint.TryParse(cjobj["score_id"].ToString(), out score_id);
                int.TryParse(cjobj["enabled_mods"].ToString(), out mod);
                double.TryParse(cjobj["pp"].ToString(), out pp);
                date = cjobj["date"].ToString();
                Rank = cjobj["rank"].ToString();
                Mods = ModList.FromInteger(mod);
                Accuracy = AccCalc(Mode);
                DateTime.TryParse(date, out d);
                if (perfect == 1)
                    Perfect = true;
                else if (perfect == 0) Perfect = false;
                if (replay_available == 1)
                    ReplayAvailable = true;
                else
                    ReplayAvailable = false;
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
                if (beatmapid > 0) beatmap_id = beatmapid;
                int.TryParse(cjobj["countgeki"].ToString(), out countgeki);
                int.TryParse(cjobj["countkatu"].ToString(), out countkatu);
                int.TryParse(cjobj["count300"].ToString(), out count300);
                int.TryParse(cjobj["count100"].ToString(), out count100);
                int.TryParse(cjobj["count50"].ToString(), out count50);
                int.TryParse(cjobj["countmiss"].ToString(), out countmiss);
                int.TryParse(cjobj["maxcombo"].ToString(), out maxcombo);
                int.TryParse(cjobj["score"].ToString(), out score);
                int.TryParse(cjobj["user_id"].ToString(), out user_id);
                int.TryParse(cjobj["perfect"].ToString(), out perfect);
                int.TryParse(cjobj["replay_available"].ToString(), out replay_available);
                uint.TryParse(cjobj["score_id"].ToString(), out score_id);
                int.TryParse(cjobj["enabled_mods"].ToString(), out mod);
                double.TryParse(cjobj["pp"].ToString(), out pp);
                date = cjobj["date"].ToString();
                Rank = cjobj["rank"].ToString();
                Mods = ModList.FromInteger(mod);
                DateTime.TryParse(date, out d);
                if (perfect == 1)
                    Perfect = true;
                else if (perfect == 0) Perfect = false;
                if (replay_available == 1)
                    ReplayAvailable = true;
                else
                    ReplayAvailable = false;
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
        public int BeatmapID { get; private set; }

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
            b.Replace("pp", PP.ToString());
            b.Replace("c300g", c300g.ToString());
            b.Replace("c300", c300.ToString());
            b.Replace("c200", c200.ToString());
            b.Replace("c100", c100.ToString());
            b.Replace("c50", c50.ToString());
            b.Replace("cMiss", cMiss.ToString());
            b.Replace("userid", UserID.ToString());
            b.Replace("rank", Rank);
            b.Replace("playtime", d.ToString("yyyy/MM/dd HH:mm:ss"));
            b.Replace("score", Score.ToString());
            b.Replace("beatmapid", BeatmapID.ToString());
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
            return $"{UserID} {PP} {Score}";
        }

        private double AccCalc(OsuGameMode mode)
        {
            return GameMode.FromLegacyMode(mode).AccuracyCalc(new ScoreInfo
                {c300g = c300g, c300 = c300, c200 = c200, c100 = c100, c50 = c50, cMiss = cMiss});
        }

        /// <summary>
        ///     获取该成绩对应的谱面
        /// </summary>
        /// <returns></returns>
        public OnlineBeatmap GetOnlineBeatmap()
        {
            var q = new OnlineBeatmapQuery();
            q.OsuApiKey = QuerierApiKey;
            q.BeatmapID = beatmap_id;
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
            q.UserID = user_id;
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