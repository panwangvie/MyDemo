using Castle.DynamicProxy;
using Pwj.Shared.Common.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
