using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.BookModel
{
    class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Id == book.Id &&
                   Title == book.Title &&
                   Author == book.Author;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Author);
        }
    }
}
