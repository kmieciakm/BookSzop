using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Models.PagesModels;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using ShopService.Models.EventModel;
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
    public class TransactionPageViewModel : ViewModelBase
    {
        private IPurchaseService _purchaseService { get; }
        private IStoreManagementService _storeManagementService { get; }
        private TransactionsModel _transactionsModel { get; }

        public ObservableCollection<IEvent> Orders { get => _transactionsModel.Orders; }
        public ObservableCollection<IEvent> Refunds { get => _transactionsModel.Refunds; }




        public ICommand BackCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.RefreshSession();
                NavigationHelper.NavigationService.GoBack();
            });
        }

        public TransactionPageViewModel(IPurchaseService purchaseService, IStoreManagementService storeManagementService)
        {
            SessionHelper.SessionChanged += UpdateTransactionData;
            _purchaseService = purchaseService;
            _storeManagementService = storeManagementService;
            _transactionsModel = new TransactionsModel();

        }


        public ICommand PlaceRefundCommand
        {
            get => new RelayCommand(eventId =>
            {
                try
                {
                    _purchaseService.PlaceRefund(_transactionsModel.UserId.Value, (int)eventId);
                    UpdateTransactionData(this, new EventArgs());
                }
                catch (PurchaseException purchaseExc)
                {
                    throw purchaseExc;
                }
            });
        }

        public void UpdateTransactionData(object sender, EventArgs e)
        {
            _transactionsModel.UserId = SessionHelper.GetSessionUserId();

            _transactionsModel.Orders.Clear();
            _transactionsModel.Refunds.Clear();

            // Updates transactions list for logged in user
            _purchaseService
                .GetUserPurchases((int)_transactionsModel.UserId)
                .ToList()
                .ForEach(purchase => _transactionsModel.Orders.Add(purchase));

            // Updates refunds list for logged in user
            _purchaseService
                .GetUserRefunds((int)_transactionsModel.UserId)
                .ToList()
                .ForEach(refund => _transactionsModel.Refunds.Add(refund));
        }



    }
}
