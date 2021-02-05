using Castle.DynamicProxy;
using Pwj.Shared.Common.Aop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common
{
    /// <summary>
    /// 功能描述    ：ContainerAopExtend  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 17:40:09 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 17:40:09 
    /// </summary>
    public static class ContainerAopExtend
    {
        public static object AOP(this object t, Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            StandardInterceptor interceptor = new LogInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }


        /// <summary>
        /// 拓展实现AOP代理
        /// </summary>
        /// <param name="t"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static object BaseAop(this object t, Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            BaseInterceptor interceptor = new BaseInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }

        public static object BaseAop(this object t)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推
            MethodBase method = frame.GetMethod();
            Type type = method.ReflectedType;
            ProxyGenerator generator = new ProxyGenerator();
            BaseInterceptor interceptor = new BaseInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(type, t, interceptor);
            return t;
        }
    }
}
