using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.HttpContact
{
    /// <summary>
    /// 功能描述    ：请求产生构造基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/5 11:32:42 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/5 11:32:42 
    /// </summary>
    public class BaseRequest<T>
    {
        /// <summary>
        /// 路由地址
        /// </summary>
  
        public virtual string route { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public T Parameter { get; set; }
    }

    public class BaseRequest
    {
        /// <summary>
        /// 路由地址
        /// </summary>

        public virtual string route { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public Object Parameter { get; set; }
    }
}
