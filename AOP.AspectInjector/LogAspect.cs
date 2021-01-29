using AspectInjector.Broker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOP.AspectInjector
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LogAspect))]
    public class LogAspect:Attribute
    {
        
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="arguments">参数</param>
        [Advice(Kind.Before)]
        public void LogBefore([Argument(Source.Name)] string name,[Argument(Source.Arguments)] object[] arguments )
        {
            Console.WriteLine("");
            Console.WriteLine($"Before，调用方法：'{name}'，调用参数：{JsonConvert.SerializeObject(arguments)}");
        }
        [Advice(Kind.After)]
        public void LogBefore([Argument(Source.Name)] string name,[Argument(Source.Arguments)] object[] arguments,[Argument(Source.ReturnValue)] object returnValue)
        {
            Console.WriteLine("");
            Console.WriteLine($"After，调用方法：'{name}'，调用参数：{JsonConvert.SerializeObject(arguments)}，返回值为：{JsonConvert.SerializeObject(returnValue)}");
        }

    }
}
