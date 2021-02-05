using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pwj.Shared.Common
{
    /// <summary>
    /// 功能描述    ：TestIOC  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/2/1 14:14:35 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/2/1 14:14:35 
    /// </summary>
    public class TestIOC
    {
        #if regionIOCTestIOC
        public static void TestIOC()
        {
            Console.WriteLine($"测试---IOC");
            ContainerBuilder builder = new ContainerBuilder();

            #region Autofac默认都是构造函数注入
            //Autofac默认都是构造函数注入
            //builder.RegisterType<TestA>().As<ITestA>().InstancePerDependency();//瞬态
            //builder.RegisterType<TestB>().As<ITestB>().SingleInstance();//单例
            //builder.RegisterType<TestC>().As<ITestC>().InstancePerLifetimeScope();//作用域，应用域
            //builder.RegisterType<TestD>().As<ITestD>().InstancePerMatchingLifetimeScope("TEST");指定作用域，指定应用域

           // Autofac接口服务使用属性注入----PropertiesAutowired属性注入----接口中的实现类中的其他接口服务的属性注入
            //builder.RegisterType<TestE>().As<ITestE>().InstancePerMatchingLifetimeScope("TEST123").PropertiesAutowired();//指定作用域，指定应用域
            #endregion

            #region Autofac Controller控制器中接口服务使用属性注入----PropertiesAutowired属性注入----接口中的实现类中的其他接口服务的属性注入
            //Autofac Controller控制器中接口服务使用属性注入----PropertiesAutowired属性注入----接口中的实现类中的其他接口服务的属性注入
            containerBuilder.RegisterType<HHController>().As<ControllerBase>().InstancePerMatchingLifetimeScope("TEST123").PropertiesAutowired();//指定作用域，指定应用域
                                                                                                                                                 //public void ConfigureServices(IServiceCollection services)中添加如下
                                                                                                                                                 //控制器属性注入，默认ioc容器之创建接口服务，控制器的创建是由IControllerActivator创建的，现在使用ioc容器创建ServiceBasedControllerActivator
                                                                                                                                                 //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //public void ConfigureContainer(ContainerBuilder containerBuilder)中添加如下
            //var types = this.GetType().Assembly.ExportedTypes.Where(t => typeof(ControllerBase).IsAssignableFrom(t)).ToArray();
           // 注册所有controller,PropertiesAutowired 属性注入所有的接口服务以及自定义特性CustomPropAttribute区分标记和自定义属性选择器MyPropertySelector
            //containerBuilder.RegisterTypes(types).PropertiesAutowired(new MyPropertySelector()); 
            #endregion

            #region 使用方法注入----接口中的实现类中的其他接口服务的属性注入
           // 使用方法注入----接口中的实现类中的其他接口服务的属性注入
            //builder.RegisterType<TestG>().OnActivated(t => t.Instance.MethodInject(t.Context.Resolve<ITestB>())).As<ITestG>().InstancePerMatchingLifetimeScope("TEST456").PropertiesAutowired();//指定作用域，指定应用域
            #endregion


            #region Autofac 配置文件 配置IOC 依赖注入 属性注入
            //Autofac 配置文件 配置IOC 依赖注入 属性注入
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.Add(new JsonConfigurationSource() { Path = "Config/autofacconfig.json", Optional = false, ReloadOnChange = true });
            var conmodule = new ConfigurationModule(configurationBuilder.Build());
            builder.RegisterModule(conmodule);
            #endregion

            var contaier = builder.Build();
            var testA = contaier.Resolve<ITestA>();
            testA.Show();
            var testA1 = contaier.Resolve<ITestA>();
            testA1.Show();
            var testB = contaier.Resolve<ITestB>();
            testB.Show();
            var testB1 = contaier.Resolve<ITestB>();
            testB1.Show();
            Console.WriteLine($"瞬态：{object.ReferenceEquals(testA, testA1)}");

            #region 作用域
            #region 方法注入  InstancePerMatchingLifetimeScope使用作用域及子作用域，匹配作用域，只有一个实例，无论是父子作用域还是父下面的不同子作用域他们的实例都是相同的
            //using var scope = contaier.BeginLifetimeScope("TEST456");
            //var testG = scope.Resolve<ITestG>();
            //testG.Show();//测试方法注入
            #endregion

            #region 属性注入  InstancePerMatchingLifetimeScope使用作用域及子作用域，匹配作用域，只有一个实例，无论是父子作用域还是父下面的不同子作用域他们的实例都是相同的

            //using var scope = contaier.BeginLifetimeScope("TEST123");
            //var testE = scope.Resolve<ITestE>();
            //testE.Show();//测试属性注入

            #endregion

            #region 属性注入 InstancePerMatchingLifetimeScope使用作用域及子作用域，匹配作用域，只有一个实例，无论是父子作用域还是父下面的不同子作用域他们的实例都是相同的
            //ITestD testD5;
            //ITestD testD6;
            //ITestD testD7;
            //using var scope = contaier.BeginLifetimeScope("TEST");
            //var testD = scope.Resolve<ITestD>();
            var testA = scope.Resolve<ITestA>();//测试属性注入
            testA.Show();
            //testD.Show();//测试属性注入
            //testD5 = testD;

            子作用域
            //using var scope1 = scope.BeginLifetimeScope();
            //var testD1 = scope1.Resolve<ITestD>();
            //var testD11 = scope1.Resolve<ITestD>();
            //testD6 = testD1;
            //Console.WriteLine($"子作用域内部：{object.ReferenceEquals(testD1, testD11)}");

            子作用域
            //using var scope2 = scope.BeginLifetimeScope();
            //var testD2 = scope2.Resolve<ITestD>();
            //testD7 = testD2;
            //var testD21 = scope2.Resolve<ITestD>();
            //Console.WriteLine($"子作用域内部：{object.ReferenceEquals(testD2, testD21)}");

            //Console.WriteLine($"作用域及子作用域：{object.ReferenceEquals(testD5, testD6)}");
            //Console.WriteLine($"不同子作用域对比：{object.ReferenceEquals(testD6, testD7)}");
            #endregion

            #region InstancePerLifetimeScope使用作用域及子作用域
            //ITestC testC5;
            //ITestC testC6;
            //ITestC testC7;
            //using var scope = contaier.BeginLifetimeScope();
            //var testC = scope.Resolve<ITestC>();
            //testC5 = testC;

            子作用域
            //using var scope1 = scope.BeginLifetimeScope();
            //var testC1 = scope1.Resolve<ITestC>();
            //var testC11 = scope1.Resolve<ITestC>();
            //testC6 = testC1;
            //Console.WriteLine($"子作用域内部：{object.ReferenceEquals(testC1, testC11)}");

            子作用域
            //using var scope2 = scope.BeginLifetimeScope();
            //var testC2 = scope2.Resolve<ITestC>();
            //testC7 = testC2;
            //var testC21 = scope2.Resolve<ITestC>();
            //Console.WriteLine($"子作用域内部：{object.ReferenceEquals(testC2, testC21)}");

            //Console.WriteLine($"作用域及子作用域：{object.ReferenceEquals(testC5, testC6)}");
            //Console.WriteLine($"不同子作用域对比：{object.ReferenceEquals(testC6, testC7)}");
            #endregion

            #region InstancePerLifetimeScope使用同一作用域及不同作用域
            //ITestC testC5;
            //ITestC testC6;
            //using var scope1 = contaier.BeginLifetimeScope();
            //var testC = scope1.Resolve<ITestC>();
            //testC.Show();
            //testC5 = testC;
            //var testC1 = scope1.Resolve<ITestC>();
            //testC1.Show();
            //Console.WriteLine($"相同作用域：{object.ReferenceEquals(testC, testC1)}");

            //using var scope2 = contaier.BeginLifetimeScope();
            //var testC3 = scope2.Resolve<ITestC>();
            //testC3.Show();
            //testC6 = testC3;
            //var testC4 = scope2.Resolve<ITestC>();
            //testC4.Show();
            //Console.WriteLine($"相同作用域：{object.ReferenceEquals(testC3, testC4)}");

            //Console.WriteLine($"不同作用域对比：{object.ReferenceEquals(testC5, testC6)}");
            #endregion

            #region InstancePerLifetimeScope使用作用域
            //using var scope = contaier.BeginLifetimeScope();
            //var testC = scope.Resolve<ITestC>();
            //testC.Show();
            //var testC1 = scope.Resolve<ITestC>();
            //testC1.Show();
            //Console.WriteLine($"作用域：{object.ReferenceEquals(testC, testC1)}");
            #endregion

            #region InstancePerLifetimeScope默认作用域单例
            //var testC = contaier.Resolve<ITestC>();
            //testC.Show();
            //var testC1 = contaier.Resolve<ITestC>();
            //testC1.Show(); 
            //Console.WriteLine($"作用域：{object.ReferenceEquals(testC, testC1)}");
            #endregion
            #endregion

            #region 单例
            //var testB = contaier.Resolve<ITestB>();
            //testB.Show();
            //var testB1 = contaier.Resolve<ITestB>();
            //testB1.Show();
            //Console.WriteLine($"单例：{object.ReferenceEquals(testB, testB1)}");
            #endregion

            #region 瞬态
            //var testA = contaier.Resolve<ITestA>();
            testA.Show();
            //var testA1 = contaier.Resolve<ITestA>();
            testA1.Show();
            //Console.WriteLine($"瞬态：{object.ReferenceEquals(testA,testA1)}"); 
            #endregion

            Console.WriteLine($"测试完成。。。");
        }
 
        #endif
    }

    /// <summary>
    /// autofac中自定义属性选择器类
    /// </summary>
    public class MyPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.GetCustomAttributes().Any(att => att.GetType() == typeof(CustomPropAttribute));
        }
    }

    /// <summary>
    /// 标记不同的属性--特性标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomPropAttribute : Attribute
    {
    }
}
