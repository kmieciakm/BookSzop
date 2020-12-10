using BookSzop.Models;
using BookSzop.ViewModels.Dialogs;
using BookSzop.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Dailogs
{
    public class BookBundleDialog : IDialog<BookBundle>
    {
        private BookBundleDialogView bundleDailogView { get; set; }
        private BookBundleDialogViewModel bundleDailogViewModel { get; set; }

        public void Show(BookBundle bookBundle, Action onConfirm)
        {
            bundleDailogViewModel = new BookBundleDialogViewModel(bookBundle);
            bundleDailogView = new BookBundleDialogView(bundleDailogViewModel);

            bundleDailogViewModel.OnClose += Close;
            bundleDailogViewModel.OnSave += onConfirm;

            bundleDailogView.Show();
        }

        public void Close()
        {
            bundleDailogView.Close();
        }
    }
}
