using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using BookSzop.Views;
using DatabaseManager.Models;
using ShopService.UserServ;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private INavigationHelper _navigation { get; }
        private IUserService _userService;

        public UserPageViewModel(INavigationHelper navigation, IUserService userService)
        {
            SessionHelper.SessionChanged += UpdateLoggedInUserData;
            _navigation = navigation;
            _userService = userService;
        }

        #region Session
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        private int? _UserId { get; set; } = null;
        public void UpdateLoggedInUserData(object sender, EventArgs eventArgs)
        {
            _UserId = SessionHelper.GetSessionUserId();
            if (_UserId.HasValue)
            {
                var userId = _UserId.Value;
                _userService.GetBooksOfUser(userId)?.ForEach(b => _books.Add(b));
                WelcomeMessage = "Welcome " + _userService.GetUserName(userId);
            }
        }

        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
        #endregion

        private ObservableCollection<Book> _books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books { get => _books; }
    }
}
