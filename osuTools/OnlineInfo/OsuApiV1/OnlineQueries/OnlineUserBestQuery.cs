using System;
using System.Text;

namespace osuTools.Online.ApiV1.Querier
{
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