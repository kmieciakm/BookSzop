using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.ViewModels.Base;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels.Dialogs
{
    public class BookBundleDialogViewModel : DialogViewModelBase
    {
        private BookBundle _bundleToSave { get; set; }
        private IStoreManagementService _storeService { get; }

        public BookBundleDialogViewModel(BookBundle bundle, IStoreManagementService storeService)
        {
            _bundleToSave = bundle;
            _storeService = storeService;

            _price = _bundleToSave?.Price == 0 ? "" : _bundleToSave?.Price.ToString();
            _quantity = _bundleToSave?.Quantity.ToString();
            _bookId = _bundleToSave?.BookId == 0 ? "" : _bundleToSave?.BookId.ToString();
        }

        private string _price { get; set; }
        public string Price {
            get => _price.ToString();
            set
            {
                _price = value;
                ClearErrors(nameof(Price));
                ValidateIsPositiveRealNumber(nameof(Price), Price);
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(SaveButtonEnable));
            }
        }
        private string _quantity { get; set; }
        public string Quantity
        {
            get => _quantity.ToString();
            set
            {
                _quantity = value;
                ClearErrors(nameof(Quantity));
                ValidateIsPositiveIntegerNumber(nameof(Quantity), Quantity);
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(SaveButtonEnable));
            }
        }
        private string _bookId { get; set; }
        public string BookId
        {
            get => _bookId.ToString();
            set
            {
                _bookId = value;
                ClearErrors(nameof(BookId));
                ValidateBookId(BookId);
                OnPropertyChanged(nameof(BookId));
                OnPropertyChanged(nameof(SaveButtonEnable));
            }
        }
        public bool SaveButtonEnable
        {
            get => IsFormCorrect();
        }

        public override ICommand Save
        {
            get => new RelayCommand(param =>
            {
                _bundleToSave.BookId = int.Parse(_bookId);
                _bundleToSave.Price = Math.Ceiling(double.Parse(_price) * 100) / 100;
                _bundleToSave.Quantity = int.Parse(_quantity);

                OnSave?.Invoke();
                OnClose?.Invoke();
            });
        }
        public override ICommand Cancel
        {
            get => new RelayCommand(param =>
            {
                OnClose?.Invoke();
            });
        }

        public bool IsFormCorrect()
        {
            return
                !(
                    HasErrors ||
                    string.IsNullOrEmpty(Price) ||
                    string.IsNullOrEmpty(Quantity) ||
                    string.IsNullOrEmpty(BookId)
                );
        }

        private void ValidateBookId(string bookId)
        {
            if (!int.TryParse(bookId, out int result) || !_storeService.BookExists(result))
            {
                AddError(nameof(BookId), $"Book of such id does not exist.");
            }
        }
    }
}
