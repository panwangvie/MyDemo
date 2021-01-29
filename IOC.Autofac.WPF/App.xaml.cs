using Autofac;
using MyAf.Middleware;
using MyAutofac.IViewModel;
using MyAutofac.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IOC.Autofac.WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

           var builder =  new  ContainerBuilder();

           builder.AddRepository<LoginVM, ILoginVM>();
            //builder.RegisterInstance<LoginVM>.As<ILoginVM>();
            AfProvider.InitAfProvider(builder.Build());
        }
    }
}
