using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.OnlineInfo.OsuApiV2.Online.ApiV2.Authorization;

namespace osuTools.OnlineInfo.OsuApiV2
{
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
            string recvjson;
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
                var r = new StreamReader(response.GetResponseStream() ?? throw new WebException());
                recvjson = r.ReadToEnd();
            }

            return new OsuApiV2Token((JObject) JsonConvert.DeserializeObject(recvjson));
        }
    }
}