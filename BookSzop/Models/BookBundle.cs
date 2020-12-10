using ShopService.Models.BookBundleModel;
using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Models
{
    public class BookBundle : IBookBundle
    {
        public int Id { get; set; }
        public IBook Book { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public BookBundle() { }

        public BookBundle(IBookBundle bundle)
        {
            Id = bundle.Id;
            Book = new Book(bundle.Book);
            BookId = bundle.BookId;
            Quantity = bundle.Quantity;
            Price = bundle.Price;
        }
    }
}
