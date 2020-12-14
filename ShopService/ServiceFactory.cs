using DatabaseManager;
using DatabaseManager.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Authentication;
using ShopService.Purchase;
using ShopService.StoreManagement;
using ShopService.UserServ;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService
{
    public static class ServiceFactory
    {
        public static IServiceProvider databaseServiceProvider { get; } = new ServiceCollection()
            .AddSingleton(provider => DbFactory.CreateSQLiteDb())
            .AddSingleton(provider => DbFactory.CreateUserRepository(provider.GetRequiredService<DbContextBase>()))
            .AddSingleton(provider => DbFactory.CreateBookRepository(provider.GetRequiredService<DbContextBase>()))
            .AddSingleton(provider => DbFactory.CreateBookBundleRepository(provider.GetRequiredService<DbContextBase>()))
            .AddSingleton(provider => DbFactory.CreateBookOrderRepository(provider.GetRequiredService<DbContextBase>()))
            .AddSingleton(provider => DbFactory.CreateEventsRepository(provider.GetRequiredService<DbContextBase>()))
            .BuildServiceProvider();

        public static IAuthenticationManager CreateAuthenticationManager()
            => new AuthenticationManager(
                databaseServiceProvider.GetRequiredService<IUserRepository>()
            );

        public static IUserService CreateUserService()
            => new UserService(
                databaseServiceProvider.GetRequiredService<IUserRepository>()
            );

        public static IStoreManagementService CreateStoreManagementService()
            => new StoreManagementService(
                databaseServiceProvider.GetRequiredService<IBookRepository>(),
                databaseServiceProvider.GetRequiredService<IBookBundleRepositiory>()
            );

        public static IPurchaseService CreatePurchaseService()
            => new PurchaseService(
               databaseServiceProvider.GetRequiredService<IEventsRepository>(),
               databaseServiceProvider.GetRequiredService<IUserRepository>(),
               databaseServiceProvider.GetRequiredService<IBookBundleRepositiory>()
            );
    }
}