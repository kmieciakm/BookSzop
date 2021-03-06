﻿using ShopService.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BookSzop.Models.PagesModels
{
    class TransactionsModel
    {
        public int? UserId { get; set; }
        public string ErrorMessage { get; set; }
        public ObservableCollection<IEvent> Orders { get; } = new ObservableCollection<IEvent>();
        public ObservableCollection<IEvent> Refunds { get; } = new ObservableCollection<IEvent>();
    }
}
