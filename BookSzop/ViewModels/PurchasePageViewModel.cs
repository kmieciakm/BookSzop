using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using DatabaseManager.Models;
using ShopService.Models;
using ShopService.Purchase;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<BasketElement> _Basket { get; } = new ObservableCollection<BasketElement>();

        public ObservableCollection<BookBundle> BookBundles { get => _BookBundles; }
        public ObservableCollection<BasketElement> Basket { get => _Basket; }
        public ICommand BackCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.RefreshSession();
                NavigationHelper.NavigationService.GoBack();
            });
        }
        public ICommand PurchaseCommand
        {
            get => new RelayCommand(param =>
            {
                var booksToOrder = new List<IBookOrderCreate>();
                foreach (var element in _Basket)
                {
                    booksToOrder.Add(
                        new BookOrderCreate()
                        {
                            BookBundleId = element.BookBundleId,
                            Quantity = element.Quantity
                        }
                    );
                }

                if (booksToOrder.Count > 0)
                {
                    _purchaseService.MakePurchase(_Id.Value, booksToOrder);
                }

                SessionHelper.RefreshSession();
                NavigationHelper.NavigationService.GoBack();
            });
        }
        public ICommand AddToCardCommand
        {
            get => new RelayCommand(id =>
            {
                if (!_Basket.Any(element => element.BookBundleId == (int)id))
                {
                    _Basket.Add(
                        new BasketElement()
                        {
                            BookBundleId = (int)id,
                            BookTitle = _BookBundles.First(bundle => bundle.Id == (int)id).Book.Title
                        }
                    );
                }
            });
        }
        public ICommand DeleteFromBasketCommand
        {
            get => new RelayCommand(id =>
            {
                var elementToDelete = _Basket.FirstOrDefault(element => element.BookBundleId == (int)id);
                if (elementToDelete != null)
                {
                    _Basket.Remove(elementToDelete);
                }
            });
        }
        public ICommand IncrementCommand
        {
            get => new RelayCommand(id =>
            {
                // BUG - NOT REFLECTED IN VIEW 
                _Basket.FirstOrDefault(element => element.BookBundleId == (int)id).Increase();
            });
        }
        public ICommand DecrementCommand
        {
            get => new RelayCommand(id =>
            {
                // BUG - NOT REFLECTED IN VIEW 
                _Basket.FirstOrDefault(element => element.BookBundleId == (int)id)?.Decrease();
            });
        }

        private void UpdateBasketData(object sender, EventArgs e)
        {
            _Id = SessionHelper.GetSessionUserId();
            if (!_Id.HasValue) return;

            var userId = _Id.Value;

            // Clear Collections
            _Basket.Clear();
            _BookBundles.Clear();

            // Update Book Bundles
            _storeManagementService
                .GetAllBookBundles()
                ?.ForEach(bundle => _BookBundles.Add(bundle));
        }
    }
}
