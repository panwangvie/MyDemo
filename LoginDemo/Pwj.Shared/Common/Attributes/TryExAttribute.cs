using Castle.DynamicProxy;
using Pwj.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common.Attributes
{
    /// <summary>
    /// 功能描述    ：处理异常的拓展  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/2 10:00:33 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/2 10:00:33 
    /// </summary>
    public class TryExAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    var inStr = invocation.Arguments;
                    NetCoreProvider.Resolve<IFLog>().Error($"调用方法{invocation.Method.Name}",ex);
                }
            };
        }
    }
}
