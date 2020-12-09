using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BookSzop.Models
{
    public class PurchaseModel
    {
        public int? BuyerId { get; set; }
        public string TotalPrice { get; set; }
        public string ErrorMessage { get; set; }
        public ObservableCollection<BookBundle> BookBundles { get; } = new ObservableCollection<BookBundle>();
        public ObservableCollection<BasketElement> Basket { get; } = new ObservableCollection<BasketElement>();

        public double CalculateTotalPrice() => Basket.Sum(ele => ele.Quantity * ele.Price);
    }
}