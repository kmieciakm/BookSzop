using ShopService.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BookSzop.Models
{
    class TransactionsModel
    {
        public ObservableCollection<IEvent> Transactions { get; } = new ObservableCollection<IEvent>();
    }
}
