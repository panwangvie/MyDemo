using Castle.DynamicProxy;
using Pwj.Shared.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common.Aop
{
    /// <summary>
    /// 功能描述    ：日志AOP 
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 17:49:46 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 17:49:46 
    /// </summary>
    public class LogInterceptor: StandardInterceptor
    {

        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            NetCoreProvider.Resolve<IFLog>().Info($"调用{invocation.Method.Name}前拦截器");
            base.PreProceed(invocation);
          
        }

        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            NetCoreProvider.Resolve<IFLog>().Info($"调用{invocation.Method.Name}返回时调用的拦截器");
            base.PerformProceed(invocation);
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            NetCoreProvider.Resolve<IFLog>().Info($"调用{invocation.Method.Name}后拦截器");
            base.PostProceed(invocation);
        }
    }
}
