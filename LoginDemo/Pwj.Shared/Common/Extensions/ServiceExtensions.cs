using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common
{
    /// <summary>
    /// 功能描述    ：ContainerBuilder的拓展  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/1/29 15:07:44 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/1/29 15:07:44 
    /// </summary>
    public static class ServiceExtensions
    {

        public static ContainerBuilder AddViewModel<TRepository, IRepository>(this ContainerBuilder services) where TRepository : class, IRepository
        {
            services.RegisterType<TRepository>().As<IRepository>();
            return services;
        }

        /// <summary>
        /// 单接口多实现的时候
        /// </summary>
        /// <typeparam name="TCenter"></typeparam>
        /// <typeparam name="ICenter"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ContainerBuilder AddViewCenter<TCenter, ICenter>(this ContainerBuilder services) where TCenter : class, ICenter
        {
            services.RegisterType<TCenter>().Named(typeof(TCenter).Name, typeof(ICenter));
            return services;
        }

        public static ContainerBuilder AddViewService<TRepository, IRepository>(this ContainerBuilder services) where TRepository : class, IRepository
        {
            services.RegisterType<TRepository>().As<IRepository>();
       
            return services;
        }

    }
}
