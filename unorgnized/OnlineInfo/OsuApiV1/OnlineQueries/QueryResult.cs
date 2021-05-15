using Newtonsoft.Json.Linq;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        ///     在线查询的结果
        /// </summary>
        public class QueryResult
        {
            /// <summary>
            ///     使用JArray初始化一个QueryResult
            /// </summary>
            /// <param name="jarr"></param>
            public QueryResult(JArray jarr)
            {
                Results = jarr;
            }

            /// <summary>
            ///     创造一个空的QueryResult对象
            /// </summary>
            public QueryResult()
            {
            }

            /// <summary>
            ///     从http请求获取到的结果
            /// </summary>
            public JArray Results { get; }
        }
    }
}