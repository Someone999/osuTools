using System;
using System.Text;

namespace osuTools.Online.ApiV1.Querier
{
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
}