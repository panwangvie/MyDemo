using System;

namespace AOP.AspectInjector
{
    [LogAspect]
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyAoP("小潘", 26);
            MyAoP2("小潘", 26);
        }

       
        public static void MyAoP(string name, int age)
        {
            Console.WriteLine($"{name}{age}");

        }
        [LogAspect]
        public static string MyAoP2(string name, int age)
        {
            Console.WriteLine($"{name}{age}");

            return $"{name}{age}";

        }
    }
}
