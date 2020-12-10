﻿using BookSzop.Dailogs;
using BookSzop.Models;
using BookSzop.Utils;
using BookSzop.ViewModels;
using BookSzop.ViewModels.Dialogs;
using BookSzop.Views;
using BookSzop.Views.Dialogs;
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
            // Services
            services.AddSingleton(provider => ServiceFactory.CreateAuthenticationManager());
            services.AddSingleton(provider => ServiceFactory.CreateUserService());
            services.AddSingleton(provider => ServiceFactory.CreatePurchaseService());
            services.AddSingleton(provider => ServiceFactory.CreateStoreManagementService());

            // Dialogs
            services.AddSingleton<IDialog<Book>, BookDialog>();
            services.AddSingleton<IDialog<BookBundle>, BookBundleDialog>();

            // View Models
            services.AddTransient<TransactionPageViewModel>();
            services.AddTransient<UserPageViewModel>();
            services.AddTransient<AdminPageViewModel>();
            services.AddTransient<PurchasePageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<BookDialogViewModel>();

            // Pages
            services.AddTransient<TransactionPage>();
            services.AddTransient<UserPage>();
            services.AddTransient<AdminPage>();
            services.AddTransient<PurchasePage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<MainPage>();
            services.AddTransient<BookDialogView>();

            services.AddSingleton<MainWindow>();

            services.AddTransient<INavigationHelper, NavigationHelper>();
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
