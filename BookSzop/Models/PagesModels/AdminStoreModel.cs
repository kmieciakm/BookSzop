using ShopService.Models.BookBundleModel;
using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BookSzop.Models.PagesModels
{
    public class AdminStoreModel
    {
        public string ErrorMessage { get; set; }
        public ObservableCollection<IBook> Books { get; } = new ObservableCollection<IBook>();
        public ObservableCollection<IBookBundle> BookBundles { get; } = new ObservableCollection<IBookBundle>();
    }
}
