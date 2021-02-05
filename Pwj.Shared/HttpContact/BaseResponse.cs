using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.HttpContact
{
    /// <summary>
    /// 功能描述    ：BaseResponse  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/4 17:25:37 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/4 17:25:37 
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// //返回状态
        /// </summary>
        public int StatusCode { get; set; }

        public object Result { get; set; }
    }

    public class BaseResponse<T>
    {
        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// //返回状态
        /// </summary>
        public int StatusCode { get; set; }

        public T Result { get; set; }
    }
}
