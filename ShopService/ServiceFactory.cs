using DatabaseManager.Repository.Contracts;
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
        public static IAuthenticationManager CreateAuthenticationManager(IUserRepository userRepository)
            => new AuthenticationManager(userRepository);

        public static IUserService CreateUserService(IUserRepository userRepository)
            => new UserService(userRepository);

        public static IStoreManagementService CreateStoreManagementService(IBookRepository bookRepository, IBookBundleRepositiory bookBundleRepositiory)
            => new StoreManagementService(bookRepository, bookBundleRepositiory);

        public static IPurchaseService CreatePurchaseService(IEventsRepository eventsRepository, IUserRepository userRepository, IBookBundleRepositiory bookBundleRepositiory)
           => new PurchaseService(eventsRepository, userRepository, bookBundleRepositiory);
    }
}