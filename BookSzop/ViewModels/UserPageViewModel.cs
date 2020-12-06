using BookSzop.Commands;
using BookSzop.Models;
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
        private IUserService _userService { get; }
        private PurchasePage _purchasePage { get; }

        private UserModel _UserModel { get; }

        public UserPageViewModel(INavigationHelper navigation, IUserService userService, PurchasePage purchasePage)
        {
            SessionHelper.SessionChanged += UpdateUserData;
            _navigation = navigation;
            _userService = userService;
            _purchasePage = purchasePage;
            _UserModel = new UserModel();
        }
      
        public string WelcomeMessage
        {
            get => $"Welcome {_UserModel.Name}";
        }
        public ObservableCollection<Book> Books { get => _UserModel.Books; }
        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
        public ICommand PurchaseBooksCommand
        {
            get => new RelayCommand(param =>
            {
                NavigationHelper.Navigate(_purchasePage);
            });
        }

        private void UpdateUserData(object sender, EventArgs eventArgs)
        {
            _UserModel.Id = SessionHelper.GetSessionUserId();
            if (!_UserModel.Id.HasValue) return;

            var userId = _UserModel.Id.Value;

            // Update welcome message
            _UserModel.Name = _userService.GetUserName(userId);
            OnPropertyChanged(nameof(WelcomeMessage));

            // Update welcome message
            _userService
                .GetBooksOfUser(userId)
                ?.ForEach(book => _UserModel.Books.Add(book));
        }
    }
}
