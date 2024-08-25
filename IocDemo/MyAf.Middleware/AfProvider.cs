using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAf.Middleware
{
    public class AfProvider
    {

        public static IContainer Instance { get;private set; }

        public static IContainer InitAfProvider(IContainer container)
        {
            if (Instance == null)
                Instance = container;
            return Instance;
        }

        public static T Resolve<T>()
        {
            if(!Instance.IsRegistered<T>())
                new ArgumentNullException(nameof(T));

            return Instance.Resolve<T>();
        }
        public static T ResolveNamed<T>(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                new ArgumentNullException(nameof(T));

            return Instance.ResolveNamed<T>(typeName);
        }
    }
}
