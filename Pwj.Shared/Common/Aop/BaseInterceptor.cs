using Castle.DynamicProxy;
using Pwj.Shared.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common.Aop
{
    /// <summary>
    /// 功能描述    ：拓展Castel的AOP，使用特性进行注入  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/1 14:52:35 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/1 14:52:35 
    /// </summary>
    public class BaseInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            var method = invocation.Method;
            Action action = () => base.PerformProceed(invocation); //

            //顺序控制
            if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
            {
                var date = method.GetCustomAttributesData();

                var XX = method.GetCustomAttributes(typeof(BaseInterceptorAttribute), true);
                //想注入 就容器实例---
                foreach (var attribute in method.GetCustomAttributes(typeof(BaseInterceptorAttribute),true).ToArray().Reverse())
                {
                    action = (attribute as BaseInterceptorAttribute).Do(invocation, action);
                }
            }
            //那就说明前面不能执行具体动作--前面只能是组装动作---配置管道模型---委托
            action.Invoke();
            //1 能解决哪些方法不用AOP的问题
            //2 可以把切面逻辑转移到特性里面去
        }

        /// <summary>
        /// 调用方法后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
        }

        /// <summary>
        /// 调用方法前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {

        }
    }
}
