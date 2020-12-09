using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using ShopService.Models.BookBundleModel;
using ShopService.Models.BookOrderModel;
using ShopService.Purchase;
using ShopService.StoreManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class PurchasePageViewModel : ViewModelBase
    {
        private IPurchaseService _purchaseService { get; }
        private IStoreManagementService _storeManagementService { get; }

        private PurchaseModel _purchaseModel { get; }

        public PurchasePageViewModel(IPurchaseService purchaseService, IStoreManagementService storeManagementService)
        {
            SessionHelper.SessionChanged += UpdateBasketData;
            _purchaseService = purchaseService;
            _storeManagementService = storeManagementService;
            _purchaseModel = new PurchaseModel();
        }

        public ObservableCollection<IBookBundle> BookBundles { get => _purchaseModel.BookBundles; }
        public ObservableCollection<BasketElement> Basket { get => _purchaseModel.Basket; }
        public string TotalPrice
        {
            get => _purchaseModel.TotalPrice;
            set
            {
                _purchaseModel.TotalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public string Message
        {
            get => _purchaseModel.ErrorMessage;
            set
            {
                _purchaseModel.ErrorMessage = value;
                OnPropertyChanged(nameof(Message));
            }
        }
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
                foreach (var element in Basket)
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
                    try
                    {
                        _purchaseService.MakePurchase(_purchaseModel.BuyerId.Value, booksToOrder);
                        SessionHelper.RefreshSession();
                        NavigationHelper.NavigationService.GoBack();
                    }
                    catch (PurchaseException purchaseExc)
                    {
                        Message = $"{purchaseExc.Message}";
                    }
                }
            });
        }
        public ICommand AddToCardCommand
        {
            get => new RelayCommand(id =>
            {
                // if there is no item in the basket yet, add it
                if (!Basket.Any(element => element.BookBundleId == (int)id))
                {
                    var bundle = BookBundles.FirstOrDefault(bundle => bundle.Id == (int)id);
                    if (bundle != null)
                    {
                        Basket.Add(
                            new BasketElement()
                            {
                                BookBundleId = (int)id,
                                Price = bundle.Price,
                                BookTitle = bundle?.Book.Title
                            }
                        );
                        UpdateTotalPrice();
                    }
                }
            });
        }
        public ICommand DeleteFromBasketCommand
        {
            get => new RelayCommand(id =>
            {
                var elementToDelete = Basket.FirstOrDefault(element => element.BookBundleId == (int)id);
                if (elementToDelete != null)
                {
                    Basket.Remove(elementToDelete);
                    UpdateTotalPrice();
                }
            });
        }
        public ICommand IncrementCommand
        {
            get => new RelayCommand(id =>
            {
                var element = Basket.FirstOrDefault(element => element.BookBundleId == (int)id);
                if (element != null)
                {
                    Basket.Remove(element);
                    element.Increase();
                    Basket.Add(element);
                    UpdateTotalPrice();
                }
            });
        }
        public ICommand DecrementCommand
        {
            get => new RelayCommand(id =>
            {
                var element = Basket.FirstOrDefault(element => element.BookBundleId == (int)id);
                if (element != null)
                {
                    Basket.Remove(element);
                    element.Decrease();
                    Basket.Add(element);
                    UpdateTotalPrice();
                }
            });
        }

        private void UpdateBasketData(object sender, EventArgs e)
        {
            _purchaseModel.BuyerId = SessionHelper.GetSessionUserId();
            if (!_purchaseModel.BuyerId.HasValue) return;

            // Clear View
            Message = string.Empty;
            TotalPrice = string.Empty;
            Basket.Clear();
            BookBundles.Clear();

            // Update Book Bundles
            _storeManagementService
                .GetAllBookBundles()
                ?.ToList()
                .ForEach(bundle => BookBundles.Add(bundle));
        }
        private void UpdateTotalPrice()
        {
            TotalPrice = $"Total: {_purchaseModel.CalculateTotalPrice()}$";
        }
    }
}
