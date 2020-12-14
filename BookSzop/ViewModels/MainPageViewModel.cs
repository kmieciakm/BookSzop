using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationHelper _Navigation { get; }

        public MainPageViewModel(INavigationHelper navigation)
        {
            _Navigation = navigation;
        }

        public string Title
        {
            get
            {
                return "Welcome in Book SZOP !";
            }
        }
        public ICommand ContinueButtonCommand
        {
            get => new RelayCommand(param => _Navigation.NavigateToLoginPage());
        }
    }
}
