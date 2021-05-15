using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace osuTools.Online.ApiV1
{
    /// <summary>
    ///     在线查询的通用工具
    /// </summary>
    public static class OnlineQueryTools
    {
        /// <summary>
        ///     判断值是否在范围内，不包括最大值和最小值
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="value"></param>
        /// <param name="includeEdge"></param>
        /// <returns></returns>
        public static bool InRange(double max, double min, double value, bool includeEdge = true)
        {
            return !includeEdge ? value > min && value < max : value >= min && value <= max;
        }

        /// <summary>
        ///     使用HttpClient向指定的Uri发送请求
        /// </summary>
        /// <param name="target"></param>
        /// <returns>类型为<see cref="QueryResult" />的查询结果</returns>
        public static QueryResult GetResponse(Uri target)
        {
            var client = new HttpClient();
            client.BaseAddress = target;
            var rslt = client.GetStringAsync(target).Result;
            var obj = JsonConvert.DeserializeObject(rslt);
            QueryResult queryResult;
            if (obj.GetType() == typeof(JArray))
            {
                queryResult = new QueryResult((JArray) obj);
                return queryResult;
            }

            if (obj.GetType() == typeof(JObject))
            {
                queryResult = new QueryResult();
                queryResult.Results.Add(obj);
                return queryResult;
            }

            queryResult = new QueryResult();
            return queryResult;
        }
    }
}