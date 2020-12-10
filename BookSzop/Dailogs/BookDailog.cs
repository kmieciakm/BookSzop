using BookSzop.Models;
using BookSzop.ViewModels.Dialogs;
using BookSzop.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Dailogs
{
    public class BookDailog : IDialog<Book>
    {
        private BookDialogView bookDailogView { get; set; }
        private BookDialogViewModel bookDailogViewModel { get; set; }

        public void Show(Book book, Action onConfirm)
        {
            bookDailogViewModel = new BookDialogViewModel(book);
            bookDailogView = new BookDialogView(bookDailogViewModel);

            bookDailogViewModel.OnClose += Close;
            bookDailogViewModel.OnSave += onConfirm;

            bookDailogView.Show();
        }

        public void Close()
        {
            bookDailogView.Close();
        }
    }
}
