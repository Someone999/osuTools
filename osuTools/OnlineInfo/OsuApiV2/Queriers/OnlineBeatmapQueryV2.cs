using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.OnlineInfo.OsuApiV2.Online.ApiV2.Authorization;
using osuTools.OnlineInfo.OsuApiV2.ResultClasses;

namespace osuTools.OnlineInfo.OsuApiV2.Queriers
{
    /// <summary>
    ///     用于向OsuApiV2查询谱面
    /// </summary>
    public class OnlineBeatmapQueryV2
    {
        /// <summary>
        ///     <see cref="OsuApiV2Token" />，可以通过<see cref="OsuApiV2Authorization" />来获取
        /// </summary>
        public OsuApiV2Token Token { get; set; }

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId { get; set; }
        /// <summary>
        /// 使用存储的BeatmapId获取谱面信息
        /// </summary>
        /// <returns></returns>
        public OnlineBeatmapSetV2 GetResult()
        {
            if (Token == null)
                throw new InvalidOperationException(
                    "必须指定一个Token。Token可以从Online.ApiV2.Authorization.OsuApiV2Authorization获取。");
            var uri = $"https://osu.ppy.sh/api/v2/beatmaps/{BeatmapId}";
            var request = WebRequest.CreateHttp(uri);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {Token.AccessToken}");
            if (request is null)
                throw new NullReferenceException();
            var r = new StreamReader(request.GetResponse().GetResponseStream() ?? new MemoryStream());
            var recvjson = r.ReadToEnd();
            var jobj = (JObject) JsonConvert.DeserializeObject(recvjson);
            return new OnlineBeatmapSetV2(jobj);

        }
    }
}