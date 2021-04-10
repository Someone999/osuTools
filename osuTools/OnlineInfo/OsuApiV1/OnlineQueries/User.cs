using System;
using System.Text;
using Newtonsoft.Json.Linq;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        ///     在线获取用户的信息。
        /// </summary>
        [Serializable]
        public partial class OnlineUser : IFormattable
        {
            private int
                _countRankSs,
                _countRankSsh,
                _countRankS,
                _countRankSh,
                _countRankA;

            private string _joinDate = "0-0-0 0:0:0";

            private double
                _level,
                _ppRaw,
                _accuracy;

            private OsuGameMode _mode;
            private int _playcount;
            private int _ppRank;
            private double _rankedScore, _totalScore;
            private DateTime _t;

            private int
                _totalSecondsPlayed,
                _ppCountryRank;

            private int _userId;

            /// <summary>
            ///     实例化一个OnlineUser对象，内容为空
            /// </summary>
            public OnlineUser()
            {
            }

            /// <summary>
            ///     使用一个Json填充一个OnlineUser对象
            /// </summary>
            /// <param name="jarr">json</param>
            public OnlineUser(JArray jarr)
            {
                if (jarr.Count == 0)
                {
                    Failed = true;
                    return;
                }

                try
                {
                    var jobj = jarr[0];
                    int.TryParse(jobj["user_id"].ToString(), out _userId);
                    int.TryParse(jobj["playcount"].ToString(), out _playcount);
                    int.TryParse(jobj["pp_rank"].ToString(), out _ppRank);
                    int.TryParse(jobj["count_rank_ss"].ToString(), out _countRankSs);
                    int.TryParse(jobj["count_rank_ssh"].ToString(), out _countRankSsh);
                    int.TryParse(jobj["count_rank_s"].ToString(), out _countRankS);
                    int.TryParse(jobj["count_rank_sh"].ToString(), out _countRankSh);
                    int.TryParse(jobj["count_rank_a"].ToString(), out _countRankA);
                    int.TryParse(jobj["total_seconds_played"].ToString(), out _totalSecondsPlayed);
                    int.TryParse(jobj["pp_country_rank"].ToString(), out _ppCountryRank);
                    double.TryParse(jobj["ranked_score"].ToString(), out _rankedScore);
                    double.TryParse(jobj["total_score"].ToString(), out _totalScore);
                    double.TryParse(jobj["pp_raw"].ToString(), out _ppRaw);
                    double.TryParse(jobj["level"].ToString(), out _level);
                    double.TryParse(jobj["accuracy"].ToString(), out _accuracy);
                    UserName = jobj["username"].ToString();
                    _joinDate = jobj["join_date"].ToString();
                    Country = jobj["country"].ToString();
                    DateTime.TryParse(_joinDate, out _t);
                }
                catch (NullReferenceException)
                {
                    Failed = true;
                }
            }

            /// <summary>
            ///     指示本次查询是否成功
            /// </summary>
            public bool Failed { get; private set; }

            /// <summary>
            ///     使用一定的格式构造一个字符串
            /// </summary>
            /// <param name="format"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                var b = new StringBuilder(format);
                b.Replace("username", UserName);
                b.Replace("userid", UserId.ToString());
                b.Replace("mode", _mode.ToString());
                b.Replace("pp", Pp.ToString());
                b.Replace("globalrank", GlobalRank.ToString());
                b.Replace("countryrank", CountryRank.ToString());
                b.Replace("cssh", SshCount.ToString());
                b.Replace("csh", ShCount.ToString());
                b.Replace("css", SsCount.ToString());
                b.Replace("cs", SCount.ToString());
                b.Replace("ca", ACount.ToString());
                b.Replace("acc", $"{Accuracy:f2}%");
                b.Replace("rankedscore", RankedScore.ToString());
                b.Replace("totalscore", TotalScore.ToString());
                b.Replace("playcount", PlayCount.ToString());
                b.Replace("level", Level.ToString());
                b.Replace("countrycn", GetCountryInCn(Country));
                b.Replace("country", Country);
                b.Replace("joindate", JoinDate.ToString("yyyy/MM/dd HH:mm:ss"));
                return b.ToString();
            }

            /// <summary>
            ///     返回一个包含所有信息的字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string temp;
                var j = new DateTime(DateTime.Now.Ticks - _t.Ticks - DateTime.MinValue.Ticks);
                temp =
                    $"UserName:{UserName}(ID:{UserId})\nMode:{_mode.ToString()} pp:{Pp}\nGlobal Rank:{GlobalRank} Country Rank:{CountryRank}\n" +
                    $"Silver SS:{SshCount} SS:{SsCount} Silver S:{ShCount} S:{SCount} A:{ACount}\n" +
                    $"Accuracy:{_accuracy.ToString("f2")}% Total Score:{TotalScore} Ranked Score:{_rankedScore}\n" +
                    $"Play Count{PlayCount}({$"{PlayTime.Days}d {PlayTime.Hours}:{PlayTime.Minutes}:{PlayTime.Seconds})"} Level:{_level}\n" +
                    $"From {Country}\nJoin at {JoinDate.ToString()}(Joined for {$"{j.Year}y{j.Month}mon{j.Day}d {j.Hour}h{j.Minute}m{j.Second}s"})\n";
                return temp;
            }

            /// <summary>
            ///     使用一定的格式构造一个字符串
            /// </summary>
            /// <param name="format"></param>
            /// <returns></returns>
            public string ToString(string format)
            {
                return ToString(format, null);
            }

            private string GetCountryInCn(string countryId)
            {
                if (countryId == "CN") return "中国";
                if (countryId == "HK") return "香港";
                if (countryId == "TW") return "台湾";
                if (countryId == "KR") return "韩国";
                if (countryId == "JP") return "日本";
                if (countryId == "GB") return "英国";
                if (countryId == "DE") return "德国";
                if (countryId == "US") return "美国";
                if (countryId == "VN") return "越南";
                if (countryId == "MY") return "马来西亚";
                if (countryId == "CL") return "智利";
                if (countryId == "SG") return "新加坡";
                if (countryId == "SE") return "瑞典";
                if (countryId == "RU") return "俄罗斯";
                if (countryId == "PE") return "秘鲁";
                if (countryId == "NZ") return "新西兰";
                if (countryId == "PT") return "葡萄牙";
                if (countryId == "PH") return "菲律宾";
                if (countryId == "PK") return "巴基斯坦";
                if (countryId == "PL") return "波兰";
                if (countryId == "PS") return "巴勒斯坦";
                if (countryId == "ZA") return "南非";
                if (countryId == "CA") return "加拿大";
                if (countryId == "AU") return "澳大利亚";
                return countryId;
            }
        }
    }
}