using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BookSzop.Models.PagesModels
{
    public class UserModel
    {
        public int? Id { get; set; } = null;
        public string Name { get; set; }
        public ObservableCollection<BookDetail> Books { get; set; } = new ObservableCollection<BookDetail>();
    }
}
