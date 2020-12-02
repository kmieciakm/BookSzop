using BookSzop.Utils;
using BookSzop.ViewModels;
using BookSzop.Views;
using DatabaseManager;
using DatabaseManager.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;
using ShopService;
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
            // Database setup
            var dbContext = DbFactory.CreateSQLiteDb();
            services.AddSingleton(provider => dbContext);
            services.AddSingleton(provider => DbFactory.CreateUserRepository(dbContext));
            services.AddSingleton(provider => DbFactory.CreateBookRepository(dbContext));
            services.AddSingleton(provider => DbFactory.CreateBookBundleRepository(dbContext));
            services.AddSingleton(provider => DbFactory.CreateBookOrderRepository(dbContext));
            services.AddSingleton(provider => DbFactory.CreateEventsRepository(dbContext));

            // Services
            services.AddSingleton(provider =>
                ServiceFactory.CreateAuthenticationManager(
                    provider.GetRequiredService<IUserRepository>()
                ));
            services.AddSingleton(provider => 
                ServiceFactory.CreateAuthenticationManager(
                    provider.GetRequiredService<IUserRepository>()
                ));
            services.AddSingleton(provider =>
                ServiceFactory.CreatePurchaseService(
                    provider.GetRequiredService<IEventsRepository>(),
                    provider.GetRequiredService<IUserRepository>(),
                    provider.GetRequiredService<IBookBundleRepositiory>()
                ));
            services.AddSingleton(provider =>
                ServiceFactory.CreateStoreManagementService(
                    provider.GetRequiredService<IBookRepository>(),
                    provider.GetRequiredService<IBookBundleRepositiory>()
                ));

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
