using BookSzop.Utils;
using BookSzop.ViewModels;
using BookSzop.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookSzop
{
    /// <summary>
    /// Main entry for application
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider _ServiceProvider { get; set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // TODO: Logic layer services need earlier registration of database services
            // For now it's disabled, pending resolving references issues

            // Services
            // services.AddSingleton<IAuthenticationManager, AuthenticationManager>();

            // View Models
            services.AddSingleton<LoginPageViewModel>();
            services.AddSingleton<MainPageViewModel>();

            // Pages
            services.AddSingleton<LoginPage>();
            services.AddSingleton<MainPage>();

            services.AddSingleton<MainWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        void App_Navigated(object sender, NavigationEventArgs eventArgs)
        {
            Page page = eventArgs.Content as Page;
            if (page != null)
            {
                NavigationHelper.NavigationService = page.NavigationService;
            }
        }

    }
}
