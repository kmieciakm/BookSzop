﻿using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using BookSzop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private INavigationHelper _navigation { get; }

        public UserPageViewModel(INavigationHelper navigation)
        {
            SessionHelper.SessionChanged += UpdateLoggedInUserId;
            _navigation = navigation;
        }

        #region Session
        private string _welcomeMessage;
        public string WelcomeMessage {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        private int? _UserId { get; set; } = null;
        public void UpdateLoggedInUserId(object sender, EventArgs eventArgs)
        {
            _UserId = SessionHelper.GetSessionUserId();
            WelcomeMessage = "Welcome " + _UserId.Value;
        }

        public ICommand LogoutCommand
        {
            get => new RelayCommand(param => {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
        #endregion
    }
}
