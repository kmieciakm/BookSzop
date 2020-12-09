using BookSzop.Commands;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class TransactionPageViewModel : ViewModelBase
    {
        public ICommand BackCommand
        {
            get => new RelayCommand(param =>
            {
                SessionHelper.RefreshSession();
                NavigationHelper.NavigationService.GoBack();
            });
        }
    }
}
