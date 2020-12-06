using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class AdminPageViewModel : ViewModelBase
    {
        private INavigationHelper _navigation { get; }

        public AdminPageViewModel(INavigationHelper navigation)
        {
            _navigation = navigation;
        }

        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
    }
}
