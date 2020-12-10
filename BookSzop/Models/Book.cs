using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Models
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Book() { }

        public Book(IBook bookToCopy)
        {
            Id = bookToCopy.Id;
            Title = bookToCopy.Title;
            Author = bookToCopy.Author;
        }
    }
}
