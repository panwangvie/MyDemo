using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF.IOC.NetCore.Interfaces;
using WPF.IOC.NetCore.Services;

namespace WPF.IOC.NetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        private ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITextService>(provider => new TextService("Hi WPF .NET Core 3.0"));
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var main = _serviceProvider.GetRequiredService<MainWindow>();
            main.Show();

        }
    }
}
