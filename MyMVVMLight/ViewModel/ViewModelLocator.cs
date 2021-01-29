/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MyMVVMLight"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;

namespace MyMVVMLight.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }
        private INavigationService CreateNavigationService()
        {
            var nav = new NavigationService();

            var navigationService = new NavigationService();
            navigationService.Configure("PageTwo1", new Uri("/MvvmLightSample;component/Views/PageOne.xaml", UriKind.Relative));
            navigationService.Configure("PageTwo2", new Uri("/MvvmLightSample;component/Views/PageTwo.xaml", UriKind.Relative));
            navigationService.Configure("PageTwo3", new Uri("/MvvmLightSample;component/Views/PageThree.xaml", UriKind.Relative));
            navigationService.Configure("PageTwo4", new Uri("/MvvmLightSample;component/Views/PageFour.xaml", UriKind.Relative));
            return navigationService;
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WelcomeViewModel Welcome
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            }
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public PageOneViewModel PageOne
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageOneViewModel>();
            }
        }
        public PageTwoViewModel PageTwo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageTwoViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}