using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using BookSzop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookSzop.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private LoginPage LoginPage { get; }

        public MainPageViewModel(LoginPage loginPage)
        {
            title = "Welcome in Book SZOP !";
            LoginPage = loginPage;
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public ICommand ContinueButtonCommand
        {
            get
            {
                return new RelayCommand(param => NavigationHelper.Navigate(LoginPage));
            }
        }

    }
}
