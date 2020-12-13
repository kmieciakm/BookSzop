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
        public ObservableCollection<IEvent> Transactions { get => _transactionsModel.Transactions; }




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


        public void UpdateTransactionData(object sender, EventArgs e)
        {
            int? userId = SessionHelper.GetSessionUserId();

            _transactionsModel.Transactions.Clear();

            _purchaseService
                .GetUserPurchases((int)userId)
                .ToList()
                .ForEach(purchase => _transactionsModel.Transactions.Add(purchase));

        }


    }
}
