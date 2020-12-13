using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using ShopService.Models.BookBundleModel;
using ShopService.Models.BookModel;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private ObservableCollection<IBook> _Books { get; } = new ObservableCollection<IBook>();
        private ObservableCollection<IBookBundle> _BookBundles { get; } = new ObservableCollection<IBookBundle>();

        public ObservableCollection<IBook> Books { get => _Books; }
        public ObservableCollection<IBookBundle> BookBundles { get => _BookBundles; }
        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
        public ICommand DeleteBookCommand
        {
            get => new RelayCommand(bookId =>
            {
                try
                {
                    _storeManagementService.RemoveBook((int)bookId);
                    // Remove from view when database delete opertation succedeed
                    var bookToRemove = Books.FirstOrDefault(book => book.Id == (int)bookId);
                    Books.Remove(bookToRemove);
                    var bundleWithRemovedBook = BookBundles.Where(bundle => bundle.BookId == (int)bookId).ToList();
                    bundleWithRemovedBook.ForEach(bundle => BookBundles.Remove(bundle));
                }
                catch (StoreManagementException storeExc)
                {
                    
                }
            });
        }
        public ICommand DeleteBookBundleCommand
        {
            get => new RelayCommand(bundleId =>
            {
                
                try
                {
                    _storeManagementService.RemoveBookBundle((int)bundleId);
                    // Remove from view when database delete opertation succedeed
                    var bundleToRemove = BookBundles.FirstOrDefault(bundle => bundle.Id == (int)bundleId);
                    BookBundles.Remove(bundleToRemove);
                }
                catch (StoreManagementException storeExc)
                {

                }
            });
        }

        public void UpdateStoreData()
        {
            Books.Clear();
            BookBundles.Clear();

            _storeManagementService
                .GetAllBooks()
                ?.ToList()
                .ForEach(book => Books.Add(book));
            _storeManagementService
                .GetAllBookBundles()
                ?.ToList()
                .ForEach(bundle => BookBundles.Add(bundle));
        }
    }
}
