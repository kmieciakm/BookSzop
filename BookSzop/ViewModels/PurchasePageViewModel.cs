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
    public class PurchasePageViewModel : ViewModelBase
    {
        private IPurchaseService _purchaseService { get; }
        private IStoreManagementService _storeManagementService { get; }

        public PurchasePageViewModel(IPurchaseService purchaseService, IStoreManagementService storeManagementService)
        {
            SessionHelper.SessionChanged += UpdateBasketData;
            _purchaseService = purchaseService;
            _storeManagementService = storeManagementService;
        }

        private int? _Id;
        private ObservableCollection<BookBundle> _BookBundles { get; } = new ObservableCollection<BookBundle>();
        public ObservableCollection<BookBundle> BookBundles { get => _BookBundles; }
        public ICommand BackCommand
        {
            get => new RelayCommand(param =>
            {
                NavigationHelper.NavigationService.GoBack();
            });
        }
        public ICommand AddToCardCommand
        {
            get => new RelayCommand(param =>
            {
                throw new NotImplementedException();
            });
        }

        private void UpdateBasketData(object sender, EventArgs e)
        {
            _Id = SessionHelper.GetSessionUserId();
            if (!_Id.HasValue) return;

            var userId = _Id.Value;

            // Update Book Bundles
            _storeManagementService
                .GetAllBookBundles()
                ?.ForEach(bundle => _BookBundles.Add(bundle));
        }
    }
}
