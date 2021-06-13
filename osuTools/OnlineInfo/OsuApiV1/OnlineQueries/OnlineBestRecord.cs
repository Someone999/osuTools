using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.Beatmaps;
using osuTools.Beatmaps.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.Game.Mods;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     最高PP榜中的记录
    /// </summary>
    [Serializable]
    public partial class OnlineBestRecord : PpSorted, IFormattable
    {
        private BeatmapCollection _bc;

        private int
            _beatmapId,
            _scoreId;

        private DateTime _d;

        private string
            _date;

        private int _mods;

        private double _pp;

        private int
            _score,
            _maxcombo,
            _count50,
            _count100,
            _count300,
            _countmiss,
            _countkatu,
            _countgeki,
            _perfect,
            _userId;

        /// <summary>
        ///     初始化一个新的OnlineBestRecord实例
        /// </summary>
        public OnlineBestRecord()
        {
            Perfect = false;
            _d = DateTime.MinValue;
            _beatmapId = 0;
            _scoreId = 0;
            _score = 0;
            _pp = 0.0;
            WeightedPP = 0.0;
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
            Mods = HitObjectTools.GetGenericTypesByInt<OsuGameMod>(_mods);
            Rank = "?";
        }

        /// <summary>
        ///     使用json填充一个OnlineBestRecord对象,并指定模式
        /// </summary>
        /// <param name="json"></param>
        /// <param name="mode"></param>
        public OnlineBestRecord(string json, OsuGameMode mode)
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
            int.TryParse(jobj["enabled_mods"].ToString(), out _mods);
            int.TryParse(jobj["beatmap_id"].ToString(), out _beatmapId);
            int.TryParse(jobj["score_id"].ToString(), out _scoreId);
            double.TryParse(jobj["pp"].ToString(), out _pp);
            _date = jobj["date"].ToString();
            Rank = jobj["rank"].ToString();
            Mods = HitObjectTools.GetGenericTypesByInt<OsuGameMod>(_mods);
            Accuracy = AccCalc(mode);
            DateTime.TryParse(_date, out _d);
            if (_perfect == 1)
                Perfect = true;
            else if (_perfect == 0) Perfect = false;
        }

        /// <summary>
        ///     使用了的Mod
        /// </summary>
        public List<OsuGameMod> Mods { get; }

        /// <summary>
        ///     准度
        /// </summary>
        public double Accuracy { get; private set; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; private set; }

        /// <summary>
        ///     使用指定的格式构建字符串
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var b = new StringBuilder(format);
            b.Replace("perfect", Perfect.ToString());
            b.Replace("pp", Pp.ToString(CultureInfo.CurrentCulture));
            b.Replace("Count300g", Count300g.ToString());
            b.Replace("c300", Count300.ToString());
            b.Replace("Count200", Count200.ToString());
            b.Replace("Count100", Count100.ToString());
            b.Replace("Count50", Count50.ToString());
            b.Replace("CountMiss", CountMiss.ToString());
            b.Replace("userid", UserId.ToString());
            b.Replace("rank", Rank);
            b.Replace("playtime", _d.ToString("yyyy/MM/dd HH:mm:ss"));
            b.Replace("score", Score.ToString());
            b.Replace("beatmapid", BeatmapId.ToString());
            b.Replace("maxcombo", MaxCombo.ToString());
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
            var osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
            q.OsuApiKey = osuApiKey;
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
            var user = new OnlineUser();
            return user;
        }

        private double AccCalc(OsuGameMode mode)
        {
            double c3G = Count300g, c3 = Count300, c2 = Count200, c1 = Count100, c5 = Count50, cm = CountMiss;
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
        ///     使用指定的格式构建字符串
        /// </summary>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        internal void CalcWeight(int index)
        {
            WeightedPP = _pp * Math.Pow(0.95, index);
        }

        /// <summary>
        ///     使用osu!api获得相应谱面的信息并转换成Beatmap
        /// </summary>
        /// <returns>返回一个<seealso cref="Beatmap" />对象</returns>
        public Beatmap GetBeatmap()
        {
            var query = new OnlineBeatmapQuery();
            var osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
            query.BeatmapId = _beatmapId;
            query.OsuApiKey = osuApiKey;
            var bms = query.Beatmaps;
            var b = new Beatmap(bms[0]);
            if (b.BeatmapId == -2048)
                b = null;
            return b;
        }

        /*
            /// <summary>
            /// 使用osu!api获得相应谱面的信息
            /// </summary>
            /// <returns>返回一个<seealso cref="OnlineBeatmap"/>对象</returns>
            public OnlineBeatmap GetOnlineBeatmap()
            {
                OnlineBeatmapCollection bms = new OnlineBeatmapCollection();
                OsuApiQuery q = new OsuApiQuery();
                string osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
                q.QueryType = OsuApiQueryType.Beatmaps;
                q.ApiKey = osuApiKey;
                q.BeatmapId = (int)beatmap_id;
                bms.AllParse(q);
                return bms[0];
            }*/
        /// <summary>
        ///     在本地的谱面集合中寻找对应谱面
        /// </summary>
        /// <param name="bc">谱面集合</param>
        /// <returns>查找到的谱面</returns>
        public Beatmap FindInBeatmapCollection(BeatmapCollection bc)
        {
            _bc = bc ?? throw new NullReferenceException();
            return bc.Find(_beatmapId);
        }

        /// <summary>
        ///     将查询到的数据转换成一定格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                if (_bc == null)
                    return
                        $"{_beatmapId}\nScore:{Score} PP:{Pp}\nc300g:{Count300g} c300:{Count300} Count200:{Count200} Count100:{Count100} Count50:{Count50} CountMiss:{CountMiss} MaxCombo:{MaxCombo}\nPerfect:{Perfect}";

                var v = FindInBeatmapCollection(_bc);
                if (v == null) throw new NullReferenceException();
                return
                    $"{v}\nScore:{Score} PP:{Pp}\nc300g:{Count300g} c300:{Count300} Count200:{Count200} Count100:{Count100} Count50:{Count50} CountMiss:{CountMiss} MaxCombo:{MaxCombo}\nPerfect:{Perfect}";
            }
            catch
            {
                return "操作失败";
            }
        }
    }
}