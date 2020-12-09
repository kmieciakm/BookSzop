using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using DatabaseManager.Models;
using ShopService.Purchase;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class AdminPageViewModel : ViewModelBase
    {
        private INavigationHelper _navigation { get; }
        private IStoreManagementService _storeManagementService { get; }

        public AdminPageViewModel(INavigationHelper navigation, IStoreManagementService storeManagementService)
        {
            _navigation = navigation;
            _storeManagementService = storeManagementService;

            UpdateStoreData();
        }

        public ObservableCollection<Book> _Books { get; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books { get => _Books; }

        public ObservableCollection<BookBundle> _BookBundles { get; } = new ObservableCollection<BookBundle>();
        public ObservableCollection<BookBundle> BookBundles { get => _BookBundles; }
        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }

        public void UpdateStoreData()
        {
            Books.Clear();
            BookBundles.Clear();

            _storeManagementService
                .GetAllBooks()
                ?.ForEach(book => Books.Add(book));
            _storeManagementService
                .GetAllBookBundles()
                ?.ForEach(bundle => BookBundles.Add(bundle));
        }
    }
}
