using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.ExtraMethods;

namespace osuTools
{
    namespace Online.ApiV2.Authorization
    {
        /// <summary>
        ///     用于调用OsuApiV2的Token
        /// </summary>
        public class OsuApiV2Token
        {
            /// <summary>
            ///     使用Json填充一个OsuApiV2Token对象
            /// </summary>
            /// <param name="json"></param>
            public OsuApiV2Token(JObject json)
            {
                var sec = 0;
                int.TryParse(json["expires_in"].ToString(), out sec);
                TokenType = json["token_type"].ToString();
                if (sec == 0)
                    throw new ArgumentException();
                ExpiresIn = TimeSpan.FromSeconds(sec);
                AccessToken = json["access_token"].ToString();
            }

            /// <summary>
            ///     Token的类型，应为
            /// </summary>
            public string TokenType { get; }

            /// <summary>
            ///     剩余有效时间
            /// </summary>
            public TimeSpan ExpiresIn { get; }

            /// <summary>
            ///     Token
            /// </summary>
            public string AccessToken { get; }
        }

        /// <summary>
        ///     通过指定的私钥和AppID向OsuApiV2请求Token的类
        /// </summary>
        public class OsuApiV2Authorization
        {
            /// <summary>
            ///     使用正确的私钥和AppID创建一个OsuApiV2Authorization对象
            /// </summary>
            /// <param name="secret"></param>
            /// <param name="appId"></param>
            public OsuApiV2Authorization(string secret, int appId)
            {
                SecretKey = secret;
                AppID = appId;
            }

            /// <summary>
            ///     私钥
            /// </summary>
            public string SecretKey { get; set; }

            /// <summary>
            ///     AppID
            /// </summary>
            public int AppID { get; set; }

            /// <summary>
            ///     可访问的领域
            /// </summary>
            public string AccessScope { get; set; } = "identify public";

            /// <summary>
            ///     请求方法
            /// </summary>
            public string RequestMethod { get; set; } = "post";

            /// <summary>
            ///     http请求
            /// </summary>
            public HttpWebRequest Request { get; } = WebRequest.CreateHttp("https://osu.ppy.sh/oauth/token");

            /// <summary>
            ///     通过填写的信息获取Token
            /// </summary>
            /// <returns>一个<see cref="OsuApiV2Token" /></returns>
            public OsuApiV2Token GetToken()
            {
                var recvjson = "";
                if (string.IsNullOrEmpty(SecretKey) || AppID == 0)
                    throw new ArgumentNullException();
                Request.Accept = "application/json";
                Request.ContentType = "application/json";
                Request.Method = "post";
                var json =
                    $"{{\"grant_type\":\"client_credentials\",\"client_id\":\"{AppID}\",\"client_secret\":\"{SecretKey}\",\"scope\":\"{AccessScope}\"}}";
                using (var stream = Request.GetRequestStream())
                {
                    var bytes = json.ToBytes(Encoding.ASCII);
                    stream.Write(bytes, 0, bytes.Length);
                }

                using (var response = Request.GetResponse())
                {
                    var r = new StreamReader(response.GetResponseStream());
                    recvjson = r.ReadToEnd();
                }

                return new OsuApiV2Token((JObject) JsonConvert.DeserializeObject(recvjson));
            }
        }
    }
}