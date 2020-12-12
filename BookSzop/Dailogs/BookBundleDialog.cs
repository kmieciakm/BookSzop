using BookSzop.Models;
using BookSzop.ViewModels.Dialogs;
using BookSzop.Views.Dialogs;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Dailogs
{
    public class BookBundleDialog : IDialog<BookBundle>
    {
        private BookBundleDialogView bundleDailogView { get; set; }
        private BookBundleDialogViewModel bundleDailogViewModel { get; set; }

        private IStoreManagementService _storeService;

        public BookBundleDialog(IStoreManagementService storeService)
        {
            _storeService = storeService;
        }

        public void Show(BookBundle bookBundle, Action onConfirm)
        {
            bundleDailogViewModel = new BookBundleDialogViewModel(bookBundle, _storeService);
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
