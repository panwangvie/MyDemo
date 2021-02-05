using System;
using System.Reflection;


/// <summary>
/// 
/// </summary>
namespace MyReflection
{
    class Program
    {
#if region
如何实现反射；反射的应用
1.使用Assemby.LoadFile方法动态加载程序集， 通过CreateInstance方法创建对象，通过GetType方法获取对象的类型。
也可以通过Activator.CreateInstance创建对象实例。Assembly内部调用的是Activator的

#endif
        static void Main(string[] args)
        {
            Activator.CreateInstance("Kernel.SimpleLibrary", "Kernel.SimpleLibrary.Person");

            Type type = Assembly.Load("StudyInvokeMethod").GetType("StudyInvokeMethod.HomeService");

            var assembly = Assembly.LoadFile("");

            Type t1 = typeof(Person);
            MethodInfo mi1 = t1.GetMethod("SayHello", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
            MethodInfo mi2 = t1.GetMethod("SayHello", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(String) }, null);
            MethodInfo mi3 = t1.GetMethod("SayHello", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(String), typeof(Int32) }, null);

            //如何分别反射以下三个方法？
            //private void SayHello<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
            //private void SayHello<T1>(T1 t)
            //private void GM1<T1>(T1 t)

            object obj1 = Activator.CreateInstance(t1);
            //开始调用

            Assembly.Load("").CreateInstance("");
            //
            Console.ReadKey();
        }
    }
    class Person
    {
        private void SayHello()
        {
            Console.WriteLine("我是一个无参的方法");
        }
        private void SayHello(string s)
        {
            Console.WriteLine("我是一个有参的方法，参数：{0}", s);
        }
        private void SayHello(string s, int i)
        {
            Console.WriteLine("我是一个有两个参数的方法，参数：{0}、{1}", s, i);
        }
        private void SayHello<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
        {
            Console.WriteLine(t1.ToString() + t2.ToString() + t3.ToString());
        }
        private void SayHello<T1>(T1 t)
        {
            Console.WriteLine(t.ToString());
        }
        private void GM1<T1>(T1 t)
        {
            Console.WriteLine(t.ToString());
        }
    }
}
