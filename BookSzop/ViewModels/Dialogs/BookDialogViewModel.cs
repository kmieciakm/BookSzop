using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.ViewModels.Base;
using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels.Dialogs
{
    public class BookDialogViewModel : DailogViewModelBase
    {
        private Book _bookToSave { get; set; }

        public BookDialogViewModel(Book book)
        {
            _bookToSave = book;
        }

        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public ICommand Save
        {
            get => new RelayCommand(param =>
            {
                _bookToSave.Author = Author;
                _bookToSave.Title = Title;

                OnSave?.Invoke();
                OnClose?.Invoke();
            });
        }
        public ICommand Cancel
        {
            get => new RelayCommand(param =>
            {
                OnClose?.Invoke();
            });
        }
    }
}
