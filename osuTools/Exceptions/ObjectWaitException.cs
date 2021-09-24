using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.Exceptions
{
    /// <summary>
    /// 对象等待失败时抛出的异常
    /// </summary>
    public class ObjectWaitException:Exception
    {
        /// <summary>
        /// 用指定的错误消息初始化ObjectWaitException的新实例
        /// </summary>
        /// <param name="msg">消息</param>
        public ObjectWaitException(string msg):base(msg)
        {
        }
        /// <summary>
        /// 用指定的错误消息和为此异常原因的内部异常初始化ObjectWaitException的新实例
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="innerException">内部异常</param>
        public ObjectWaitException(string msg,Exception innerException):base(msg,innerException)
        {
        }
    }
}
