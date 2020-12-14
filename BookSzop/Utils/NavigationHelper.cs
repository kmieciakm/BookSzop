using BookSzop.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookSzop.Utils
{
    public class NavigationHelper : INavigationHelper
    {
        private IServiceProvider _serviceProvider { get; }

        public NavigationHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static NavigationService NavigationService { get; set; }

        public static void Navigate(Page page)
        {
            NavigationService.Navigate(page);
        }

        public void NavigateToLoginPage() => Navigate(_serviceProvider.GetRequiredService<LoginPage>());
        public void NavigateToAdminPage() => Navigate(_serviceProvider.GetRequiredService<AdminPage>());
        public void NavigateToUserPage() => Navigate(_serviceProvider.GetRequiredService<UserPage>());
        public void NavigateToPurchasePage() => Navigate(_serviceProvider.GetRequiredService<PurchasePage>());
        public void NavigateToTransactionPage() => Navigate(_serviceProvider.GetRequiredService<TransactionPage>());
    }
}
