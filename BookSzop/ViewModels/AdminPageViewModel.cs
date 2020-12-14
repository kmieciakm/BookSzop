using BookSzop.Commands;
using BookSzop.Dailogs;
using BookSzop.Models;
using BookSzop.Models.PagesModels;
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

        private AdminStoreModel _adminModel { get; }

        private IDialog<Book> _bookDialog { get; }
        private IDialog<BookBundle> _bookBundleDialog { get; }

        public AdminPageViewModel(INavigationHelper navigation, IStoreManagementService storeManagementService,
            IDialog<Book> bookDialog, IDialog<BookBundle> bookBundleDialog)
        {
            _navigation = navigation;
            _storeManagementService = storeManagementService;
            _adminModel = new AdminStoreModel();
            _bookDialog = bookDialog;
            _bookBundleDialog = bookBundleDialog;

            UpdateStoreData();
        }

        public string Message
        {
            get => _adminModel.ErrorMessage;
            set
            {
                _adminModel.ErrorMessage = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public ObservableCollection<IBook> Books { get => _adminModel.Books; }
        public ObservableCollection<IBookBundle> BookBundles { get => _adminModel.BookBundles; }
        public ICommand LogoutCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.ClearSession();
                _navigation.NavigateToLoginPage();
            });
        }
        public ICommand AddBookCommand
        {
            get => new RelayCommand(param =>
            {
                Book newBook = new Book();
                _bookDialog.Show(newBook, () =>
                {
                    if (newBook != null)
                    {
                        try
                        {
                            _storeManagementService.RegisterBook(newBook);
                            UpdateStoreData();
                        }
                        catch (StoreManagementException storeExc)
                        {
                            Message = storeExc.Message;
                        }
                    }
                });
            });
        }
        public ICommand EditBookCommand
        {
            get => new RelayCommand(bookId =>
            {
                IBook book = Books.First(book => book.Id == (int)bookId);
                Book bookToUpdate = new Book(book);
                _bookDialog.Show(bookToUpdate, () =>
                {
                    if (bookToUpdate != null)
                    {
                        try
                        {
                            _storeManagementService.UpdateBook(bookToUpdate);
                            UpdateStoreData();
                        }
                        catch (StoreManagementException storeExc)
                        {
                            Message = storeExc.Message;
                        }
                    }
                });
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
                    Message = storeExc.Message;
                }
            });
        }
        public ICommand AddBookBundleCommand
        {
            get => new RelayCommand(param =>
            {
                BookBundle newBookBundle = new BookBundle();
                _bookBundleDialog.Show(newBookBundle, () =>
                {
                    if (newBookBundle != null)
                    {
                        try
                        {
                            _storeManagementService.RegisterBookBundle(newBookBundle);
                            UpdateStoreData();
                        }
                        catch (StoreManagementException storeExc)
                        {
                            Message = storeExc.Message;
                        }
                    }
                });
            });
        }
        public ICommand EditBookBundleCommand
        {
            get => new RelayCommand(bundleId =>
            {
                IBookBundle bundle = BookBundles.First(bundle => bundle.Id == (int)bundleId);
                BookBundle bundleToUpdate = new BookBundle(bundle);
                _bookBundleDialog.Show(bundleToUpdate, () =>
                {
                    if (bundleToUpdate != null)
                    {
                        try
                        {
                            _storeManagementService.UpdateBookBundle(bundleToUpdate);
                            UpdateStoreData();
                        }
                        catch (StoreManagementException storeExc)
                        {
                            Message = storeExc.Message;
                        }
                    }
                });
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
                    Message = storeExc.Message;
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
