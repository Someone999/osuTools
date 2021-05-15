using System;
using System.Text;
using osuTools.Game.Modes;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    /// <summary>
    ///     在线查询用户信息
    /// </summary>
    public class OnlineUserQuery
    {
        private bool _queried;
        private OnlineUser _rec = new OnlineUser();

        /// <summary>
        ///     查询到的用户信息
        /// </summary>
        public OnlineUser UserInfo
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
        ///     用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     要查询的模式
        /// </summary>
        public OsuGameMode Mode { get; set; } = OsuGameMode.Osu;

        /// <summary>
        ///     osuApi密钥
        /// </summary>
        public string OsuApiKey { get; set; }

        /// <summary>
        ///     距离上次时间的最大天数
        /// </summary>
        public int MaxDaysLastEventBefore { get; set; } = 1;

        /// <summary>
        ///     生成查询的Uri
        /// </summary>
        /// <returns></returns>
        public Uri UriGenerator()
        {
            var baseuri = $"https://osu.ppy.sh/api/get_user?k={OsuApiKey}";
            if (UserId == 0 && string.IsNullOrEmpty(UserName))
                throw new ArgumentException();
            string usern = $"&u={UserName}&type=string",
                userid = $"&u={UserId}&type=id",
                mode = $"&m={(int) Mode}",
                eventdays = $"&event_days={MaxDaysLastEventBefore}";
            var b = new StringBuilder(baseuri);
            b.Append(UserId == 0 ? usern : userid);
            b.Append(mode);
            b.Append(eventdays);
            return new Uri(b.ToString());
        }

        private void GetResult()
        {
            UserInfo = new OnlineUser(OnlineQueryTools.GetResponse(UriGenerator()).Results,Mode);
        }
    }
}