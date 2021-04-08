using System;
using System.Text;
using System.Windows.Forms;
using osuTools.Game.Mods;

namespace osuTools.Online.ApiV1.Querier
{
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
}