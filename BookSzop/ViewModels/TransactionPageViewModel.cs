using BookSzop.Commands;
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
        private TransactionsModel _transactionsModel { get; }

        public ObservableCollection<IEvent> Orders { get => _transactionsModel.Orders; }
        public ObservableCollection<IEvent> Refunds { get => _transactionsModel.Refunds; }

        public TransactionPageViewModel(IPurchaseService purchaseService)
        {
            SessionHelper.SessionChanged += UpdateTransactionData;
            _purchaseService = purchaseService;
            _transactionsModel = new TransactionsModel();

            UpdateTransactionData(this, new EventArgs());
        }

        public string Message
        {
            get => _transactionsModel.ErrorMessage;
            set
            {
                _transactionsModel.ErrorMessage = value;
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
        public ICommand PlaceRefundCommand
        {
            get => new RelayCommand(eventId =>
            {
                try
                {
                    if (eventId == null)
                    {
                        Message = "Choose order which you want to refund";
                    }
                    else
                    {
                        _purchaseService.PlaceRefund(_transactionsModel.UserId.Value, (int)eventId);
                        UpdateTransactionData(this, new EventArgs());
                    }

                }
                catch (PurchaseException purchaseExc)
                {
                    Message = purchaseExc.Message;
                }
            });
        }

        public void UpdateTransactionData(object sender, EventArgs e)
        {
            _transactionsModel.UserId = SessionHelper.GetSessionUserId();
            if (!_transactionsModel.UserId.HasValue) return;

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
