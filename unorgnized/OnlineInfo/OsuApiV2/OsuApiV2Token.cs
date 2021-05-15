using System;
using Newtonsoft.Json.Linq;

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
                int.TryParse(json["expires_in"].ToString(), out var sec);
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
    }
}