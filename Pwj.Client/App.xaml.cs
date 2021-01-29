using Autofac;
using Pwj.Client.ViewCenter;
using Pwj.Interfaces;
using Pwj.Shared.Common;
using Pwj.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pwj.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
            var login = NetCoreProvider.ResolveNamed<ILoginCenter>("LoginCenter");
            login.ShowDialog();
        }

        /// <summary>
        /// 注册
        /// </summary>
        private void ConfigureServices()
        {
            var builder = new ContainerBuilder();
            builder.AddViewModel<LoginViewModel, ILoginViewModel>();



            builder.AddViewCenter<LoginCenter,ILoginCenter>();

            NetCoreProvider.RegisterServiceLocator(builder.Build());

        }


    }
}
