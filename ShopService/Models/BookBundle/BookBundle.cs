using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.BookBundleModel
{
    class BookBundle : IBookBundle
    {
        public int Id { get; set; }
        public IBook Book { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BookBundle bundle &&
                   Id == bundle.Id &&
                   EqualityComparer<IBook>.Default.Equals(Book, bundle.Book) &&
                   BookId == bundle.BookId &&
                   Quantity == bundle.Quantity &&
                   Price == bundle.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Book, BookId, Quantity, Price);
        }
    }
}
