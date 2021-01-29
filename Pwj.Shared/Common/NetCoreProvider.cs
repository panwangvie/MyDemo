using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common
{
    /// <summary>
    /// 功能描述    ：服务提供者 
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:05:43 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:05:43 

    public class NetCoreProvider
    {
        public static IContainer Instance { get; private set; }

        public static void RegisterServiceLocator(IContainer locator)
        {
            if (Instance == null)
                Instance = locator;
        }

        public static T Resolve<T>()
        {
            if (!Instance.IsRegistered<T>())
                new ArgumentNullException(nameof(T));

            return Instance.Resolve<T>();
        }

        public static T ResolveNamed<T>(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                new ArgumentNullException(nameof(T));

            return Instance.ResolveNamed<T>(typeName);
        }

        public static T ResolveAop<T>()
        {
            return (T)Resolve<T>().AOP(typeof(T));
            
        }

        public static T ResolveNamedAop<T>(string typeName)
        {
            

            return (T)ResolveNamed<T>(typeName).AOP(typeof(T));
        }
    }
}
