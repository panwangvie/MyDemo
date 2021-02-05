using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common.Attributes
{
    /// <summary>
    /// 功能描述    ：拓展casetle AOP的特性基类  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/1 14:54:54 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/1 14:54:54 
    /// </summary>
  #region attribute Interceptor
    public abstract class BaseInterceptorAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation, Action action);
    }

    public class LogBeforeAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogBeforeAttribute1 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //去执行真实逻辑
                action.Invoke();
                //写个日志---参数检查--能做的事儿已经很多了
                Console.WriteLine($"This is LogBeforeAttribute2 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogAfterAttribute1  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //去执行真实逻辑
                action.Invoke();
                Console.WriteLine($"This is LogAfterAttribute2  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class MonitorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                Console.WriteLine("This is MonitorAttribute 1");
                stopwatch.Start();

                //去执行真实逻辑
                action.Invoke();
                //想做个缓存拦截，觉得自己能搞定刷个1

                stopwatch.Stop();
                Console.WriteLine($"本次方法花费时间{stopwatch.ElapsedMilliseconds}ms");
                Console.WriteLine("This is MonitorAttribute 2");
            };
        }
    }

    public class LoginAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine("This is LoginAttribute 1");
                action.Invoke();
                Console.WriteLine("This is LoginAttribute 2");
            };
        }
    }
    #endregion
}
