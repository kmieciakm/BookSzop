using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.BookBundleModel
{
    public interface IBookBundle
    {
        public int Id { get; set; }
        public IBook Book { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
