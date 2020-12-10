using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels.Dialogs
{
    public class BookBundleDialogViewModel : DialogViewModelBase
    {
        private BookBundle _bundleToSave { get; set; }

        public BookBundleDialogViewModel(BookBundle bundle)
        {
            _bundleToSave = bundle;

            Price = _bundleToSave?.Price.ToString();
            BooksQuantity = _bundleToSave?.Quantity.ToString();
            BookId = _bundleToSave?.BookId.ToString();
        }

        private double _price { get; set; }
        public string Price {
            get => _price.ToString();
            set
            {
                _price = double.Parse(value);
                OnPropertyChanged(Price);
            }}
        private int _booksQuantity { get; set; }
        public string BooksQuantity
        {
            get => _booksQuantity.ToString();
            set
            {
                _booksQuantity = int.Parse(value);
                OnPropertyChanged(BooksQuantity);
            }
        }
        private int _bookId { get; set; }
        public string BookId
        {
            get => _bookId.ToString();
            set
            {
                _bookId = int.Parse(value);
                OnPropertyChanged(BookId);
            }
        }

        public override ICommand Save
        {
            get => new RelayCommand(param =>
            {
                _bundleToSave.BookId = _bookId;
                _bundleToSave.Price = _price;
                _bundleToSave.Quantity = _booksQuantity;

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
    }
}
