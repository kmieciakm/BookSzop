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
    public class BookDialogViewModel : DialogViewModelBase
    {
        private Book _bookToSave { get; set; }

        public BookDialogViewModel(Book book)
        {
            _bookToSave = book;

            Author = book.Author;
            Title = book.Title;
        }

        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public override ICommand Save
        {
            get => new RelayCommand(param =>
            {
                _bookToSave.Author = Author;
                _bookToSave.Title = Title;

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
