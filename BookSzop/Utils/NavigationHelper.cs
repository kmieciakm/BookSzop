using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookSzop.Utils
{
    public static class NavigationHelper
    {
        private static NavigationService navigator;

        public static NavigationService NavigationService
        {
            set
            {
                navigator = value;
            }
            get => navigator;
        }

        public static void Navigate(Page page)
        {
            NavigationService.Navigate(page);
        }
    }
}
