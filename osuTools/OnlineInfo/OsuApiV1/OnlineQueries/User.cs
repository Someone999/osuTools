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
                count_rank_ss,
                count_rank_ssh,
                count_rank_s,
                count_rank_sh,
                count_rank_a;

            private string join_date = "0-0-0 0:0:0";

            private double
                level,
                pp_raw,
                accuracy;

            private OsuGameMode mode;
            private int playcount;
            private int pp_rank;
            private double ranked_score, total_score;
            private DateTime t;

            private int
                total_seconds_played,
                pp_country_rank;

            private int user_id;

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
                    int.TryParse(jobj["user_id"].ToString(), out user_id);
                    int.TryParse(jobj["playcount"].ToString(), out playcount);
                    int.TryParse(jobj["pp_rank"].ToString(), out pp_rank);
                    int.TryParse(jobj["count_rank_ss"].ToString(), out count_rank_ss);
                    int.TryParse(jobj["count_rank_ssh"].ToString(), out count_rank_ssh);
                    int.TryParse(jobj["count_rank_s"].ToString(), out count_rank_s);
                    int.TryParse(jobj["count_rank_sh"].ToString(), out count_rank_sh);
                    int.TryParse(jobj["count_rank_a"].ToString(), out count_rank_a);
                    int.TryParse(jobj["total_seconds_played"].ToString(), out total_seconds_played);
                    int.TryParse(jobj["pp_country_rank"].ToString(), out pp_country_rank);
                    double.TryParse(jobj["ranked_score"].ToString(), out ranked_score);
                    double.TryParse(jobj["total_score"].ToString(), out total_score);
                    double.TryParse(jobj["pp_raw"].ToString(), out pp_raw);
                    double.TryParse(jobj["level"].ToString(), out level);
                    double.TryParse(jobj["accuracy"].ToString(), out accuracy);
                    UserName = jobj["username"].ToString();
                    join_date = jobj["join_date"].ToString();
                    Country = jobj["country"].ToString();
                    DateTime.TryParse(join_date, out t);
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
                b.Replace("userid", UserID.ToString());
                b.Replace("mode", mode.ToString());
                b.Replace("pp", PP.ToString());
                b.Replace("globalrank", GlobalRank.ToString());
                b.Replace("countryrank", CountryRank.ToString());
                b.Replace("cssh", SSHCount.ToString());
                b.Replace("csh", SHCount.ToString());
                b.Replace("css", SSCount.ToString());
                b.Replace("cs", SCount.ToString());
                b.Replace("ca", ACount.ToString());
                b.Replace("acc", $"{Accuracy:f2}%");
                b.Replace("rankedscore", RankedScore.ToString());
                b.Replace("totalscore", TotalScore.ToString());
                b.Replace("playcount", PlayCount.ToString());
                b.Replace("level", Level.ToString());
                b.Replace("countrycn", GetCountryInCN(Country));
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
                var j = new DateTime(DateTime.Now.Ticks - t.Ticks - DateTime.MinValue.Ticks);
                temp =
                    $"UserName:{UserName}(ID:{UserID})\nMode:{mode.ToString()} pp:{PP}\nGlobal Rank:{GlobalRank} Country Rank:{CountryRank}\n" +
                    $"Silver SS:{SSHCount} SS:{SSCount} Silver S:{SHCount} S:{SCount} A:{ACount}\n" +
                    $"Accuracy:{accuracy.ToString("f2")}% Total Score:{TotalScore} Ranked Score:{ranked_score}\n" +
                    $"Play Count{PlayCount}({$"{PlayTime.Days}d {PlayTime.Hours}:{PlayTime.Minutes}:{PlayTime.Seconds})"} Level:{level}\n" +
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

            private string GetCountryInCN(string countryId)
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