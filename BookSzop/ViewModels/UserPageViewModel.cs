using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Models.PagesModels;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
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

        private UserModel _UserModel { get; }

        public UserPageViewModel(INavigationHelper navigation, IUserService userService)
        {
            SessionHelper.SessionChanged += UpdateUserData;
            _navigation = navigation;
            _userService = userService;
            _UserModel = new UserModel();

            UpdateUserData(this, new EventArgs());
        }

        public string WelcomeMessage
        {
            get => $"Welcome {_UserModel.Name}";
        }
        public ObservableCollection<BookDetail> Books { get => _UserModel.Books; }
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
                _navigation.NavigateToPurchasePage();
            });
        }
        public ICommand TransactionsCommand
        {
            get => new RelayCommand(param =>
            {
                _navigation.NavigateToTransactionPage();
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

            // Update books collection
            _UserModel.Books.Clear();
            _userService
                .GetBooksOfUser(userId)
                ?.ToList()
                .ForEach(book => _UserModel.Books.Add(
                    new BookDetail()
                    {
                        Title = book.Title,
                        Author = book.Author,
                        Amount = _userService.GetOwnedBookAmount(userId, book.Id)
                    }
                ));
        }
    }
}
