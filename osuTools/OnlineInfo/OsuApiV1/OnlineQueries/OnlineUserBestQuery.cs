using System;
using System.Text;
using osuTools.Game.Modes;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     在线查询玩家的最佳记录
    /// </summary>
    public class OnlineUserBestQuery
    {
        private int _lim = 10;
        private bool _queried;
        private OnlineBestRecordCollection _res = new OnlineBestRecordCollection();

        /// <summary>
        ///     osuApi的密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserId { get; set; }

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
            get => _lim;
            set
            {
                if (OnlineQueryTools.InRange(0, 50, value))
                    _lim = value;
            }
        }

        /// <summary>
        ///     查询结果
        /// </summary>
        public OnlineBestRecordCollection Results
        {
            get
            {
                if (!_queried)
                {
                    GetResult();
                    _queried = true;
                }

                return _res;
            }
            private set => _res = value;
        }

        private void GetResult()
        {
            if (UserId == 0)
                if (string.IsNullOrEmpty(UserName))
                    throw new ArgumentException("必须指定用户名或用户ID。");
            var basestr = $"https://osu.ppy.sh/api/get_user_best?k={OsuApiKey}";
            var b = new StringBuilder(basestr);
            b.Append(UserId != 0 ? $"&u={UserId}&type=id&m={(int) Mode}" : $"&u={UserName}&type=string&m={(int) Mode}");
            var q = OnlineQueryTools.GetResponse(new Uri(b.ToString()));
            if (q.Results.Count == 0) _res.Failed = true;
            foreach (var json in q.Results) _res.Records.Add(new OnlineBestRecord(json.ToString(), Mode));
        }
    }
}